using System.ComponentModel.DataAnnotations;

namespace StoreLibrary.Models
{
    public class OrderItemModel
    {

        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }
        public int OrderId { get; set; }
        public float ItemPrice { get; set; }
        public  OrderModel Order { get; set; }
    }
}
