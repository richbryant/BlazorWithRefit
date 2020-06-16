using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Refit;

namespace BlazorWithRefit.Client
{
    public class JsonContentSerializer : IContentSerializer
    {
        private readonly JsonSerializerOptions serializerOptions;
 
        public JsonContentSerializer(JsonSerializerOptions serializerOptions = null)
        {
            this.serializerOptions = serializerOptions ?? new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
 
        public async Task<T> DeserializeAsync<T>(HttpContent content)
        {
            using var utf8Json = await content.ReadAsStreamAsync()
                .ConfigureAwait(false);
 
            return await JsonSerializer.DeserializeAsync<T>(utf8Json,
                serializerOptions).ConfigureAwait(false);
        }
 
        public Task<HttpContent> SerializeAsync<T>(T item)
        {
            var json = JsonSerializer.Serialize(item, serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
 
            return Task.FromResult((HttpContent)content);
        }
    }
}
