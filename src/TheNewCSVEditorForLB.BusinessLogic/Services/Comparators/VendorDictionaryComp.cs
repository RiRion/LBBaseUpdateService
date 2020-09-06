using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Comparators
{
	public class VendorDictionaryComp : IComparer<VendorsId>
	{
		public Int32 Compare(VendorsId x, VendorsId y)
		{
			if(x.ImportVendorId.CompareTo(y.ImportVendorId) != 0)
				return x.ImportVendorId.CompareTo(y.ImportVendorId);
			return 0;
		}
	}
}