using StoreLibrary.Models;

namespace StoreLibrary.DataAccess
{
    public interface ICategoryData
    {
        Task<List<CategoryModel>> GetAllCategories();
    }
}