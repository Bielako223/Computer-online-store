using Microsoft.AspNetCore.Mvc;
using StoreLibrary.Models;

namespace OnlineStore.Controllers
{
    public class MenuController : Controller
    {
        private readonly ShoppingCartModel _ShoppingCart;
        public MenuController(ShoppingCartModel shoppingCartModel)
        {
            _ShoppingCart= shoppingCartModel;
        }
        public PartialViewResult Menu()
        { 
            var amount = _ShoppingCart.GetShoppingCartTotal();
            return PartialView(amount);
        }
    }
}
