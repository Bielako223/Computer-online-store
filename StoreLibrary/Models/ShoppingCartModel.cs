using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StoreLibrary.Models
{
    public class ShoppingCartModel
    {
        private readonly StoreDataContext _db;
        public ShoppingCartModel(StoreDataContext db)
        {
            _db = db;
        }

        public static ShoppingCartModel GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<StoreDataContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCartModel(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(ItemModel item, int amount)
        {
            var shoppingCartItem = _db.ShoppingCartItems.SingleOrDefault(s => s.Item.Id == item.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItemModel
                {
                    ShoppingCartId = ShoppingCartId,
                    Item = item,
                    Amount = 1
                };

                _db.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _db.SaveChanges();
        }

        public int RemoveFromCart(ItemModel item)
        {
            var shoppingCartItem = _db.ShoppingCartItems.SingleOrDefault(s => s.Item.Id == item.Id && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount>1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _db.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _db.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItemModel> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _db.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Item).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _db.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _db.ShoppingCartItems.RemoveRange(cartItems);
            _db.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _db.ShoppingCartItems.Where(c=>c.ShoppingCartId == ShoppingCartId).Select(c=>c.Item.Price * c.Amount).Sum();
            return total;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItemModel> ShoppingCartItems { get; set; }
    }
}
