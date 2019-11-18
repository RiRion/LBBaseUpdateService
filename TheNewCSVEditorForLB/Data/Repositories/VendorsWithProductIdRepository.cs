using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Services.Comparators;

namespace TheNewCSVEditorForLB.Data.Repositories
{
    public class VendorsWithProductIdRepository : IVendorsWithProductIdRepository
    {
        public List<VendorsWithProductId> AllVendorsWithProductId { get; set; }

        public VendorsWithProductIdRepository()
        {
            AllVendorsWithProductId = new List<VendorsWithProductId>();
        }
        public void AddMany(List<VendorsWithProductId> list)
        {
            AllVendorsWithProductId.AddRange(list);
        }

        public void AddVendorId(int correctVendorId, int importVendorId)
        {
            AllVendorsWithProductId.Add(new VendorsWithProductId{CorrectVendorId = correctVendorId, ImportVendorId = importVendorId});
            AllVendorsWithProductId.Sort(new VendorDictionaryComp());
        }
    }
}