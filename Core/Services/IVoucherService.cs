using CleanBase.Core.Services.Core.Generic;
using Core.Data.Repositories;
using Core.Entities;
using Core.ViewModels.Requests.Voucher;
using Core.ViewModels.Responses.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IVoucherService : IServiceBase<Voucher, VoucherRequest, VoucherResponse,VoucherGetAllRequest>
    {
        Task<VoucherResponse> CheckValidVoucherAsync(CheckValidVoucherRequest request);
        Task<VoucherResponse> GetVoucherByCode(string code);
    }
}
