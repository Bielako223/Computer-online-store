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
    public class CategoryData
    {
        private readonly StoreDataContext _db;
        public CategoryData(StoreDataContext db)
        {
            _db = db;
        }
        public async Task<List<CategoryModel>> GetAllCategories()
        {
            if (_db.Category.Count() != 0)
            {
                var output = await _db.Category.ToListAsync();
                return output;
            }
            else
                return null;
        }
    }
}
