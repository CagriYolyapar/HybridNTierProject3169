using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.BLL.ManagerServices.Concretes;
using Project.ENTITIES.Models;

namespace Project.COREMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        readonly ICategoryManager _categoryManager;
        //CategorySimulationManager _catSimMan;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
            //_catSimMan = new CategorySimulationManager();
        }

        //Refactor Domain Entity'leri düzenleyin

        //Onemli : Tüm Refactor'lerde(Domain Entity'leri iceren tüm Refactorlerde) Refactoring'i su yasam döngüsüne göre yapın : VM(PageVM/Request,ResponseModel vs) => DTO(Katmanlar arası haberlesebileceginiz tek model) => Domain Entity

        // CategoryDTO , Admin Models Categories ResponseModels CategoryResponseModel CategoryResponsePageVM (CategoryResponsePageVM icerisinde bir List<CategoryResponseModel> tipinde property tasır)... Siz BLL'deki Manager'larınızı da parametre olarak DTO tipinde yapılar almakla göreblendirmelisiniz...


        public IActionResult Index()
        {
            return View(_categoryManager.GetAll());
        }


        public IActionResult CreateCategory()
        {
            return View();
        }

        //Burada normal şartlar altında CreateCategoryRequestModel alırsınız.. Aldıgınız MOdel'in Validation kurallarına uydugundan tamamen emin olmalısınız....Ondan sonra CreateCategoryRequestModel burada CategoryDTO'ya maplenir...Ve CategorySimulationManager icerisindeki Add cagrılarak DTO class'ı verilir...

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category model)
        {
            await _categoryManager.AddAsync(model);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteCategory(int id)
        {
            _categoryManager.Delete(await _categoryManager.FindAsync(id));
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DestroyCategory(int id)
        {
            TempData["Message"] = _categoryManager.Destroy(await _categoryManager.FindAsync(id));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCategory(int id)
        {
            return View(await _categoryManager.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Category model)
        {
            await _categoryManager.UpdateAsync(model);
            return RedirectToAction("Index");
        }
    }
}
