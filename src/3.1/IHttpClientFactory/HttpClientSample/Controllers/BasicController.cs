using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientSample.Controllers
{
    [Route("Basic/[action]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public BasicController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }


        /// <summary>
        /// Use Browser test it.
        /// </summary>
        /// <returns>UI display content.</returns>
        public async Task<IActionResult> CallSkilltree()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://skilltree.my");
            var client = _httpClient.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, new MediaTypeHeaderValue("text/html"));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}