using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjections
{
    //DbContextPool'umuz böylece StartUp'da DAL'den bir sınıfı kullanmaktan ziyade sadece BLL'deki bu yaratılmıs olan class'ın kurallarıyla bir Service Entegrasyonu yapacaktır...

    public static class DbContextService
    {
        public static IServiceCollection AddDbContextService(this IServiceCollection services)
        {

            //Neden ServiceProvider

            //Cünkü biz bu noktada aslında bir Core.MVC platformundaki startup dosyasında degiliz...Dolayısıyla oradaki hazır service elimizde yok...Biz o yapıları ayaga kaldırmak adına bir giriş noktasına ihtiyac duyarız...Ve bu giriş noktasını bana ServiceProvider nesnesi saglar...

            ServiceProvider provider = services.BuildServiceProvider();

            //Sakın IConfiguration kullanırken Castle kütüphanesini kullanmayın... Kullanacagınız kütüphane Microsoft.Extensions.Configuration olmak zorundadır...

            //Neden IConfiguration

            //IConfiguration sayesinde projenizin conf.(ayarlamalarının) bulundugu dosyaya ulasabiliyorsunuz...

            IConfiguration? configuration = provider.GetService<IConfiguration>();

            services.AddDbContextPool<MyContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());

            return services;

        }
    }
}
