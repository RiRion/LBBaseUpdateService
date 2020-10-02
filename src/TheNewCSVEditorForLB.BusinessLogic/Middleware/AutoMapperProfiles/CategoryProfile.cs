using AutoMapper;
using BitrixService.Contracts.ApiModels;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Middleware.AutoMapperProfiles
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