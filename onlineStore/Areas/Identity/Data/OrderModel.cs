using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Areas.Identity.Data
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
