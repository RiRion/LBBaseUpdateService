using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Services.Comparators
{
    public class VendorDictionaryComp : IComparer<VendorDictionary>
    {
        public int Compare(VendorDictionary x, VendorDictionary y)
        {
            if (x.ImportVendorId.CompareTo(y.ImportVendorId) != 0) return x.ImportVendorId.CompareTo(y.ImportVendorId);
            return 0;
        }
    }
}