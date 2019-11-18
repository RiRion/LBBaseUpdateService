using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Data.Interfaces
{
	public interface IVendorsWithProductIdRepository
	{
		List<VendorsWithProductId> VendorDictionary { get; set; }
		void AddMany(List<VendorsWithProductId> list);
		void AddVendorId(Int32 correctVendorId, Int32 importVendorId);
	}
}