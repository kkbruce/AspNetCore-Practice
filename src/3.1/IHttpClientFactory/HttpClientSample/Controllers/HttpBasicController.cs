using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientSample.Controllers
{
    [Route("HttpBasic/[action]")]
    [ApiController]
    public class HttpBasicController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public HttpBasicController(IHttpClientFactory httpClient)
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

        public async Task<IActionResult> CallBlogByNamed()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "search/label/ASP.NET Core");
            var client = _httpClient.CreateClient("blog");
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

        public async Task<IActionResult> CallGithubByNamed()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "repos/kkbruce/AspNetCore-Practice/branches");
            var client = _httpClient.CreateClient("github");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}