using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WBPR.BlazorServer.Pages
{
    public class LoginModel : PageModel
    {
        public async Task OnGetAsync(string redirectUri)
        {
            //await HttpContext.ChallengeAsync("Microsoft", new AuthenticationProperties { RedirectUri = redirectUri });
            await HttpContext.ChallengeAsync("OpenIdConnect", new AuthenticationProperties { RedirectUri = redirectUri });
        }
    }
}
