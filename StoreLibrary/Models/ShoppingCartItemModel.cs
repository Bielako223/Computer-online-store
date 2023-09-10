using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreLibrary.Models
{
    public class ShoppingCartItemModel
    {
        [Key]
        public int ShoppingCartItemId { get; set; }
        public ItemModel Item { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
