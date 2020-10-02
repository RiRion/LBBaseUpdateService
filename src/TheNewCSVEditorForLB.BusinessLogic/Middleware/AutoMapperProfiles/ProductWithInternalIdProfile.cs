using AutoMapper;
using BitrixService.Contracts.ApiModels;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class ProductWithInternalIdProfile : Profile
    {
        public ProductWithInternalIdProfile()
        {
            CreateMap<ProductIdWithInternalId, ProductIdWithInternalIdAto>()
                .ReverseMap();
        }
    }
}