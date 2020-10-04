using AutoMapper;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Middleware.AutoMapperProfiles
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