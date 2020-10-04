using CsvHelper.Configuration;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.Mappings
{
	public sealed class CategoriesMap : ClassMap<Categories>
	{
		public CategoriesMap()
		{
			Map(m => m.Category1).Name("categories_1");
			Map(m => m.Category2).Name("categories_2");
			Map(m => m.Category3).Name("categories_3");
		}
	}
}