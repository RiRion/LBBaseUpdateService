using AutoMapper;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Categories, CategoriesAto>().ReverseMap();
        }
    }
}