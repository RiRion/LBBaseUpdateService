using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TheNewCSVEditorForLB.BusinessLogic.ExternalClients.LoveBeri.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.ExternalClients.LoveBeri.Models.Config;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Exceptions;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Storage;

namespace TheNewCSVEditorForLB.BusinessLogic.ExternalClients.LoveBeri
{
	public class LoveBeriClient : ILoveBeriClient
	{
		private readonly LoveBeriClientConfig _config;


		public LoveBeriClient(LoveBeriClientConfig config)
		{
			_config = config;
		}


		// ILoveBeriClient ////////////////////////////////////////////////////////////////////////
		public async Task<ProductIdWithIntarnalId[]> GetInternalIdAsync()
		{
			using(var client = new HttpClient())
			{
				var response = await client.GetAsync($"{_config.BaseUrl}/bitrix/my_tools/getDictionaryId.php");
				if(!response.IsSuccessStatusCode)
					throw new ExternalIdRetrievalException($"Status code {response.StatusCode}, message: {await response.Content.ReadAsStringAsync()}");

				return JsonConvert.DeserializeObject<ProductIdWithIntarnalId[]>(await response.Content.ReadAsStringAsync());
			}
		}
	}
}