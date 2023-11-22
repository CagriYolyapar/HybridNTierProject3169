using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Repositories.Abstracts;
using Project.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjections
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepServices(this IServiceCollection services)
        {
            /*
             
             Transient: Bir Request'in ulastığı Scope'un parametre kümesinde aynı tipten kac tane varsa o kadar instance alınır...Manager ve Repository'ler icin anlamsızdır...CÜnkü bunlardan bir tanesi bir Request'teki scope icin yeterlidir...

            Scoped : Bir Request'te Scope'un parametre kümesinde aynı tipte birden fazla parametre varsa bile  1 instance üzerinden calısırsınız...Ancak bu Singleton degildir...Cünkü Request'in işi bittigi zaman Garbage Collector Ram'den o instance'i kaldırır...Bir Request'in scope'unda aynı tipte birden fazla instance Repository'ler ve Managerlar icin gerekli degildir... O yüzden Scoped tercih edilir...
             

            Singleton : Bir Request'in ulastıgı Scope'un parametre kümesinde bir tip görüldügü anda instance alınır ve program kapanıncaya kadar o instance'tan devam devam edilir...Manager ve Repository'ler icin cok tehlikelidir...
             
             
             
             
             
             
             */

            //Eger siz Generic bir interface ile calısacaksanız bu AddScoped /AddTransient/AddSingleton metotlarına ilgili tipi typeof keyword'u ile belirtmelisiniz

          


            services.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>)); //Burada programa demek istedigimiz şey bir scope icerisinde ilgili Scope'a gelecek parametre generic bir IRepository ise bize generic bir BaseRepository instance'i gönder diyoruz

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IAppUserRepository,AppUserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            return services;


            
        }
    }
}
