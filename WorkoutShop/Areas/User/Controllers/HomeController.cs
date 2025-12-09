using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkoutShop.Models;
using System.Threading.Tasks;
using System.Linq;

namespace WorkoutShop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<Models.User> _userManager;

        public HomeController(UserManager<Models.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string firstName, string lastName, string address, string phoneNumber)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Address = address;
            user.PhoneNumber = phoneNumber;
            user.UpdatedAt = System.DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Profile updated successfully!" });
            }

            return Json(new { success = false, message = "Failed to update profile" });
        }
    }
}
