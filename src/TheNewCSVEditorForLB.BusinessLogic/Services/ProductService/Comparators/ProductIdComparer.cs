using System.Collections.Generic;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Comparators
{
    public class ProductIdComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.ProductId == y.ProductId;
        }

        public int GetHashCode(Product obj)
        {
            return obj.ProductId;
        }
    }
}