using CruiseShipManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace CruiseShipManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> TestPassword()
        {
            var admin = await _userManager.FindByEmailAsync("admin@example.com");
            if (admin == null)
            {
                return Content("Admin user not found.");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(admin, "Admin@123");
            if (isPasswordCorrect)
            {
                return Content("Password is correct.");
            }
            else
            {
                return Content("Password is incorrect.");
            }
        }
    }
}