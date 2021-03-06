using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BitrixService.Clients.TypedHttp.Exceptions;
using BitrixService.Clients.TypedHttp.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace BitrixService.Clients.TypedHttp
{
    public class TypedHttpClient : HttpClient, ITypedHttpClient
    {
        public JsonSerializerSettings SerializerSettings { get; }

        public TypedHttpClient()
        {
            SerializerSettings = new JsonSerializerSettings();
        }

        public TypedHttpClient(JsonSerializerSettings serializerSettings)
        {
            SerializerSettings = serializerSettings;
        }

        public TypedHttpClient(JsonSerializerSettings serializerSettings, HttpMessageHandler messageHandler) : base(
            messageHandler)
        {
            SerializerSettings = serializerSettings;
        }
        // ITypedHttpClient ////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<T> GetObjectAsync<T>(string uri)
        {
            using (var response = await GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpServerException(response.StatusCode, 
                        $"Error with status code {response.StatusCode}. GET: {uri}.");
                return await DeserializeObject<T>(response, SerializerSettings);
            }
        }
        
        public async Task<T[]> GetCsvObjectAsync<T>(string uri, CsvConfiguration csvConfiguration)
        {
            using (var response = await GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpServerException(response.StatusCode, 
                        $"Error with status code {response.StatusCode}. GET: {uri}.");
                return await CsvDeserializeObject<T>(response, csvConfiguration);
            }
        }

        public async Task<HttpResponseMessage> PostObjectAsync<T>(string uri, T obj)
        {
            var content = obj as HttpContent ?? SerializeObject(obj, SerializerSettings);
            return await PostAsync(uri, content);
        }

        public async Task<HttpResponseMessage> PutObjectAsync<T>(string uri, T obj)
        {
            var content = obj as HttpContent ?? SerializeObject(obj, SerializerSettings);
            return await PutAsync(uri, content);
        }

        public async Task<HttpResponseMessage> DeleteObjectAsync<T>(string uri, T obj)
        {
            var content = obj as HttpContent ?? SerializeObject(obj, SerializerSettings);
            return await PostAsync(uri, content);
        }
        
        // FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////////////
        protected HttpContent SerializeObject<T>(T obj, JsonSerializerSettings settings)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj, settings));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        protected T Deserialize<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response, SerializerSettings);
        }

        protected async Task<T> DeserializeAsync<T>(HttpResponseMessage responseMessage, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync(), settings);
        }
        
        protected async Task<T> DeserializeObject<T>(HttpResponseMessage message, JsonSerializerSettings settings)
        {
            var reader = JsonSerializer.Create(settings);
            using (var streamReader = new StreamReader(await message.Content.ReadAsStreamAsync()))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    return reader.Deserialize<T>(jsonTextReader);
                }
            }
        }
        
        protected async Task<T[]> CsvDeserializeObject<T>(HttpResponseMessage responseMessage, CsvConfiguration csvConfiguration)
        {
            using (var stream = new StreamReader(await responseMessage.Content.ReadAsStreamAsync()))
            {
                using(var reader = new CsvReader(stream, csvConfiguration))
                {
                    return reader.GetRecords<T>().ToArray();
                }
            }
            
        }
    }
}