using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLibrary.Models
{
    public class ItemModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Price { get; set; }
        public int Discount { get; set; } = 0;
        public string Details { get; set; }
        public string ImgPath { get; set; }
        public int Quantity { get; set; }
        public  CategoryModel Category { get; set; } = new();

    }
}
