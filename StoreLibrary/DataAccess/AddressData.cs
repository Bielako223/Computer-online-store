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
    public class AddressData
    {
        private readonly StoreDataContext _db;
        public AddressData(StoreDataContext db)
        {
            _db = db;
        }

        public async Task<UserAddressModel> GetAddressById(int id)
        {
            var output = await _db.Address.Where(x => x.Id == id).FirstOrDefaultAsync();
            return output;
        }

        public async Task AddAddress(UserAddressModel model)
        {
            await _db.Address.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAddress(UserAddressModel model)
        {
            var result = await _db.Address.SingleOrDefaultAsync(b => b.Id == model.Id);
            if (result != null)
            {
                result = model;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAddress(int id)
        {
            _db.Address.Remove(_db.Address.Single(x => x.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}
