using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces
{
	public interface IVendorsWithProductIdRepository
	{
		List<VendorsWithProductId> VendorDictionary { get; set; }
		void AddMany(List<VendorsWithProductId> list);
		void AddVendorId(Int32 correctVendorId, Int32 importVendorId);
	}
}