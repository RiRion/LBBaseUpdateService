using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Data.Interfaces;
using Newtonsoft.Json;

namespace TheNewCSVEditorForLB.Data.Repositories
{
    public class IeIdDictionaryRepository : IIeIdDictionaryRepository
    {
        public List<IeIdDictionary> DictionaryID { get; private set; }
            
        public bool GetFromServer(string path)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage resp = client.GetAsync(path).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    string str = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    DictionaryID = JsonConvert.DeserializeObject<List<IeIdDictionary>>(str);
                }

                return resp.IsSuccessStatusCode;
            }
        }
    }
}