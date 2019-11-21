using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Comparators
{
	public class IeIdDictionaryComp : IComparer<ProductIdWithIntarnalId>
	{
		public Int32 Compare(ProductIdWithIntarnalId x, ProductIdWithIntarnalId y)
		{
			if(x.ProductId.CompareTo(y.ProductId) != 0)
				return x.ProductId.CompareTo(y.ProductId);
			throw new Exception("Equal ProductId");
		}
	}
}