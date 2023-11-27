using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COREMVC.Areas.Admin.Models.Products.PageVMS;
using Project.ENTITIES.Models;

namespace Project.COREMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IProductManager _productManager;
        ICategoryManager _categoryManager;

        public ProductController(ICategoryManager categoryManager, IProductManager productManager)
        {
            _categoryManager = categoryManager;
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            return View(_productManager.GetActives());
        }

        public IActionResult CreateProduct()
        {
            CreateProductPageVM cpVm = new CreateProductPageVM()
            {
                Categories = _categoryManager.GetActives()
            };
            return View(cpVm);
        }


        [HttpPost]
        public IActionResult CreateProduct(Product product,IFormFile formFile)
        {
            Guid uniqueName = Guid.NewGuid();
           
            string extension = Path.GetExtension(formFile.FileName); //dosyanın uzantısını aldık... starwars.jpg
            product.ImagePath = $"/images/{uniqueName}{extension}";
            string path = $"{Directory.GetCurrentDirectory()}/wwwroot{product.ImagePath}";
            FileStream stream = new FileStream(path, FileMode.Create);
            formFile.CopyTo(stream);
            
            _productManager.Add(product);

            return RedirectToAction("Index");
            
        }
    }
}
