using AutoMapper;
using BitrixService.Contracts.ApiModels;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductAto>()
                .ForMember(m => m.Category,
                    opt => opt.MapFrom(
                        p => p.Categories.CategoryId
                    ))
                .ReverseMap()
                .ForPath(m => m.Categories.CategoryId, opt => opt.MapFrom(
                    p => p.Category));
            CreateMap<Product, ProductFromSupplierAto>().ReverseMap();
        }
    }
}