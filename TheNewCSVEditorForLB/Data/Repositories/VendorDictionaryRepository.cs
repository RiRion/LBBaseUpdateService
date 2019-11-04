using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Services.Comparators;

namespace TheNewCSVEditorForLB.Data.Repositories
{
    public class VendorDictionaryRepository : IVendorDictionaryRepository
    {
        public List<VendorDictionary> VendorDictionary { get; set; }

        public VendorDictionaryRepository()
        {
            VendorDictionary = new List<VendorDictionary>();
        }
        public void AddMany(List<VendorDictionary> list)
        {
            VendorDictionary.AddRange(list);
        }

        public void AddVendorId(int correctVendorId, int importVendorId)
        {
            VendorDictionary.Add(new VendorDictionary{CorrectVendorId = correctVendorId, ImportVendorId = importVendorId});
            VendorDictionary.Sort(new VendorDictionaryComp());
        }
    }
}