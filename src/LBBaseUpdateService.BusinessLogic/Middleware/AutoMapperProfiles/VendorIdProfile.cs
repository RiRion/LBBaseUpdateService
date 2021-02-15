using AutoMapper;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic.Services.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class VendorIdProfile : Profile
    {
        public VendorIdProfile()
        {
            CreateMap<VendorId, VendorIdAto>().ReverseMap();
        }
    }
}