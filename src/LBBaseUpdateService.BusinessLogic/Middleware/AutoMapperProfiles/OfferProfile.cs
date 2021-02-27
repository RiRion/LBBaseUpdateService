using AutoMapper;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;

namespace LBBaseUpdateService.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<Offer, OfferAto>()
                .ReverseMap();
        }
    }
}