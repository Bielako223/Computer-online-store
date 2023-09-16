using Microsoft.AspNetCore.Mvc;
using StoreLibrary.Models;

namespace OnlineStore.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly ShoppingCartModel _ShoppingCart;
        public Menu(ShoppingCartModel shoppingCartModel)
        {
            _ShoppingCart = shoppingCartModel;
        }

        public IViewComponentResult Invoke()
        {
            var amount = _ShoppingCart.GetShoppingCartItems().Count();

            return View(amount);
        }
    }
}
