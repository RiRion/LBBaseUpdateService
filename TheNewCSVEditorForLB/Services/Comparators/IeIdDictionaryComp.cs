using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Services.Comparators
{
    public class IeIdDictionaryComp : IComparer<ProductIdWithIntarnalId>
    {
        public int Compare(ProductIdWithIntarnalId x, ProductIdWithIntarnalId y)
        {
            if (x.ProductId.CompareTo(y.ProductId) != 0) return x.ProductId.CompareTo(y.ProductId);
            throw new Exception("Equal ProductId");
        }
    }
}