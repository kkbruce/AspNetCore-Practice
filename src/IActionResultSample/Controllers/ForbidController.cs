using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("Forbid/[action]")]
    [ApiController]
    public class ForbidController : ControllerBase
    {
        /// <summary>
        /// Will throw exception, need more startup.cs config
        /// </summary>
        /// <returns></returns>
        public IActionResult Demo1()
        {
            return Forbid();
        }

        // Todo: MVC?
        //Forbid()
        //Forbid(params string[] authenticationSchemes)
        //Forbid(AuthenticationProperties properties)
        //Forbid(AuthenticationProperties properties, params string[] authenticationSchemes)

    }
}