using System;
using System.Collections.Generic;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.ProductService.Comparators
{
	public class IeIdDictionaryComparer : IComparer<ProductIdWithInternalId>
	{
		public Int32 Compare(ProductIdWithInternalId x, ProductIdWithInternalId y)
		{
			if(x.ProductId.CompareTo(y.ProductId) != 0)
				return x.ProductId.CompareTo(y.ProductId);
			throw new Exception("Equal ProductId");
		}
	}
}