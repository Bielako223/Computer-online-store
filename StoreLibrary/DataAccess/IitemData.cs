using StoreLibrary.Models;

namespace StoreLibrary.DataAccess
{
    public interface IitemData
    {
        Task AddItem(ItemModel model);
        Task DeleteItem(int id);
        Task<List<ItemModel>> GetAllItems();
        Task<List<ItemModel>> GetSearchedItems(string searchingName, int priceMin, int priceMax, int categoryId);
        Task<ItemModel> GetItemById(int id);
        Task UpdateItem(ItemModel model);
    }
}