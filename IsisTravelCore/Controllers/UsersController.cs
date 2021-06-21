using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsisTravelCore.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IsisTravelCore.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userbyType = await _userManager.GetUsersInRoleAsync("CompanyUser");
            return View(userbyType);
        }
    }
}