using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.Transaction;
using Core.ViewModels.Responses.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITransactionService : IServiceBase<Transaction, TransactionRequest, TransactionResponse, GetAllTransactionRequest>
    {
        Task<CreateTransactionResponse> CreateTransactionRequest(TransactionRequest transaction);
        bool ValidateSignature(string queryString, string hashSecret);
        Task<TransactionResponse> ConfirmTransaction(string queryString);
    }
}
