using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Helpers;
using Core.Services;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using Domain.Extensions.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Services
{
    public class CustomCanvasService : ServiceBase<CustomCanvas, CustomCanvasRequest, CustomCanvasResponse, GetAllCustomCanvasRequest>, ICustomCanvasService
    {
        public CustomCanvasService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }

        protected override IQueryable<CustomCanvas> ApplyGetAllOperator(IQueryable<CustomCanvas> query)
        {
            return query.Where(p => p.IsPublic);
        }

        public async Task<Guid> CustomNewTemplate(Guid customCanvasId)
        {
            var customCanvas = Repository.Where(p => p.Id == customCanvasId)
                .Select(p => new CustomCanvas
                {
                    Id = Guid.NewGuid(),
                    Content = p.Content,
                    CanvasCode = p.CanvasCode + "-" + Randomize.RandomString(5),
                    Color = p.Color,
                    Sizes = p.Sizes,
                    TemplateId = p.TemplateId,
                    AuthorId = IdentityProvider.Identity.UserId,
                    IsPublic = false,
                    Images = "",
                    LensVRUrl = ""
                })
                .FirstOrDefault();

            this.Repository.Add(customCanvas);
            this.UnitOfWork.SaveChanges();

            return customCanvas.Id;
        }

        public async Task<Dictionary<Guid, CustomCanvas>> GetDictionaryByIds(List<Guid> ids, Expression<Func<CustomCanvas, object>>[] excludedProperties)
        {
            var canvases = await Repository.Where(canvas => ids.Contains(canvas.Id))
                                            .ExcludeProperties(excludedProperties)
                                            .ToListAsync();

            var canvasDictionary = canvases.ToDictionary(canvas => canvas.Id);

            return canvasDictionary;
        }
    }
}
