using AutoMapper;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductAto>()
                .ReverseMap()
                .ForPath(m => m.Categories, 
                    opt => opt.Ignore());
            CreateMap<Product, ProductFromSupplierAto>()
                .ReverseMap()
                .ForMember(m=>m.Sale, opt => opt.Ignore());
        }
    }
}