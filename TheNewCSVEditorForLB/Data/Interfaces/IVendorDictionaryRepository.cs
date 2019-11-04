using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Data.Interfaces
{
    public interface IVendorDictionaryRepository
    {
        List<VendorDictionary> VendorDictionary { get; set; }
        void AddMany(List<VendorDictionary> list);
        void AddVendorId(int correctVendorId, int importVendorId);
    }
}