using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreLibrary.Models;

namespace StoreLibrary.Data
{
    public class StoreDataContext: DbContext
    {
        public StoreDataContext(DbContextOptions options): base(options) { }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<UserAddressModel> Address { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<ShoppingCartItemModel> ShoppingCartItems { get; set; }
    }
}
