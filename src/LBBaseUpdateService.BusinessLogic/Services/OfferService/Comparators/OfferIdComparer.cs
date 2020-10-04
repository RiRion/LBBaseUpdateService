using System.Collections.Generic;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.OfferService.Comparators
{
    public class OfferIdComparer : IEqualityComparer<Offer>
    {
        public bool Equals(Offer x, Offer y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.XmlId == y.XmlId;
        }

        public int GetHashCode(Offer obj)
        {
            return obj.XmlId;
        }
    }
}