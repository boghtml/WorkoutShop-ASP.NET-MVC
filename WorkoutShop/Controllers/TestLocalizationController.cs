using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WorkoutShop.Resources;

namespace WorkoutShop.Controllers
{
    public class TestLocalizationController : Controller
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public TestLocalizationController(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentCulture = System.Globalization.CultureInfo.CurrentUICulture.Name;
            ViewBag.HomeText = _localizer["Home"].Value;
            ViewBag.ProductsText = _localizer["Products"].Value;
            ViewBag.CartText = _localizer["Cart"].Value;
            return View();
        }
    }
}
