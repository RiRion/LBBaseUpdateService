using System.Collections.Generic;
using TheNewCSVEditorForLB.BusinessLogic.Services.OfferService.Models;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.OfferService.Interfaces
{
    public interface IOfferService
    {
        void ReplaceVendorProductIdWithInternalId(List<Offer> offers, ProductIdWithInternalId[] idSheet);
        Offer[] GetOfferSheetToAdd(Offer[] vendorOffers, Offer[] internalOffers);
        Offer[] GetOffersSheetToUpdate(Offer[] vendorOffers, Offer[] internalOffers);
        int[] GetOffersIdToDelete(Offer[] vendorOffers, Offer[] internalOffers);
        void SetDefaultFieldWeight(IEnumerable<Offer> offers);
    }
}