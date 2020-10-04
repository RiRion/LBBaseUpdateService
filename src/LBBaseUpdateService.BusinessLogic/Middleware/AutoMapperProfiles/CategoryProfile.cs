using AutoMapper;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAto, Category>()
                .ReverseMap();
        }
    }
}