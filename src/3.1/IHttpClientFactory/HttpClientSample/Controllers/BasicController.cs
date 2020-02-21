using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}