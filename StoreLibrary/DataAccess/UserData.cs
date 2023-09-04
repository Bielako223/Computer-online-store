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
    public class UserData
    {
        private readonly StoreDataContext _db;
        public UserData(StoreDataContext db)
        {
            _db = db;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var output = await _db.User.Where(x => x.Id == id).FirstOrDefaultAsync();
            return output;
        }

        public async Task CreateUser(UserModel model)
        {
            await _db.User.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUser(UserModel model)
        {
            var result = await _db.User.SingleOrDefaultAsync(b => b.Id == model.Id);
            if (result != null)
            {
                result = model;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(int id)
        {
            _db.User.Remove(_db.User.Single(x => x.Id == id));
            await _db.SaveChangesAsync();
        }

        
    }
}
