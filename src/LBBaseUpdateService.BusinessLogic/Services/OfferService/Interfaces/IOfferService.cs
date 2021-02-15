using System.Collections.Generic;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.OfferService.Interfaces
{
    public interface IOfferService
    {
        void ReplaceVendorProductIdWithInternalId(List<Offer> offers, ProductIdWithInternalId[] idList);
        Offer[] GetOfferListToAdd(Offer[] vendorOffers, Offer[] internalOffers);
        Offer[] GetOfferListToUpdate(Offer[] vendorOffers, Offer[] internalOffers);
        int[] GetOffersIdToDelete(Offer[] vendorOffers, Offer[] internalOffers);
        void DeleteOffersWithoutProduct(List<Offer> offers, ProductIdWithInternalId[] idSheet);
    }
}