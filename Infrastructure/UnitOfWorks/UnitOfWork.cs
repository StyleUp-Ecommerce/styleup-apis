using CleanBase.Core.Infrastructure.EF.UnitOfWork;
using CleanBase.Core.Services.Core.Base;
using Core.Data.UnitOfWorks;
using Core.Entities;
using Infrastructure.Context;

namespace Infrastructure.UnitOfWorks
{
    public class UnitOfWorkGeneral : EFUnitOfWorkIdentity<User>, IBaseUnitOfWorks
    {
        public UnitOfWorkGeneral(ICoreProvider coreProvider, AppDbContext context) : base(coreProvider, context)
        {
        }

        protected override void InitRepositoriesMapping()
        {
            base.InitRepositoriesMapping();
        }
    }
}
