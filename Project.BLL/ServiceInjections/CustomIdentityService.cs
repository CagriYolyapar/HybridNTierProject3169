using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL.ContextClasses;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjections
{
    public static class CustomIdentityService
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            //services.AddIdentityCore<AppUser>(x =>
            //{

            //}).AddRoleManager<IdentityRole<int>>().AddRoles<IdentityRole<int>>().AddEntityFrameworkStores<MyContext>();

            services.AddIdentity<AppUser, IdentityRole<int>>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequiredLength = 2;
                x.SignIn.RequireConfirmedEmail = true;
                x.Password.RequireNonAlphanumeric = false;


            }).AddEntityFrameworkStores<MyContext>();

            return services;

        }
    }
}
