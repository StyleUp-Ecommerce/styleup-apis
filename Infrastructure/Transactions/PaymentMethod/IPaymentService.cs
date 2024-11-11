using Core.Entities;
using Core.ViewModels.Requests.Transaction;
using Core.ViewModels.Responses.Order;
using Core.ViewModels.Responses.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Transactions.PaymentMethod
{
    public interface IPaymentService
    {
        string CreatePaymentRequest(TransactionRequest transaction, OrderResponse orderData);
        bool ValidateSignature(string queryString, string hashSecret);
        Task<Transaction> ProcessPaymentResponseAsync(string queryString);
        Task<string> CheckTransaction(string orderCode, string transactionDate, decimal totalPrice);
        Task StartCheckTransactionJob(string orderCode, string transactionDate, decimal totalPrice);
    }
}
