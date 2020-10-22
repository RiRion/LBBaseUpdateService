using System.Collections;
using System.Collections.Generic;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.VendorService.Comparators
{
    public class VendorByIdComparer : IEqualityComparer<Vendor>
    {
        public bool Equals(Vendor x, Vendor y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.VendorId == y.VendorId;
        }

        public int GetHashCode(Vendor obj)
        {
            return obj.VendorId;
        }
    }
}