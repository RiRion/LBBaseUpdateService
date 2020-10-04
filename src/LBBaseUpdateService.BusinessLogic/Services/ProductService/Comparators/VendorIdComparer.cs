using System;
using System.Collections.Generic;
using LBBaseUpdateService.BusinessLogic.Services.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.ProductService.Comparators
{
    public class VendorIdComparer : IEqualityComparer<VendorId>, IComparer<VendorId>
    {
        public bool Equals(VendorId x, VendorId y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.InternalId == y.InternalId && x.ExternalId == y.ExternalId;
        }

        public int GetHashCode(VendorId obj)
        {
            return HashCode.Combine(obj.InternalId, obj.ExternalId);
        }

        public int Compare(VendorId x, VendorId y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return x.ExternalId.CompareTo(y.ExternalId);
        }
    }
}