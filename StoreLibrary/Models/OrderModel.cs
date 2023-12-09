using System.ComponentModel.DataAnnotations;

namespace StoreLibrary.Models
{
    public class OrderModel
    {

        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public string User { get; set; }
    }
}
