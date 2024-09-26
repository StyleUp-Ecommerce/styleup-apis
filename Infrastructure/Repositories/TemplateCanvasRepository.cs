﻿using CleanBase.Core.Infrastructure.EF.Repositories;
using CleanBase.Core.Services.Core.Base;
using Core.Data.Repositories;
using Core.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TemplateCanvasRepository : EFRepositoryIdentity<TemplateCanvas, User>, ITemplateCanvasRepository
    {
        public TemplateCanvasRepository(ICoreProvider coreProvider, AppDbContext context) : base(coreProvider, context)
        {
        }
    }
}
