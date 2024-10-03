using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Extensions;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.TemplateCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using Core.ViewModels.Responses.TemplateCanvas;
using Domain.Extensions.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class TemplateCanvasService : ServiceBase<TemplateCanvas, TemplateCanvasRequest, TemplateCanvasResponse, GetAllTemplateCanvasRequest>, ITemplateCanvasService
    {
        public TemplateCanvasService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }

        public async Task<ListResult<GetAllTemplateCanvasResponse>> GetAllTemplateCanvasAsync(GetAllTemplateCanvasRequest request)
        {
            request.NormalizeData();

            try
            {
                var source = GetAll(request, Math.Max(request.PageSize, 1))
                    .Include(p => p.CustomCanvas)
                    .Include(e => e.Provider);

                var resultItems = source.AsEnumerable()
                    .Select(templateCanvas =>
                    {
                        var product = Mapper.Map<GetAllTemplateCanvasResponse>(templateCanvas);
                        product.Colors = templateCanvas.CustomCanvas != null && templateCanvas.CustomCanvas.Any()
                            ? string.Join(",", templateCanvas.CustomCanvas.Select(c => c.ColorString))
                            : string.Empty;

                        product.MinPrice = templateCanvas.CustomCanvas != null && templateCanvas.CustomCanvas.Any()
                            ? templateCanvas.CustomCanvas.Min(c => c.Price)
                            : 0;
                        product.ProviderName = templateCanvas?.Provider?.Name;
                        return product;
                    }).ToList();

                return new ListResult<GetAllTemplateCanvasResponse>(resultItems, resultItems.Count)
                {
                    Total = Repository.Where(p => p.IsDeleted == false).Count(),
                    Skiped = request.Skip,
                    PageSize = request.PageSize,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetTemplateCanvasProductResponse> GetTemplateCanvasProductAsync(Guid id)
        {
            var templateCanvas = this.Repository
                .Where(p => p.Id == id)
                .ExcludeProperties(p => p.Content)
                .FirstOrDefault();

            if (templateCanvas is null)
                throw new DomainException("Item not have anything", "NOT_EXITS", null, 200);

            var customCanvasProduct = templateCanvas.CustomCanvas.Select(templateCanvas =>
            {
                return Mapper.Map<CustomCanvas, CustomCanvasProductResponse>(templateCanvas);
            }).ToList();

            return new GetTemplateCanvasProductResponse
            {
                Products = customCanvasProduct,
                Images = templateCanvas.Images.Split(",").ToList(),
                Colors = string.Join(",", customCanvasProduct.Select(p => p.Color)),
                Name = templateCanvas.Name
            };
        }
    }
}
