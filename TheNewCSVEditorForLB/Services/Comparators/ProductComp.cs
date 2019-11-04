using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Services.Comparators
{
    public class ProductComp : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            if (x.ProductId.CompareTo(y.ProductId) != 0) return x.ProductId.CompareTo(y.ProductId);
            throw new Exception("Equal ProductId");
        }
    }
}
