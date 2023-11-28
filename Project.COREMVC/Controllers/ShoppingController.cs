using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
