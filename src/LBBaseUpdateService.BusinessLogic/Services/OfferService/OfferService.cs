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
        public void DeleteOffersWithoutProduct(List<Offer> offers, ProductIdWithInternalId[] idList)
        {
            offers.RemoveAll(o => !idList.Any(i => i.ProductExId.Equals(o.ProductExId)));
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