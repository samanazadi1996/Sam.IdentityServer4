using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Services.WebMvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LogIn() =>
            Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            });

        public IActionResult LogOut()
        {
            //await HttpContext.SignOutAsync("Cookies", "oidc");

            return SignOut("Cookies", "oidc");
        }
    }
}
