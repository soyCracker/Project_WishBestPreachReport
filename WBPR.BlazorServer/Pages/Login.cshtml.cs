using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WBPR.BlazorServer.Pages
{
    public class LoginModel : PageModel
    {
        public async Task OnGetAsync(string redirectUri)
        {
            await HttpContext.ChallengeAsync("Azure AD / Microsoft", new AuthenticationProperties { RedirectUri = redirectUri });
        }
    }
}
