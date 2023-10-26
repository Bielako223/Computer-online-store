﻿using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using StoreLibrary.DataAccess;
using StoreLibrary.Models;
using StoreLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;

namespace OnlineStore.Controllers
{
    public  class ItemsController : Controller
    {
        public SearchingModel _filtering { get; set; }
        private readonly IHttpContextAccessor _contx;
        private readonly ILogger<ItemsController> _logger;  
        private readonly IitemData _data;


        [ActivatorUtilitiesConstructor]
        public ItemsController(IitemData data, IHttpContextAccessor contx, ILogger<ItemsController> logger)
        {
            _data = data;
            _contx = contx;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            string JsonFiltering = _contx.HttpContext.Session.GetString("filter");

            if (JsonFiltering.IsNullOrEmpty())
            {
                var _items = await _data.GetAllItems();
                ViewBag.min = 0;
                ViewBag.max = 3000;
                ViewBag.category = 0;
                string filteringString = JsonConvert.SerializeObject(new SearchingModel { Name = null, Min = 0, Max = 3000, Category = 0 });
                _contx.HttpContext.Session.SetString("filter", filteringString);
                return View(_items);
            }
            else
            {
                if (_filtering !=null)
                {
                    string filteringString = JsonConvert.SerializeObject(_filtering);
                    _contx.HttpContext.Session.SetString("filter", filteringString);
                }
                JsonFiltering = _contx.HttpContext.Session.GetString("filter");
                _filtering = JsonConvert.DeserializeObject<SearchingModel>(JsonFiltering);
                var _items = await _data.GetSearchedItems(_filtering);
                ViewBag.Name = _filtering.Name;
                ViewBag.min = _filtering.Min;
                ViewBag.max = _filtering.Max;
                ViewBag.category = _filtering.Category;
                return View(_items);
            }

            
        }


        [HttpPost]
        public async Task<IActionResult> Index(string Name = "", int Min = 0, int Max = 3000, int Category = 0)
        {
            _filtering.Name= Name;
            _filtering.Min = Min;
            _filtering.Max = Max;
            _filtering.Category = Category;
            ViewBag.Name = _filtering.Name;
            ViewBag.min = _filtering.Min;
            ViewBag.max = _filtering.Max;
            ViewBag.category = _filtering.Category;
            string filteringString = JsonConvert.SerializeObject(_filtering);
            _contx.HttpContext.Session.SetString("filter", filteringString);
            string filteringJson = _contx.HttpContext.Session.GetString("filter");
            var _items = await _data.GetSearchedItems(_filtering);


            return View(_items);
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
