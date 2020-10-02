using AutoMapper;
using BitrixService.Contracts.ApiModels;
using TheNewCSVEditorForLB.BusinessLogic.Services.OfferService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Middleware.AutoMapperProfiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<Offer, OfferAto>().ReverseMap();
        }
    }
}