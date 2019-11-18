using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Data.Repositories
{
	public class ProductIdWithInternalIdRepository : IProductIdWithInternalIdRepository
	{
		public List<ProductIdWithIntarnalId> AllIntarnalId { get; private set; }

		public Boolean GetInternalIdFromServer(String path)
		{
			using(var client = new HttpClient())
			{
				var resp = client.GetAsync(path).GetAwaiter().GetResult();
				if(resp.IsSuccessStatusCode)
				{
					var str = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
					AllIntarnalId = JsonConvert.DeserializeObject<List<ProductIdWithIntarnalId>>(str);
				}

				return resp.IsSuccessStatusCode;
			}
		}
	}
}