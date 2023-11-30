using Microsoft.Extensions.DependencyInjection;
using Project.BLL.ServiceInjections;
using Project.COREMVC.EmailService;
using Project.COREMVC.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache(); //Eger Session kompleks yapılarla calısmak icin Extension metodu eklenme durumuna maruz kalmıssa bu kod projenizin saglıklı calısması icin gereklidir...

builder.Services.AddSession(x =>
{
    x.IdleTimeout = TimeSpan.FromMinutes(5); //Projeyi kişinin bos durma süresi eger 1 dakikalık bir bos durma süresi olursa Session bosa cıksın...
    x.Cookie.HttpOnly = true; //document.cookie'den ilgili bilginin gözlemlenmesi
    x.Cookie.IsEssential = true;
});

builder.Services.AddIdentityServices();

builder.Services.AddDbContextService(); //DbContextService'imizi BLL'den alarak Middleware'e ekledik...
builder.Services.AddRepServices();
builder.Services.AddManagerServices();

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<IEmailService, EmailService>();
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
     name: "Admin",
     pattern:"{area}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shopping}/{action=Index}/{id?}");

app.Run();
