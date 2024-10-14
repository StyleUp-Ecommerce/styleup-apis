using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.Transaction;
using Core.ViewModels.Responses.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Profiles
{
    public class TransactionProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<Transaction, CreateTransactionResponse>();
            CreateMap<TransactionRequest, TransactionResponse>();
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<TransactionRequest, Transaction>();
            CreateMap<ListResult<TransactionRequest>, ListResult<TransactionResponse>>();
            CreateMap<Transaction, GetTransactionWithOrderCodeResponse>();
        }
    }
}
