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
            ListResult<TemplateCanvas> source = await this.ListAsync(request);

            var resultItems = source.Items.Select(templateCanvas => new GetAllTemplateCanvasResponse
            {
                Id = templateCanvas.Id,
                Image = templateCanvas.Image,
                Colors = templateCanvas.CustomCanvas != null && templateCanvas.CustomCanvas.Any()
                    ? string.Join(",", templateCanvas.CustomCanvas.Select(c => c.ColorString))
                    : string.Empty,

                MinPrice = templateCanvas.CustomCanvas?.Min(c => c.Price) ?? 0
            }).ToList();

            return new ListResult<GetAllTemplateCanvasResponse>(resultItems, resultItems.Count)
            {
                Skiped = source.Skiped,
                PageSize = source.PageSize,
            };
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
                ImagesUrl = templateCanvas.Image,
                Colors = string.Join(",", customCanvasProduct.Select(p => p.Color)),
                Name = templateCanvas.Name
            };
        }
    }
}
