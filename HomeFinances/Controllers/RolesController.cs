using HomeFinances.Data;
using HomeFinances.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeFinances.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RolesController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
                return Content("Nadano rolę Administratora dla " + user.UserName);
            }
            return Content("Błąd użytkownika");
        }
    }
}