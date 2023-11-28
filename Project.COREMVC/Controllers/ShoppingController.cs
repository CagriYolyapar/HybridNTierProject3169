using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COREMVC.Models.PaveVms;
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
    }
}
