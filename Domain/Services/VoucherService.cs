using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Helpers;
using CleanBase.Core.Services.Core.Base;
using Core.Data.Repositories;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Voucher;
using Core.ViewModels.Responses.Voucher;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class VoucherService : ServiceBase<Voucher, VoucherRequest, VoucherResponse, VoucherGetAllRequest>, IVoucherService
    {
        public VoucherService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }

        public async Task<VoucherResponse> CheckValidVoucherAsync(CheckValidVoucherRequest request)
        {
            var authorId = new Guid("d7749509-d3c4-4c4f-8870-88997e75fcce");

            var voucher = Repository.Where(p => p.Id == request.VoucherId)
                            .Select(p => new Voucher
                            {
                                UserVouchers = p.UserVouchers
                                    .Where(uv => uv.Id == authorId)  
                                    .Select(uv => new UserVoucher
                                    {
                                        UserId = uv.UserId,
                                        Id = uv.Id
                                    })
                                    .ToList(),
                                ExpirationDate = p.ExpirationDate,
                                Code = p.Code,
                                DiscountType = p.DiscountType,
                                DiscountValue = p.DiscountValue,
                            })
                            .FirstOrDefault();

            if (voucher is null) 
                throw new DomainException("Voucher not exits", "NOT_EXITS", null, 400, null);

            if(voucher.ExpirationDate > DateTimeHelper.VnNow()) 
                throw new DomainException("Voucher has exits", "NOT_EXITS", null, 400, null);

            return Mapper.Map<VoucherResponse>(voucher);
        }
    }
}
