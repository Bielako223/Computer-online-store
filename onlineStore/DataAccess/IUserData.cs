using OnlineStore.Areas.Identity.Data;
using OnlineStore.Models;
using StoreLibrary.Models;

namespace OnlineStore.DataAccess
{
    public interface IUserData
    {
        Task AddBoughtItemsToUser(List<ShoppingCartItemModel> items, string userId);
        Task<List<OrderandAddressViewModel>> GetUserOrders(string userId);
        Task<ApplicationUser> GetUser(string userId);
    }
}