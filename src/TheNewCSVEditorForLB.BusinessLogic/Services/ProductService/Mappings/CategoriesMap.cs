using CsvHelper.Configuration;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Mappings
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