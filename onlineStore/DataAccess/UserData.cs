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
                    var itemById = await _itemData.GetItemById(item.Item.Id);
                    itemById.Quantity-=item.Amount;
                    await _itemData.UpdateItem(itemById);
                }
                var user =await  GetUser(userId); 
                if (user != null)
                {
                    var address = new AddressModel()
                    {
                        CartId = items.FirstOrDefault().ShoppingCartId,
                        City = user.City,
                        Street = user.Street,
                        HouseNumber = user.HouseNumber,
                        Zipcode = user.Zipcode

                    };
                    await _db.AddressesForOrders.AddAsync(address);
                    await _db.SaveChangesAsync();
                }
               
                _contx.HttpContext.Session.SetString("CartId", Guid.NewGuid().ToString());

            }
            catch (Exception ex)
            {

            }
        }
        public async Task<List<OrderandAddressViewModel>> GetUserOrders(string userId)
        {
            try
            {
                var items = await _db.Orders.Where(x => x.User == userId).ToListAsync();
                if (items.Count != 0)
                {

                    var groupedOrdersList = items
    .GroupBy(u => u.ShoppingCartId)
    .Select(grp => grp.ToList())
    .ToList();
                    var returnList = new List<OrderandAddressViewModel>();
                    foreach (var item in groupedOrdersList)
                    {
                        var list = new List<OrdersViewModel>();
                        foreach (var it in item)
                        {
                            var tempItem = new OrdersViewModel();
                            tempItem.OrderModel = it;
                            tempItem.ItemModel = await _itemData.GetItemById(it.ItemId);
                            list.Add(tempItem);
                        }
                        var shoppingCartid = list.FirstOrDefault().OrderModel.ShoppingCartId;
                        var orderAndAddress = new OrderandAddressViewModel() { 
                        ListOfOrders= list,
                        AddressforOrder=await _db.AddressesForOrders.Where(x=> x.CartId== shoppingCartid).FirstOrDefaultAsync()
                    };
                        returnList.Add(orderAndAddress);

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
