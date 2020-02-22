using HttpClientSample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HttpClientSample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefitDIController : ControllerBase
    {
        private readonly IBlogApi _blogApi;

        public RefitDIController(IBlogApi blogApi)
        {
            _blogApi = blogApi;
        }

        public async Task<IActionResult> Get()
        {
            var kkbruce = await _blogApi.GetTagName("ASP.NET Core");
            return Ok(kkbruce);
        }
    }
}