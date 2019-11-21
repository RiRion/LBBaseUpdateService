using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Comparators
{
	public class ProductComp : IComparer<Product>
	{
		public Int32 Compare(Product x, Product y)
		{
			if(x.ProductId.CompareTo(y.ProductId) != 0)
				return x.ProductId.CompareTo(y.ProductId);
			throw new Exception("Equal ProductId");
		}
	}
}
