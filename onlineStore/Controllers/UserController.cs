using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Areas.Identity.Data;
using OnlineStore.DataAccess;
using StoreLibrary.DataAccess;
using StoreLibrary.Migrations;
using StoreLibrary.Models;

namespace OnlineStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IitemData _itemData;
        private readonly IUserData _userData;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController( IitemData itemData,IUserData userData, UserManager<ApplicationUser> userManager)
        {
      
            _itemData = itemData;
            _userData = userData;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var items =await _userData.GetUserOrders(user.Id);
            items.Reverse();
            return View(items);
        }
    }
}
