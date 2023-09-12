using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLibrary.Models
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartModel ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
