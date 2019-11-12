using System.Collections.Generic;
using System.Net.Http;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Data.Interfaces;
using Newtonsoft.Json;

namespace TheNewCSVEditorForLB.Data.Repositories
{
    public class ProductIdWithInternalIdRepository : IProductIdWithInternalIdRepository
    {
        public List<ProductIdWithIntarnalId> AllIntarnalId { get; private set; }
            
        public bool GetInternalIdFromServer(string path)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage resp = client.GetAsync(path).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string str = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    AllIntarnalId = JsonConvert.DeserializeObject<List<ProductIdWithIntarnalId>>(str);
                }

                return resp.IsSuccessStatusCode;
            }
        }
    }
}