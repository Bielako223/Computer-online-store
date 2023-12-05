using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Areas.Identity.Data;
using StoreLibrary.Data;
using StoreLibrary.Models;
using OnlineStore.Data;
using StoreLibrary.DataAccess;

namespace OnlineStore.DataAccess
{
    public class UserData : IUserData
    {
        private readonly UserContext _db;

        private readonly IitemData _itemData;
        private readonly IHttpContextAccessor _contx;
        public UserData(UserContext db, IitemData itemData, IHttpContextAccessor contx)
        {
            _db = db;
            _itemData = itemData;
            _contx = contx;
        }

        public async Task AddBoughtItemsToUser(List<ShoppingCartItemModel> items, string userId)
        {
            try
            {
                foreach (var item in items)
                {
                    var itemModel = new OrderModel()
                    {
                        ItemId = item.Item.Id,
                        Amount = item.Amount,
                        ShoppingCartId = item.ShoppingCartId,
                        User = userId
                    };
                    await _db.Orders.AddAsync(itemModel);
                    await _db.SaveChangesAsync();

                    for (int i = 0; i < items.Count; i++)
                    {
                        var itemById = await _itemData.GetItemById(item.Item.Id);
                        if (itemById.Quantity > 0)
                        {
                            itemById.Quantity -= 1;
                            await _itemData.UpdateItem(itemById);

                        }
                    }
                }
                _contx.HttpContext.Session.SetString("CartId", Guid.NewGuid().ToString());

            }
            catch (Exception ex)
            {

            }
        }
    }
}
