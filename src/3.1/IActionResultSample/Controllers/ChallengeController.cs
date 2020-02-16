using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("Challenge/[action]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        /// <summary>
        /// Will throw exception, need more startup.cs config
        /// </summary>
        /// <returns></returns>
        public IActionResult Demo1()
        {
            return Challenge();
        }

        // ToDo: MVC?
        // Challenge()
        // Challenge(params string[] authenticationSchemes)
        // Challenge(AuthenticationProperties properties)
        // Challenge(AuthenticationProperties properties, params string[] authenticationSchemes)
    }
}