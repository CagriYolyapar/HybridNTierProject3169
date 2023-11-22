using Microsoft.Extensions.DependencyInjection;
using Project.BLL.ManagerServices.Abstracts;
using Project.BLL.ManagerServices.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjections
{
    public static class ManagerService
    {
        public static IServiceCollection AddManagerServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IManager<>),typeof(BaseManager<>));

            services.AddScoped<IProductManager,ProductManager>();
            services.AddScoped<ICategoryManager,CategoryManager>();
            services.AddScoped<IOrderDetailManager,OrderDetailManager>();   
            services.AddScoped<IOrderManager,OrderManager>();
            services.AddScoped<IProfileManager,ProfileManager>();
            services.AddScoped<IAppUserManager,AppUserManager>();

            return services;
        }
    }
}
