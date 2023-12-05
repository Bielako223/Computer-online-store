using Microsoft.AspNetCore.Mvc;
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
        [BindProperty]
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
            string filteringString = JsonConvert.SerializeObject(_filtering);
            _contx.HttpContext.Session.SetString("filter", "");
        }
        public async Task<IActionResult> Index()
        {
            SearchingModel sessionFiltering = new SearchingModel();
            var zse = _contx.HttpContext.Session.GetString("filter").ToString();

            if (_contx.HttpContext.Session.GetString("filter").IsNullOrEmpty())
            {
                var _items = await _data.GetAllItems();
                ViewBag.min = 0;
                ViewBag.max = 3000;
                ViewBag.category = 0;
                string filteringString = JsonConvert.SerializeObject(new SearchingModel { Name = "", Min = 0, Max = 3000, Category = 0 });
                _contx.HttpContext.Session.SetString("filter", filteringString);
                zse = _contx.HttpContext.Session.GetString("filter").ToString();
                return View(_items);
            }
            else
            {
                if (_filtering !=null)
                {
                    string filteringString = JsonConvert.SerializeObject(_filtering);
                    _contx.HttpContext.Session.SetString("filter", filteringString);
                }
                string filteringJson = _contx.HttpContext.Session.GetString("filter");
                SearchingModel searchingModel = JsonConvert.DeserializeObject<SearchingModel>(filteringJson);
                var _items = await _data.GetSearchedItems(searchingModel);
                ViewBag.Name = _filtering.Name;
                ViewBag.min = _filtering.Min;
                ViewBag.max = _filtering.Max;
                ViewBag.category = _filtering.Category;
                return View(_items);
            }

            
        }



        //public async Task<IActionResult> Index(Object str)
        //{
        //    str
        //    var items = await _data.GetAllItems();
        //    ViewBag.min = 0;
        //    ViewBag.max = 3000;
        //    ViewBag.category = 0;
        //    return View(items);
        //}

        /* [HttpPost]
         public async Task<IActionResult> Index(string searchingName="", int priceMin=0, int priceMax=3000, int categoryId = 0)
         {
             var searching = searchingName;
             var min = priceMin;
             var max = priceMax;
             var category = categoryId;

             _items = await _data.GetSearchedItems(searching,min,max,category);


        eturn View(_items);
         }
         */
   
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
