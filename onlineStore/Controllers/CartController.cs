using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.DataAccess;
using StoreLibrary.DataAccess;
using StoreLibrary.Migrations;
using StoreLibrary.Models;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingCartModel _ShoppingCart;
        private readonly IitemData _itemData;
        private readonly IUserData _userData;
        private readonly UserManager<IdentityUser> _userManager;


        public CartController(ShoppingCartModel shoppingCartModel, IitemData itemData,IUserData userData, UserManager<IdentityUser> userManager)
        {
            _ShoppingCart= shoppingCartModel;   
            _itemData= itemData;
            _userData= userData;
            _userManager = userManager;
        }
        public ViewResult Index()
        {
            var items = _ShoppingCart.GetShoppingCartItems();
            _ShoppingCart.ShoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _ShoppingCart,
                ShoppingCartTotal = _ShoppingCart.GetShoppingCartTotal()
            };

            return View(sCVM);
        }

        public async Task<RedirectToActionResult> AddToCart(int itemId)
        {
           var item = await _itemData.GetItemById(itemId);
            if (item !=null)
            {
                if(_ShoppingCart.GetShoppingCartItems().Any(x=> x.Item==item)) { 
                    var itemfromCart = _ShoppingCart.GetShoppingCartItems().FirstOrDefault(x=>x.Item==item);
                    if (itemfromCart.Amount<item.Quantity)
                    {
                        _ShoppingCart.AddToCart(item, 1);
                    };
                }
                else
                {
                    _ShoppingCart.AddToCart(item, 1);
                }
               
            }

            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromCart(int itemId)
        {
            var item = await _itemData.GetItemById(itemId);
            if (item != null)
            {
                _ShoppingCart.RemoveTotalFromCart(item);
            }

            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveOneItemFromCart(int itemId)
        {
            var item = await _itemData.GetItemById(itemId);
            if (item != null)
            {
                _ShoppingCart.RemoveFromCart(item);
            }

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<ViewResult> Order()
        {
            var items = _ShoppingCart.GetShoppingCartItems();
            _ShoppingCart.ShoppingCartItems = items;

            var sCVM = new ShoppingCartViewModel
            {
                ShoppingCart = _ShoppingCart,
                ShoppingCartTotal = _ShoppingCart.GetShoppingCartTotal()
            };
            ViewBag.total=_ShoppingCart.GetShoppingCartTotal();
            return View(sCVM);
        }

        [Authorize]
        public async Task<RedirectToActionResult> Buy()
        {
            try
            {
                var items = _ShoppingCart.GetShoppingCartItems();
                var User= await _userManager.GetUserAsync(HttpContext.User);
                    

                await _userData.AddBoughtItemsToUser(items, User.Id);

                

            }catch (Exception ex)
            {

            }
            return RedirectToAction("OrderSummary");
        }

        public ViewResult OrderSummary()
        {
            return View();
        }
    }
}
