using Microsoft.AspNetCore.Mvc;

namespace SampleTypeSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // http://localhost:5000/api/test/SUCCESS (HTTP 200)
        // http://localhost:5000/api/test/?status=SUCCESS (HTTP 404)
        //[HttpGet("{status}")]
        //public IActionResult Get(string status)
        //{
        //    return Ok(status);
        //}

        // http://localhost:5000/api/test/SUCCESS (HTTP 204)
        // http://localhost:5000/api/test/?status=SUCCESS (HTTP 404)
        //[HttpGet("{status}")]
        //public IActionResult Test([FromQuery] string status)
        //{
        //    return Ok(status);
        //}

        // http://localhost:5000/api/test/SUCCESS (HTTP 204)
        // http://localhost:5000/api/test/?status=SUCCESS (HTTP 200)
        //[HttpGet("{status?}")]
        //public IActionResult Test([FromQuery] string status)
        //{
        //    return Ok(status);
        //}

        // http://localhost:5000/api/test/SUCCESS (HTTP 404)
        // http://localhost:5000/api/test/?status=SUCCESS (HTTP 200)
        //[HttpGet]
        //public IActionResult Test([FromQuery] string status)
        //{
        //    return Ok(status);
        //}

        // http://localhost:5000/api/test/
        // Body (HTTP 400)
        // {"status":"SUCCESS"}
        // Body (HTTP 200)
        // "SUCESS"
        //[HttpPost]
        //public IActionResult Test([FromBody] string status)
        //{
        //    return Ok(status);
        //}

        // http://localhost:5000/api/test/
        // Body (HTTP 404)
        // {"status":"SUCCESS"}
        // Body (HTTP 404)
        // "SUCESS"
        //[HttpPost("{status}")]
        //public IActionResult Test([FromBody] string status)
        //{
        //    return Ok(status);
        //}
    }
}
