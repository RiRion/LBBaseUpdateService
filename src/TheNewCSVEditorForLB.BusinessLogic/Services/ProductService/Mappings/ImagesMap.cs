using CsvHelper.Configuration;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.BusinessLogic.Services.Mappings
{
	public sealed class ImagesMap : ClassMap<Images>
	{
		public ImagesMap()
		{
			Map(m => m.Img1).Name("img1");
			Map(m => m.Img2).Name("img2");
			Map(m => m.Img3).Name("img3");
			Map(m => m.Img4).Name("img4");
			Map(m => m.Img5).Name("img5");
			Map(m => m.Img6).Name("img6");
			Map(m => m.Img7).Name("img7");
			Map(m => m.Img8).Name("img8");
			Map(m => m.Img9).Name("img9");
			Map(m => m.Img10).Name("img10");
		}
	}
}