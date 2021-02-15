using System;
using System.Collections.Generic;
using System.Linq;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Comparators;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Exceptions;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.OfferService
{
    public class OfferService : IOfferService
    {
        public void ReplaceVendorProductIdWithInternalId(List<Offer> offers, ProductIdWithInternalId[] idList)
        {
            foreach (var offer in offers)
            {
                var pair = idList.FirstOrDefault(
                    p => p.ProductExId == offer.ProductExId);
                if (pair is null) 
                    throw new ProductIdNotFoundException($"Product with provided ID {offer.ProductIeId} not found.");
                offer.ProductIeId = pair.ProductIeId;
            }
        }

        public void DeleteOffersWithoutProduct(List<Offer> offers, ProductIdWithInternalId[] idSheet)
        {
            offers.RemoveAll(o => !idSheet.Any(i => i.ProductExId.Equals(o.ProductExId)));
        }
        
        public Offer[] GetOfferListToAdd(Offer[] vendorOffers, Offer[] internalOffers)
        {
            return vendorOffers.Except(internalOffers, new OfferIdComparer()).ToArray();
        }
        
        public Offer[] GetOfferListToUpdate(Offer[] vendorOffers, Offer[] internalOffers)
        {
            var updateList = new List<Offer>();
            foreach (var offer in internalOffers)
            {
                var vendorOffer = vendorOffers.FirstOrDefault(o => o.XmlId == offer.XmlId);
                if (vendorOffer != null && !offer.Equals(vendorOffer))
                {
                    vendorOffer.Id = offer.Id;
                    updateList.Add(vendorOffer);
                };
            }

            return updateList.ToArray();
        }

        public int[] GetOffersIdToDelete(Offer[] vendorOffers, Offer[] internalOffers)
        {
            return internalOffers.Except(vendorOffers, new OfferIdComparer())
                .Select(o => o.Id).ToArray();
        }
    }
}