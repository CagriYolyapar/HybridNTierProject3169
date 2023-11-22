using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    //Todo : AppUserManager Encapsulation
    public interface IAppUserManager : IManager<AppUser>
    {
        Task<bool> AddUser(AppUser user);
    }
}
