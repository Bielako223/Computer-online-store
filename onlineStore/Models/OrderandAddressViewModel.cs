using StoreLibrary.Models;

namespace OnlineStore.Models
{
    public class OrderandAddressViewModel
    {
        public List<OrdersViewModel> ListOfOrders { get; set; }
        public AddressModel AddressforOrder { get; set; }
    }
}
