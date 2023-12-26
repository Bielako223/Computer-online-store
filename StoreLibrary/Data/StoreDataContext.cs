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
        public StoreDataContext(DbContextOptions<StoreDataContext> options): base(options) { }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<ShoppingCartItemModel> ShoppingCartItems { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<AddressModel> Address { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>()
                .HasOne<AddressModel>(p => p.Address)
                .WithOne(pp => pp.Order)
                .HasForeignKey<AddressModel>(pp => pp.OrderId);
            modelBuilder.Entity<OrderModel>()
                .HasMany<OrderItemModel>(p => p.Items)
                .WithOne(pp => pp.Order)
                .HasForeignKey(pp => pp.OrderId)
                .IsRequired();
        }
    }
}
