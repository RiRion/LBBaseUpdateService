using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Services.Comparators;

namespace TheNewCSVEditorForLB.Data.Repositories
{
	public class VendorsWithProductIdRepository : IVendorsWithProductIdRepository
	{
		public List<VendorsWithProductId> VendorDictionary { get; set; }

		public VendorsWithProductIdRepository()
		{
			VendorDictionary = new List<VendorsWithProductId>();
		}
		public void AddMany(List<VendorsWithProductId> list)
		{
			VendorDictionary.AddRange(list);
		}

		public void AddVendorId(Int32 correctVendorId, Int32 importVendorId)
		{
			VendorDictionary.Add(new VendorsWithProductId { CorrectVendorId = correctVendorId, ImportVendorId = importVendorId });
			VendorDictionary.Sort(new VendorDictionaryComp());
		}
	}
}