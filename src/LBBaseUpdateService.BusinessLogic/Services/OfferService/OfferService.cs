using System;
using System.Collections.Generic;
using System.Linq;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Comparators;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.OfferService
{
    public class OfferService : IOfferService
    {
        public void ReplaceVendorProductIdWithInternalId(List<Offer> offers, ProductIdWithInternalId[] idSheet)
        {
            List<Offer> copyList = new List<Offer>(offers); // TODO: There are offers for products that do not exist in the database.
            foreach (var offer in copyList)
            {
                var pair = idSheet.FirstOrDefault(
                    p => p.ProductId == offer.ProductId);
                if (pair != null) offer.ProductId = pair.IeId;
                else offers.Remove(offer);
            }
        }
        
        public Offer[] GetOfferSheetToAdd(Offer[] vendorOffers, Offer[] internalOffers)
        {
            return vendorOffers.Except(internalOffers, new OfferIdComparer()).ToArray();
        }
        
        public Offer[] GetOffersSheetToUpdate(Offer[] vendorOffers, Offer[] internalOffers)
        {
            var updateSheet = new List<Offer>();
            foreach (var offer in internalOffers)
            {
                var vendorOffer = vendorOffers.FirstOrDefault(o => o.XmlId == offer.XmlId);
                if (vendorOffer != null && !offer.Equals(vendorOffer))
                {
                    vendorOffer.Id = offer.Id;
                    updateSheet.Add(vendorOffer);
                };
            }

            return updateSheet.ToArray();
        }

        public void SetDefaultFieldWeight(IEnumerable<Offer> offers)
        {
            foreach (var offer in offers)
            {
                if (String.IsNullOrEmpty(offer.Weight)) offer.Weight = "0";
            }
        }

        public int[] GetOffersIdToDelete(Offer[] vendorOffers, Offer[] internalOffers)
        {
            return internalOffers.Except(vendorOffers, new OfferIdComparer())
                .Select(o => o.Id).ToArray();
        }
    }
}