using AutoMapper;
using BitrixService.Contracts.ApiModels;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<Categories, CategoriesAto>().ReverseMap();
        }
    }
}