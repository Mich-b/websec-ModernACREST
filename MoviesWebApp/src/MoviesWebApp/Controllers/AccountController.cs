using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesLibrary;
using MoviesWebApp.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace MoviesWebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private MovieIdentityService _identityService;

        public AccountController(MovieIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? "/"
            }, "oidc");

            //return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpGet]
        public IActionResult LoginSaml(string returnUrl)
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? "/"
            }, "Saml2");

            //return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // NOTICE: this was commented out since it's unnecessary now
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (model.Username == model.Password)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim("sub", model.Username)
        //        };
        //        claims.AddRange(_identityService.GetClaimsForUser(model.Username));

        //        var ci = new ClaimsIdentity(claims, "password", "name", "role");
        //        var cp = new ClaimsPrincipal(ci);

        //        await HttpContext.SignInAsync("Cookies", cp);

        //        if (model.ReturnUrl != null)
        //        {
        //            return LocalRedirect(model.ReturnUrl);
        //        }

        //        return RedirectToAction("Index", "Home");
        //    }

        //    ModelState.AddModelError("", "Invalid username or password");
        //    return View();
        //}


        public ActionResult Logout()
        {
            //no sign out for SAML implemented, relying on cookie logout. This DOES NOT LOG THE USER OUT FROM THE IDP, in contrast to the oidc implementation
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = "/"
            }, "oidc", "Cookies");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
