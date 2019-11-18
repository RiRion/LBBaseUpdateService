using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Services.Interfaces
{
    public interface IChangeData
    {
        IProductRepository Product { get; set; }
        IVendorsWithProductIdRepository Vendors { get; set; }
        List<VendorsWithProductId> NewVendors { get; set; }
        List<Product> NewProductId { get; set; }
        
        void ChangeFieldVendorIdAndVendorCountry();
        void ChangeFieldVibration();
        void ChangeFieldNewAndBest();
        void ChangeFieldIeId();
    }
}