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

        public VoucherService(IVoucherRepository voucherRepository, ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
          
        }

        public async Task<VoucherResponse> CheckValidVoucherAsync(CheckValidVoucherRequest request)
        {
            var authorId = IdentityProvider.Identity.UserId;

            var voucher = Repository.Where(p => p.Code == request.VoucherCode.Trim())
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

        public Task<VoucherResponse> GetVoucherByCode(string code)
        {
            var voucher =  Repository.Where(p => p.Code.Trim() == code.Trim())
                            .Select(p => new VoucherResponse
                            {
                                Id = p.Id,
                                Code = p.Code,
                                DiscountType  = p.DiscountType,
                                DiscountValue = p.DiscountValue,
                                ExpirationDate = p.ExpirationDate
                            })
                            .FirstOrDefaultAsync();

            if (voucher is null)
                throw new DomainException("Voucher not valid", "NOT_EXITS", null, 400, null);

            return voucher;
        }

    }
}
