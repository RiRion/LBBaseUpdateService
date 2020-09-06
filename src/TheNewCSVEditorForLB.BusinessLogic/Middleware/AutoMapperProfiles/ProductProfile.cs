using AutoMapper;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDb, ProductDto>()
                .ForMember(m => m.Category, 
                    opt => opt.MapFrom(
                        p=>p.Categories.CategoryId
                        ));
        }
    }
}