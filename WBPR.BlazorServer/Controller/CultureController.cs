using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WBPR.BlazorServer.Controller
{
    public class CultureController : ControllerBase
    {
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                        new CookieOptions() { SameSite = SameSiteMode.None, Secure = true });
            }
            return LocalRedirect(redirectUri);
        }
    }
}
