using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> AllProducts { get; }

        public ProductRepository()
        {
            AllProducts = new List<Product>();
        }

        public void AddMany(List<Product> list)
        {
            AllProducts.AddRange(list);
        }

        public void ClearColection()
        {
            AllProducts.Clear();
        }
    }
}