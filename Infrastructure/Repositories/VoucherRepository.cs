using CleanBase.Core.Infrastructure.EF.Repositories;
using CleanBase.Core.Services.Core.Base;
using Core.Data.Repositories;
using Core.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VoucherRepository : EFRepositoryIdentity<Voucher, User>, IVoucherRepository
    {
        public VoucherRepository(ICoreProvider coreProvider, AppDbContext context) : base(coreProvider, context)
        {
        }
    }
}
