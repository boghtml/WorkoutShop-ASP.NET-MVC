using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutShop.Controllers
{
    public class LanguageController : Controller
    {
        [AcceptVerbs("GET", "POST")]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions 
                { 
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true,
                    Path = "/"
                }
            );

            return LocalRedirect(returnUrl ?? "/");
        }
    }
}
