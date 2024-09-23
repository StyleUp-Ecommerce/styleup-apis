using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.Voucher;
using Core.ViewModels.Responses.Voucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Profiles
{
    public class VoucherProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<VoucherRequest, Voucher>();
            CreateMap<Voucher, VoucherResponse>();
            CreateMap<VoucherRequest, VoucherResponse>();

        }
    }
}
