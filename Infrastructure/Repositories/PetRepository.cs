using CleanBase.Core.Infrastructure.EF.Repositories;
using CleanBase.Core.Services.Core.Base;
using Core.Data.Repositories;
using Core.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PetRepository : EFRepositoryIdentity<Pet, User>, IPetRepository
    {
        public PetRepository(ICoreProvider coreProvider, AppDbContext context) : base(coreProvider, context)
        {
        }

    }
}
