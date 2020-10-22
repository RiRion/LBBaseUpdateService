using AutoMapper;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Models;

namespace LBBaseUpdateService.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Vendor, VendorAto>().ReverseMap();
        }
    }
}