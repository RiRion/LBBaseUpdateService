using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace BitrixService.Clients.TypedHttp.Interfaces
{
    public interface ITypedHttpClient
    {
        Task<T> GetObjectAsync<T>(string uri);
        Task<T[]> GetCsvObjectAsync<T>(string uri, CsvConfiguration csvConfiguration);
        Task<HttpResponseMessage> PostObjectAsync<T>(string uri, T obj);
        Task<HttpResponseMessage> PutObjectAsync<T>(string uri, T obj);
        Task<HttpResponseMessage> DeleteObjectAsync<T>(string uri, T obj);
    }
}