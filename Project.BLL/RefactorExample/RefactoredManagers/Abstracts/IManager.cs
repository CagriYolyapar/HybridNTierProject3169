using Project.BLL.RefactorExample.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.RefactorExample.RefactoredManagers.Abstracts
{
    public interface IManager<T> where T : BaseDTO
    {
    }
}
