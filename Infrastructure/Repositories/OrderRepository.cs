using CleanBase.Core.Infrastructure.EF.Repositories;
using CleanBase.Core.Services.Core.Base;
using Core.Data.Repositories;
using Core.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : EFRepositoryIdentity<Order, User>, IOrderRepository
    {
        public OrderRepository(ICoreProvider coreProvider, AppDbContext context) : base(coreProvider, context)
        {
        }
    }
}
