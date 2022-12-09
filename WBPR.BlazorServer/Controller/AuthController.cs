using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WBPR.Base.Config;

namespace WBPR.BlazorServer.Controller
{
    public class AuthController : ControllerBase
    {
        [Route("[controller]/login")]
        [HttpGet]
        public async Task<ActionResult> Login()
        {
            var props = new AuthenticationProperties
            { RedirectUri = Url.Action("MsSignIn", "Auth") };
            return Challenge(props, "Microsoft");
        }

        [Route("[controller]/logout")]
        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [Authorize(AuthenticationSchemes = "Microsoft")]
        [Route("[controller]/signin-microsoft")]
        public IActionResult MsSignIn()
        {
            string email = User.FindFirst(ClaimTypes.Email).Value;
            string username = User.Identity.Name;
            return RedirectToAction("/fetchdata");
        }
    }
}
