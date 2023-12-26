using StoreLibrary.Models;

namespace OnlineStore.Models
{
    public class OrdersViewModel
    {
        public OrderModel OrderModel { get; set; }
        public List<ItemModel> ItemModels { get; set; }
    }
}
