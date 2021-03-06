using System.Collections.Generic;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.OfferService.Interfaces
{
    public interface IOfferService
    {
        IEnumerable<Offer> GetOfferListToAdd(List<Offer> vendorOffers, List<Offer> internalOffers);
        IEnumerable<Offer> GetOfferListToUpdate(List<Offer> vendorOffers, List<Offer> internalOffers);
        int[] GetOffersIdToDelete(List<Offer> vendorOffers, List<Offer> internalOffers);
        void DeleteOffersWithoutProduct(List<Offer> offers, List<Product> products);
    }
}