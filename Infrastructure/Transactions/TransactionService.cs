using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Services.Core.Base;
using Core.Constants;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Transaction;
using Core.ViewModels.Responses.Transaction;
using Hangfire;
using Infrastructure.Jobs;
using Infrastructure.Transactions.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Transactions
{
    public class TransactionService : ServiceBase<Transaction, TransactionRequest, TransactionResponse, GetAllTransactionRequest>, ITransactionService
    {
        private readonly IPaymentService _paymentGateway;
        private readonly IOrderService _orderService;
        public TransactionService(ICoreProvider coreProvider, IUnitOfWork unitOfWork, IPaymentService paymentGateway, IOrderService orderService) : base(coreProvider, unitOfWork)
        {
            _paymentGateway = paymentGateway;
            _orderService = orderService;
        }

        public async Task<TransactionResponse> ConfirmTransaction(string queryString)
        {
            var transaction = await _paymentGateway.ProcessPaymentResponseAsync(queryString);
            await this.Repository.UpdateAsync(transaction);
            return Mapper.Map<TransactionResponse>(transaction);
        }

        public async Task<CreateTransactionResponse> CreateTransactionRequest(TransactionRequest request)
        {
            var order = await _orderService.Ordering(request.Order);

            var paymentMessage = request.PaymentMethod switch
            {
                nameof(PaymentMethodEnum.OnlinePayment) => _paymentGateway.CreatePaymentRequest(request, order),
                nameof(PaymentMethodEnum.CashOnDelivery) => "Thanh toán khi nhận hàng",
                _ => throw new DomainException("Phương thức thanh toán không hợp lệ.","NOT_EXIT_METHOD",null,400,null)
            };

            var transaction = new Transaction
            {
                Id = new Guid(),
                Status = nameof(TransactionStastusEnum.Pending),
                TransactionCode = order.OrderCode,
                PaymentMethod = request.PaymentMethod,
                PaymentUrl = request.PaymentMethod == nameof(PaymentMethodEnum.OnlinePayment) ? paymentMessage : request.ReturnUrl + "/detail-order?vnp_TxnRef="+HttpUtility.UrlEncode($"{order.OrderCode}"),
                ReturnUrl = request.PaymentMethod == nameof(PaymentMethodEnum.OnlinePayment) ? request.ReturnUrl + HttpUtility.UrlEncode($"/{order.OrderCode}") : request.ReturnUrl + "/detail-order?vnp_TxnRef="+HttpUtility.UrlEncode($"{order.OrderCode}"),
                AlreadyPaid = 0,
                Unpaid = order.TotalPrice,
                OrderId = order.Id,
            };

            BackgroundJob.Enqueue(() => _paymentGateway.StartCheckTransactionJob(
                    order.OrderCode,
                    DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss"),
                    order.TotalPrice
                ));

            await this.Repository.AddAsync(transaction);

            var result = Mapper.Map<CreateTransactionResponse>(transaction, otp =>
            {
                otp.AfterMap((src, dest) =>
                {
                    dest.Message = request.PaymentMethod == nameof(PaymentMethodEnum.OnlinePayment) ? paymentMessage : request.ReturnUrl + "/detail-order?vnp_TxnRef="+HttpUtility.UrlEncode($"{order.OrderCode}");
                });
            });

            await this.UnitOfWork.SaveChangesAsync();
            return result;
        }

        public bool ValidateSignature(string queryString, string hashSecret)
        {
            return _paymentGateway.ValidateSignature(queryString, hashSecret);
        }
    }
}
