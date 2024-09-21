using CleanBase.Core.Infrastructure.EF.Repositories;
using CleanBase.Core.Services.Core.Base;
using Core.Data.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomCanvasRepository : EFRepositoryIdentity<CustomCanvas, User>, ICustomCanvasRepository
    {
        public CustomCanvasRepository(ICoreProvider coreProvider, IdentityDbContext<User, IdentityRole<Guid>, Guid> context) : base(coreProvider, context)
        {
        }
    }
}
