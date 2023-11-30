using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.COREMVC.EmailService;
using Project.COREMVC.Models;
using Project.COREMVC.Models.AppUsers;
using Project.ENTITIES.Models;
using System.Diagnostics;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Project.COREMVC.Controllers
{
    public class HomeController : Controller
    {

        //Todo: SignIn yapan kullanıcı Register ve SignIn linklerini görmektense SignOut linkini görsün
        //Todo: Beni hatırla icin checkbox koyun...
       
        //ToDo : En önemli Refactoring'iniz burada BLL katmanındaki BL yapan interfaceleri kullanmanızdır

        //Todo: Validation'lar yapılacak


        private readonly ILogger<HomeController> _logger;

        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole<int>> _roleManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IEmailService _emailService;



        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager, SignInManager<AppUser> signInManager,IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;   
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            AppUser appUser = new()
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                #region RolKontrolIslemi
                IdentityRole<int> appRole = await _roleManager.FindByNameAsync("Member");
                if (appRole == null) await _roleManager.CreateAsync(new() { Name = "Member" });
                await _userManager.AddToRoleAsync(appUser, "Member");
                #endregion

                Guid specId = Guid.NewGuid();
                string body = $"Hesabınız olusturulmustur.Üyeliginizi onaylamak icin lütfen http://localhost:5016/Home/ConfirmEmail?specId={specId}&id={appUser.Id} linkine tıklayınız";
                //MailRequest mr = new MailRequest()
                //{
                //    ToEmail = model.Email,
                //    Subject = "Test!!",
                //    Body = body



                //};
                //await _emailService.SendEmailAsync(mr);
                MailService.Send(model.Email, body: body);

                TempData["Message"] = "Emailinizi kontrol ediniz";
                return RedirectToAction("RedirectPanel");
            }

            return View();


        }

        public async Task<IActionResult> ConfirmEmail(Guid specId, int id)
        {

            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null )
            {
                TempData["Message"] = "Kullanıcı bulunamadı";
                return RedirectToAction("RedirectPanel");
            }
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            TempData["Message"] = "Emailiniz basarıyla onaylandı";
            return RedirectToAction("SignIn");
        }


        public IActionResult RedirectPanel()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserRegisterModel model)
        {
            AppUser appUser = await _userManager.FindByNameAsync(model.UserName);

            SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, true, true);
            if(result.Succeeded)
            {
                IList<string> roles = await _userManager.GetRolesAsync(appUser);
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Category", new { Area = "Admin" });
                }
                else if (roles.Contains("Member"))
                {
                    return RedirectToAction("Privacy"); //todo ShoppingTool
                }
                return RedirectToAction("Panel");
            }
            else if (result.IsNotAllowed)
            {
                return RedirectToAction("MailPanel");
            }
            TempData["Message"] = "Kullanıcı bulunamadı";

            return RedirectToAction("SignIn");

        }


        public IActionResult MailPanel()
        {
            return View();
        }

        [Authorize(Roles ="Member")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
