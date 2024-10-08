﻿using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Provider;
using Core.ViewModels.Responses.Provider;

namespace Domain.Services
{
    public class ProviderService : ServiceBase<Provider, ProviderRequest, ProviderResponse, GetAllProviderRequest>, IProviderService
    {
        public ProviderService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }
    }
}
