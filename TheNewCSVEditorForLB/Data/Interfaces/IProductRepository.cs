using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Data.Interfaces
{
    public interface IProductRepository
    {
          List<Product> AllProducts { get; }
          void AddMany(List<Product> list);
          void ClearColection();
    }
}