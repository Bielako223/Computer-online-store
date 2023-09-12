using Microsoft.AspNetCore.Mvc;
using StoreLibrary.DataAccess;
using StoreLibrary.Migrations;
using StoreLibrary.Models;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingCartModel _ShoppingCart;
        private readonly IitemData _itemData;

        public CartController(ShoppingCartModel shoppingCartModel, IitemData itemData)
        {
            _ShoppingCart= shoppingCartModel;   
            _itemData= itemData;
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
                _ShoppingCart.AddToCart(item,1);
            }

            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromCart(int itemId)
        {
            var item = await _itemData.GetItemById(itemId);
            if (item != null)
            {
                _ShoppingCart.RemoveFromCart(item);
            }

            return RedirectToAction("Index");
        }
    }
}
