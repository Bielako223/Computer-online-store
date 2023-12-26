using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLibrary.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string ShoppingCartId { get; set; }
        public string User { get; set; }
        public float Total { get; set; }
        public  AddressModel Address { get; set; }
        public  List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();

    }
}
