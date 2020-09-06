using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Comparators
{
	public class ProductComp : IComparer<ProductDb>
	{
		public Int32 Compare(ProductDb x, ProductDb y)
		{
			if(x.ProductId.CompareTo(y.ProductId) != 0)
				return x.ProductId.CompareTo(y.ProductId);
			throw new Exception("Equal ProductId");
		}
	}
}
