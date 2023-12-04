using Project.BLL.RefactorExample.RefactoredManagers.Abstracts;
using Project.BLL.RefactorExample.RefactoredManagers.Concretes;
using Project.BLL.RefactorExample.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.RefactorExample.RefactoredManagers.Concretes
{
    public class CategoryManager : BaseManager<CategoryDTO>,ICategoryManager
    {
        //Metot implementation
        //Metot DTO olarak aldıgı veriyi Domain Entity'e mapleyerek REpository kullanır ve maplenen Domain Entity'i ilgili repository metoduna argüman olarak verir...

    }
}
