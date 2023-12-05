using StoreLibrary.Models;

namespace OnlineStore.DataAccess
{
    public interface IUserData
    {
        Task AddBoughtItemsToUser(List<ShoppingCartItemModel> items, string userId);
    }
}