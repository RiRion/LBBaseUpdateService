using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Services.Comparators
{
    public class VendorDictionaryComp : IComparer<VendorsWithProductId>
    {
        public int Compare(VendorsWithProductId x, VendorsWithProductId y)
        {
            if (x.ImportVendorId.CompareTo(y.ImportVendorId) != 0) return x.ImportVendorId.CompareTo(y.ImportVendorId);
            return 0;
        }
    }
}