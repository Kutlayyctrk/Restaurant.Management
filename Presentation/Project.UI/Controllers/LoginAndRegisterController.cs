
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.UI.Models.LoginVMs;

namespace Project.UI.Controllers
{
    public class LoginAndRegisterController : Controller
    {
      

      private readonly IAppUserManager _appUserManager;

        public LoginAndRegisterController(IAppUserManager appUserManager)
        {
            _appUserManager = appUserManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPageVM vM)
        {
            string result = await _appUserManager.LoginAsync(vM.User);

            if (result.StartsWith("Error"))
            {
                ModelState.AddModelError("", result.Replace("Error|", ""));
                return View(vM);
            }

           
            var roles = result.Replace("Success|", "").Split(',');

            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            else if (roles.Contains("İnsan Kaynakları Müdürü"))
            {
                return RedirectToAction("Index", "HR", new { area = "Manager" });
            }
            else if (roles.Contains("Restaurant Müdürü"))
            {
                return RedirectToAction("Index", "Restaurant", new { area = "Manager" });
            }
            else if (roles.Contains("Mutfak Şefi"))
            {
                return RedirectToAction("Index", "Kitchen", new { area = "Manager" });
            }
            else if (roles.Contains("Bar Şefi"))
            {
                return RedirectToAction("Index", "Bar", new { area = "Manager" });
            }
            else if (roles.Contains("İdari Personel"))
            {
                return RedirectToAction("Index", "Administration", new { area = "Manager" });
            }

            // Default yönlendirme
            return RedirectToAction("Index", "Home");
        }

    }
}
