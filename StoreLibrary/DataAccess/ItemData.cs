using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLibrary.DataAccess
{
    public class ItemData : IitemData
    {
        private readonly StoreDataContext _db;
        public ItemData(StoreDataContext db)
        {
            _db = db;
        }

        public async Task<List<ItemModel>> GetAllItems()
        {
            if (_db.Items.Count() != 0)
            {
                var output = await _db.Items.ToListAsync();
                return output;
            }
            else
                return null;
        }

        public async Task<ItemModel> GetItemById(int id)
        {
            var output = await _db.Items.Where(x => x.Id == id).FirstOrDefaultAsync();
            return output;
        }

        public async Task AddItem(ItemModel model)
        {
            await _db.Items.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateItem(ItemModel model)
        {
            var result = await _db.Items.SingleOrDefaultAsync(b => b.Id == model.Id);
            if (result != null)
            {
                result = model;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteItem(int id)
        {
            _db.Items.Remove(_db.Items.Single(x => x.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}
