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

        private readonly IitemData _data;
        private readonly ICategoryData _category;
        [ActivatorUtilitiesConstructor]
        public ItemsController(IitemData data, IHttpContextAccessor contx, ICategoryData category)
        {
            _category = category;
            _data = data;
            _contx = contx;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _category.GetAllCategories();

            if (_contx.HttpContext.Session.GetString("filter").IsNullOrEmpty())
            {
                var _items = await _data.GetAllItems();
                ViewBag.min = 0;
                ViewBag.max = 3000;
                ViewBag.category = 0;
                string filteringString = JsonConvert.SerializeObject(new SearchingModel { Name = null, Min = 0, Max = 3000, Category = 0 });
                _contx.HttpContext.Session.SetString("filter", filteringString);
                return View(Tuple.Create(_items,categories));
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
                _filtering = searchingModel;
                ViewBag.Name = _filtering.Name;
                ViewBag.min = _filtering.Min;
                ViewBag.max = _filtering.Max;
                ViewBag.category = _filtering.Category;
                return View(Tuple.Create(_items, categories));
            }

            
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
            var details =ItemById.Details.Split("/t").ToList();
            return View(Tuple.Create(ItemById, details));
        }

        public async Task<IActionResult> GetImage(int id)
        {
            // Znalezienie elementu z odpowiednim Id
            var item = await _data.GetItemById(id);

            // Sprawdzenie czy obraz istnieje
            if (item == null || item.ImgData == null)
            {
                return NotFound();  // Możesz zwrócić placeholder w przypadku braku obrazu
            }

            // Zwrócenie obrazu w formie pliku
            return File(item.ImgData, "image/jpeg");  // Upewnij się, że typ MIME odpowiada typowi obrazu (jpeg, png itd.)
        }

    }
}
