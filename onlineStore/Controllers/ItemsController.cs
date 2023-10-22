using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using StoreLibrary.DataAccess;
using StoreLibrary.Models;
using StoreLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Controllers
{
    public  class ItemsController : Controller
    {
        private readonly IitemData _data;
        [ActivatorUtilitiesConstructor]
        public ItemsController(IitemData data)
        {

            _data = data;

        }
        public async Task<IActionResult> Index()
        {
            var items = await _data.GetAllItems();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchingName, int priceMin, int priceMax, int categoryId)
        {
            var searching = searchingName;
            var min = priceMin;
            var max = priceMax;
            var category = categoryId;

            var items = await _data.GetSearchedItems(searching,min,max,category);
            ViewBag.Name = searchingName;
    
            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ItemById = await _data.GetItemById((int)Convert.ToInt64(id));
            if (ItemById == null)
            {
                return NotFound();
            }
            return View(ItemById);
        }

    }
}
