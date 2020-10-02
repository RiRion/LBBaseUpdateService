using AutoMapper;
using BitrixService.Contracts.ApiModels;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class VendorIdProfile : Profile
    {
        public VendorIdProfile()
        {
            CreateMap<VendorId, VendorIdAto>().ReverseMap();
        }
    }
}