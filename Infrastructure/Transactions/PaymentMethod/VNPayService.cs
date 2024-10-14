using Core.Entities;
using Core.ViewModels.Requests.Transaction;
using Core.ViewModels.Responses.Order;
using Core.ViewModels.Responses.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public VNPayService(string vnpayUrl, string tmnCode, string hashSecret)
        {
            _vnpayUrl = vnpayUrl;
            _tmnCode = tmnCode;
            _hashSecret = hashSecret;
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
            payData.Add("vnp_Bill_Email", orderData.RecipientEmail); // Địa chỉ email của khách hàng (nếu có)
            payData.Add("vnp_Bill_FirstName", orderData.RecipientName.Split(" ")[0]); // Họ của khách hàng (nếu có)
            payData.Add("vnp_Bill_LastName", orderData.RecipientName.Split(" ")[1]); // Tên của khách hàng (nếu có)
            payData.Add("vnp_Bill_Address", orderData.Address); // Địa chỉ của khách hàng (nếu có)
            payData.Add("vnp_Bill_Country", "VN"); // Mã quốc gia của khách hàng (VN)

            string queryString = BuildQueryString(payData);
            string secureHash = HmacSHA512(_hashSecret, queryString);
            string paymentUrl = $"{_vnpayUrl}?{queryString}&vnp_SecureHash={secureHash}";

            return paymentUrl;
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
    }
}
