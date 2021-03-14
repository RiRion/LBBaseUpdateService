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
        public void DeleteOffersWithoutProduct(List<Offer> offers, List<Product> products)
        {
            offers.RemoveAll(o => !products.Any(p => p.ProductExId.Equals(o.ProductExId)));
        }
        
        public IEnumerable<Offer> GetOfferListToAdd(List<Offer> vendorOffers, List<Offer> internalOffers)
        {
            return vendorOffers.Except(internalOffers, new OfferIdComparer());
        }
        
        public IEnumerable<Offer> GetOfferListToUpdate(List<Offer> vendorOffers, List<Offer> internalOffers)
        {
            var updateList = new List<Offer>();
            foreach (var offer in internalOffers)
            {
                var vendorOffer = vendorOffers.FirstOrDefault(o => o.XmlId == offer.XmlId);
                if (vendorOffer != null && !offer.Equals(vendorOffer))
                {
                    updateList.Add(vendorOffer);
                };
            }

            return updateList;
        }

        public int[] GetOffersIdToDelete(List<Offer> vendorOffers, List<Offer> internalOffers)
        {
            return internalOffers.Except(vendorOffers, new OfferIdComparer())
                .Select(o => o.XmlId).ToArray();
        }
    }
}