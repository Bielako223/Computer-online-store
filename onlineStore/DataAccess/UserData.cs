using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Areas.Identity.Data;
using StoreLibrary.Data;
using StoreLibrary.Models;
using OnlineStore.Data;
using StoreLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using StoreLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.DataAccess
{
    public class UserData : IUserData
    {
        private readonly StoreDataContext _db;
        private readonly UserContext _db2;

        private readonly IitemData _itemData;
        private readonly IHttpContextAccessor _contx;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserData(StoreDataContext db,UserContext db2, IitemData itemData, IHttpContextAccessor contx,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _db2 = db2;
            _db = db;
            _itemData = itemData;
            _contx = contx;
        }

        public async Task AddBoughtItemsToUser(List<ShoppingCartItemModel> items, string userId, float total)
        {
            try
            {
                var user = await GetUser(userId);
                if (user != null)
                {
                    var orderItems = new List<OrderItemModel>();
                    var address = new AddressModel()
                    {
                        City = user.City,
                        Street = user.Street,
                        HouseNumber = user.HouseNumber,
                        Zipcode = user.Zipcode

                    };
                    foreach (var item in items)
                    {

                        var itemModel = new OrderItemModel()
                        {
                            ItemId = item.Item.Id,
                            Amount = item.Amount,
                            ItemPrice = item.Item.Price
                        };
                        orderItems.Add(itemModel);
                        var itemById = await _itemData.GetItemById(item.Item.Id);
                        itemById.Quantity -= item.Amount;
                        await _itemData.UpdateItem(itemById);
                    }
                    var OrderModel = new OrderModel()
                    {
                        ShoppingCartId = items.FirstOrDefault().ShoppingCartId,
                        User = userId,
                        Address = address,
                        Items= orderItems,
                        Total = total

                    };

                    await _db.Orders.AddAsync(OrderModel);
                    await _db.SaveChangesAsync();
                    
                    _contx.HttpContext.Session.SetString("CartId", Guid.NewGuid().ToString());
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<List<OrdersViewModel>> GetUserOrders(string userId)
        {
            try
            {
                var items = await _db.Orders.Where(x => x.User == userId).Include(req=>req.Items).Include(req => req.Address).ToListAsync();
                if (items.Count != 0)
                {
                    var returnList = new List<OrdersViewModel>();
                    foreach (var item in items)
                    {
                        var list = new List<ItemModel>();
                        foreach(var itemModel in item.Items)
                        {
                            var itemId = itemModel.ItemId;
                            var itemById = await _itemData.GetItemById(itemId);
                            list.Add(itemById);
                        }
                        var orderView = new OrdersViewModel()
                        {
                            OrderModel = item,
                            ItemModels = list
                        };
                        returnList.Add(orderView);

                    }
                    return returnList;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
        public async Task<ApplicationUser> GetUser(string userId)
        {
            var user = await _db2.Users.Where(x=> x.Id == userId).FirstOrDefaultAsync();
            return user;
        }
    }
}
