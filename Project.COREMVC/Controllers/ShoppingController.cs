using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COREMVC.Models.PageVms;
using Project.COREMVC.Models.SessionService;
using Project.COREMVC.Models.ShoppingTools;
using Project.ENTITIES.Models;
using X.PagedList;

namespace Project.COREMVC.Controllers
{
    public class ShoppingController : Controller
    {
        readonly IProductManager _productManager;
        readonly ICategoryManager _categoryManager;

        public ShoppingController(IProductManager productManager, ICategoryManager categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }
        public IActionResult Index(int? page,int? categoryID)
        {
            //string a = "Cagri";
            //string b = a ?? "Deneme";
            ShoppingPageVM spVm = new ShoppingPageVM()
            {
                Products = categoryID == null ? _productManager.GetActives().ToPagedList(page ?? 1,5) : _productManager.Where(x=>x.CategoryID == categoryID).ToPagedList(page ?? 1 , 5),

                Categories = _categoryManager.GetActives()
            };

            if (categoryID != null) TempData["catID"] = categoryID;
          

            return View(spVm);
        }


        public async Task<IActionResult> AddToCart(int id)
        {
            Cart c = GetCartFromSession("scart") == null ? new Cart() : GetCartFromSession("scart");
            Product productToBeAdded = await _productManager.FindAsync(id);
            CartItem ci = new()
            {
                ID = productToBeAdded.ID,
                ProductName = productToBeAdded.ProductName,
                UnitPrice = productToBeAdded.UnitPrice,
                ImagePath = productToBeAdded.ImagePath,
                CategoryName = productToBeAdded.Category.CategoryName,
                CategoryID = productToBeAdded.CategoryID
            };
            c.AddToCart(ci);

            SetCart(c);

            TempData["message"] = $"{ci.ProductName} isimli ürün sepete eklenmiştir";
            return RedirectToAction("Index");
        }

        private void SetCart(Cart c)
        {
            HttpContext.Session.SetObject("scart", c);
        }

        public IActionResult CartPage()
        {
            if (GetCartFromSession("scart") == null)
            {
                TempData["message"] = "Sepetiniz su anda bos";
                return RedirectToAction("Index");
            }
            Cart c = HttpContext.Session.GetObject<Cart>("scart");
            return View(c); //Todo : PageVM refactoring'i unutmayın
        }

        public IActionResult DeleteFromCart(int id)
        {
            if (GetCartFromSession("scart") != null)
            {
                Cart c = GetCartFromSession("scart");
                c.RemoveFromCart(id);
                SetCart(c); 
               
                if (c.GetCartItems.Count == 0)
                {
                    HttpContext.Session.Remove("scart");
                    TempData["message"] = "Sepetinizdeki tüm ürünler cıkarılmıstır";
                    return RedirectToAction("Index");
                }

              
            }

            return RedirectToAction("CartPage");
        }



        Cart GetCartFromSession(string key)
        {
            return HttpContext.Session.GetObject<Cart>(key);
        }
    }
}
