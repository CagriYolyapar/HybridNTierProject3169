using Project.BLL.RefactorExample.DTOClasses;
using Project.BLL.RefactorExample.RefactoredManagers.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.RefactorExample.RefactoredManagers.Concretes
{
    public class BaseManager<T> : IManager<T> where T : BaseDTO
    {
    }
}
