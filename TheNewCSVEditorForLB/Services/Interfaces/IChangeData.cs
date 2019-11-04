using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Services.Interfaces
{
    public interface IChangeData
    {
        IProductRepository Product { get; set; }
        IVendorDictionaryRepository Vendors { get; set; }
        List<VendorDictionary> NewVendors { get; set; }
        List<Product> NewProductId { get; set; }
        
        void ChangeFieldVendorIdAndVendorCountry();
        void ChangeFieldVibration();
        void ChangeFieldNewAndBest();
        void ChangeFieldIeId();
    }
}