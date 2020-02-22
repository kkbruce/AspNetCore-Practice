using HttpClientSample.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Threading.Tasks;

namespace HttpClientSample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefitController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {
            var blogApi = RestService.For<IBlogApi>("https://blog.kkbruce.net");
            var kkbruce = await blogApi.GetTagName("ASP.NET Core");

            return Ok(kkbruce);
        }
    }
}