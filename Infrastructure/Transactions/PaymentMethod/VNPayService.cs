using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Services.Core.Base;
using Core.Constants;
using Core.Data.Repositories;
using Core.Entities;
using Core.ViewModels.Requests.Transaction;
using Core.ViewModels.Responses.Order;
using Core.ViewModels.Responses.Transaction;
using Core.ViewModels.Responses.Transaction.VNPay;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Transactions.PaymentMethod
{
    public class VNPayService : IPaymentService
    {
        private readonly string _hashSecret;
        private readonly string _vnpayUrl;
        private readonly string _tmnCode;
        private readonly string _vnpayCheckTransactionUrl;
        private readonly IUnitOfWork _unitOfWork;

        // Constructor
        public VNPayService(IUnitOfWork unitOfWork)
        {
            _hashSecret = "IE3N9LTPY78WFWJK7GJO8KQFH3ZQ1IG5";
            _tmnCode = "HBTD6P6Q";
            _vnpayUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            _vnpayCheckTransactionUrl = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
            _unitOfWork = unitOfWork;
        }

        public string CreatePaymentRequest(TransactionRequest transaction, OrderResponse orderData)
        {
            var payData = new SortedList<string, string>(new VnPayCompare());

            payData.Add("vnp_Version", "2.1.0");
            payData.Add("vnp_Command", "pay");
            payData.Add("vnp_TmnCode", _tmnCode); // Mã định danh của merchant
            payData.Add("vnp_Amount", ((int)orderData.TotalPrice*100).ToString()); // Số tiền thanh toán
            payData.Add("vnp_CurrCode", "VND"); // Đơn vị tiền tệ
            payData.Add("vnp_TxnRef", orderData.OrderCode.ToString()); // Mã tham chiếu giao dịch
            payData.Add("vnp_OrderInfo", $"thanh toan don hang {orderData.OrderCode} voi tong so tien {orderData.TotalPrice} VND"); // Thông tin mô tả thanh toán
            payData.Add("vnp_OrderType", "topup"); // Mã danh mục hàng hóa
            payData.Add("vnp_CreateDate", DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss")); // Thời gian ghi nhận giao dịch
            payData.Add("vnp_ExpireDate", DateTime.UtcNow.AddHours(7).AddMinutes(15).ToString("yyyyMMddHHmmss")); // Thời gian hết hạn thanh toán (15 phút sau)
            payData.Add("vnp_ReturnUrl", transaction.ReturnUrl); // Địa chỉ trả về sau khi thanh toán
            payData.Add("vnp_IpAddr", GetIpAddress()); // Địa chỉ IP của khách hàng
            payData.Add("vnp_Locale", "vn"); // Ngôn ngữ khách hàng đang sử dụng

            payData.Add("vnp_Bill_Mobile", orderData.RecipientPhone); // Số điện thoại của khách hàng (nếu có)
            payData.Add("vnp_Bill_Email", orderData.RecipientMail); // Địa chỉ email của khách hàng (nếu có)
            payData.Add("vnp_Bill_FirstName", orderData.RecipientName.Split(" ")[0]); // Họ của khách hàng (nếu có)
            payData.Add("vnp_Bill_LastName", orderData.RecipientName.Split(" ")[1]); // Tên của khách hàng (nếu có)
            payData.Add("vnp_Bill_Address", orderData.Address); // Địa chỉ của khách hàng (nếu có)
            payData.Add("vnp_Bill_Country", "VN"); // Mã quốc gia của khách hàng (VN)

            string queryString = BuildQueryString(payData);
            string secureHash = HmacSHA512(_hashSecret, queryString);
            string paymentUrl = $"{_vnpayUrl}?{queryString}&vnp_SecureHash={secureHash}";

            return paymentUrl;
        }


        public async Task<string> CheckTransaction(string orderCode, string transactionDate, decimal totalPrice)
        {
            var queryData = new SortedList<string, string>(new VnPayCompare())
        {
            { "vnp_RequestId", Guid.NewGuid().ToString() },
            { "vnp_Version", "2.1.0" },
            { "vnp_Command", "querydr" },
            { "vnp_TmnCode", _tmnCode },
            { "vnp_TxnRef", orderCode },
            { "vnp_TransactionDate", transactionDate },
            { "vnp_CreateDate", DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss") },
            { "vnp_IpAddr", GetIpAddress() },
            { "vnp_OrderInfo", $"thanh toan don hang {orderCode} voi tong so tien {totalPrice} VND" }
        };

            string data = string.Join("|",
                queryData["vnp_RequestId"],
                queryData["vnp_Version"],
                queryData["vnp_Command"],
                queryData["vnp_TmnCode"],
                queryData["vnp_TxnRef"],
                queryData["vnp_TransactionDate"],
                queryData["vnp_CreateDate"],
                queryData["vnp_IpAddr"],
                queryData["vnp_OrderInfo"]);

            string secureHash = HashWithSecureType(_hashSecret, data);

            var requestData = new
            {
                vnp_RequestId = queryData["vnp_RequestId"],
                vnp_Version = queryData["vnp_Version"],
                vnp_Command = queryData["vnp_Command"],
                vnp_TmnCode = queryData["vnp_TmnCode"],
                vnp_TxnRef = queryData["vnp_TxnRef"],
                vnp_TransactionDate = queryData["vnp_TransactionDate"],
                vnp_CreateDate = queryData["vnp_CreateDate"],
                vnp_IpAddr = queryData["vnp_IpAddr"],
                vnp_OrderInfo = queryData["vnp_OrderInfo"],
                vnp_SecureHash = secureHash
            };

            var client = new RestClient(new RestClientOptions(_vnpayCheckTransactionUrl) { MaxTimeout = -1 });
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json"); 
            request.AddJsonBody(requestData);

            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        private string HashWithSecureType(string secretKey, string data)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower(); // Chuyển đổi thành chuỗi hex
            }
        }
        public bool ValidateSignature(string queryString, string inputHash)
        {
            string calculatedHash = HmacSHA512(_hashSecret, queryString);
            return string.Equals(calculatedHash, inputHash, StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<Transaction> ProcessPaymentResponseAsync(string queryString)
        {
            var queryData = HttpUtility.ParseQueryString(queryString);
            var transaction = new Transaction
            {
                Id = new Guid(queryData["vnp_TxnRef"]),
                Status = queryData["vnp_ResponseCode"] == "00" ? "Success" : "Failed"
            };

            return transaction;
        }

        private string HmacSHA512(string key, string input)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                return string.Concat(hashValue.Select(b => b.ToString("x2")));
            }
        }

        private string BuildQueryString(SortedList<string, string> data)
        {
            return string.Join("&", data.Select(kv => $"{WebUtility.UrlEncode(kv.Key)}={WebUtility.UrlEncode(kv.Value)}"));
        }

        private string GetIpAddress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
        }

        // job 

        public async Task StartCheckTransactionJob(string orderCode, string transactionDate, decimal totalPrice)
        {
            int initialDelay = 5 * 60; 
            int subsequentDelay = 6 * 60; 
            int waitCratePaymentDelay = 5 * 60; 
            int maxAttempts = 3; 
            int attempt = 0;

            while (attempt < maxAttempts)
            {
                attempt++;


                if (attempt == 1)
                {
                    await Task.Delay(initialDelay * 1000);
                }

                var response = await CheckTransaction(orderCode, transactionDate, totalPrice);


                var responseObj = JsonConvert.DeserializeObject<VnPayCheckTransactionResponse>(response);

                if (responseObj?.vnp_TransactionStatus == "00")
                {
                    UpdateTransactionStatus(orderCode, TransactionStastusEnum.Success);

                    break;
                }

                if (responseObj?.vnp_TransactionStatus == null)
                {
                    if (attempt == maxAttempts)
                    {
                        UpdateTransactionStatus(orderCode, TransactionStastusEnum.Failed);
                        break;
                    }

                    await Task.Delay(waitCratePaymentDelay * 1000); 
                    continue;
                }

                if (responseObj?.vnp_TransactionStatus == "01")
                {
                    if (attempt == maxAttempts)
                    {
                        UpdateTransactionStatus(orderCode, TransactionStastusEnum.Failed);
                        break;
                    }

                    await Task.Delay(subsequentDelay * 1000);
                    attempt++;
                    subsequentDelay *= 2;

                    continue;
                }

                if (attempt == maxAttempts)
                {
                    UpdateTransactionStatus(orderCode, TransactionStastusEnum.Failed);
                    break;
                }
            }
        }

        private void UpdateTransactionStatus(string transCode, TransactionStastusEnum status)
        {
            switch (status)
            {
                case TransactionStastusEnum.Success:
                    var transactionToUpdateSuccess = _unitOfWork.Repository<ITransactionRepository>()
                       .Where(p => p.TransactionCode == transCode)
                       .FirstOrDefault(); 

                    if (transactionToUpdateSuccess != null)
                    {
                        transactionToUpdateSuccess.Status = status.ToString();
                        transactionToUpdateSuccess.AlreadyPaid = transactionToUpdateSuccess.Unpaid;
                        transactionToUpdateSuccess.Unpaid = 0;

                        _unitOfWork.Repository<ITransactionRepository>().Update(transactionToUpdateSuccess,true);
                        _unitOfWork.SaveChanges();
                    }
                    break;

                case TransactionStastusEnum.Failed:
                    var transactionToUpdateFaild = _unitOfWork.Repository<ITransactionRepository>()
                       .Where(p => p.TransactionCode == transCode)
                       .FirstOrDefault();

                    if (transactionToUpdateFaild != null)
                    {
                        transactionToUpdateFaild.Status = status.ToString();

                        _unitOfWork.Repository<ITransactionRepository>().Update(transactionToUpdateFaild, true);
                        _unitOfWork.SaveChanges();
                    }
                    break;
            }
            
        }
    }
}
