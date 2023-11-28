using Project.BLL.DTOClasses;
using Project.DAL.ContextClasses;
using Project.DAL.Repositories.Abstracts;
using Project.DAL.Repositories.Concretes;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    //Interface segregation ile Middleware'e eklenmesi gerekir...
    public class CategorySimulationManager
    {
        #region Dogrusu
        readonly ICategoryRepository _categoryRepository;
        public CategorySimulationManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        //CategoryRepository _catRep;

        //MyContext _db;
        //public CategorySimulationManager()
        //{
        //    _catRep = new CategoryRepository(_db);
        //}

        //Dikkat ederseniz bir DTO class'ının iş akışı bize hazır gelmedi... İş akışı Manager icerisinde yapıldı. DTO demek size gelen yapının Validation'i gecerlidir artık bunun üzerinde ilgili yerde rahatca iş akışı yapabilirsin demektir...

        //Böylece biz su anda daha iş akışı yapılırken bile bir Veritabanı baglantı teknolojisinin (Entity Framework'un) gücünün bos yere kullanılmasını engelledik...Eger biz burada Domain Entity ile calısıyor olsaydık Entity Framework onu izlemeye baslayacak ve öngöremedigimiz veya mantıksal hatalara yol acabilecek bir durum cıkarma potansiyeline sahip olacaktı...
        public void Add(CategoryDTO categoryDto)
        {
            //Karmasık (mikro servis gibi) sistemde Creation Date'i Entity'den ayırıp DTO'lara aktarmak en profesyonel yaklasımdır...
            categoryDto.CreatedDate = DateTime.Now.AddHours(3);
            Category c = new()
            {
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description,
                CreatedDate = Convert.ToDateTime(categoryDto.CreatedDate)

            };

            _categoryRepository.Add(c);
        }
    }
}
