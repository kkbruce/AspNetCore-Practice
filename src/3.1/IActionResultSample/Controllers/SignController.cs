using Microsoft.AspNetCore.Mvc;

namespace IActionResultSample.Controllers
{
    [Route("Sign/[action]")]
    [ApiController]
    public class SignController : ControllerBase
    {
        // Todo: MVC?
        //SignIn(ClaimsPrincipal principal, string authenticationScheme)
        //SignIn(ClaimsPrincipal principal, AuthenticationProperties properties, string authenticationScheme)
        //SignOut(params string[] authenticationSchemes)
        //SignOut(AuthenticationProperties properties, params string[] authenticationSchemes)
    }
}