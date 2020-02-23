using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientConsoleSample
{
    public interface IBlog
    {
        Task<string> GetArticle(string article);
    }

    public class Blog : IBlog
    {
        private readonly IHttpClientFactory _client;

        public Blog(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<string> GetArticle(string article)
        {
            var uri = Path.Combine("https://blog.kkbruce.net/", article);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = _client.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }
    }
}
