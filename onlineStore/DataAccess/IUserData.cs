using OnlineStore.Areas.Identity.Data;
using OnlineStore.Models;
using StoreLibrary.Models;

namespace OnlineStore.DataAccess
{
    public interface IUserData
    {
        Task AddBoughtItemsToUser(List<ShoppingCartItemModel> items, string userId, float total);
        Task<List<OrdersViewModel>> GetUserOrders(string userId);
        Task<ApplicationUser> GetUser(string userId);
    }
}