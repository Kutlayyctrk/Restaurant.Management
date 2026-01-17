
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Domain.Entities.Concretes;
using Project.UI.Models.LoginVMs;
using Project.UI.Models.RegisterVms;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.UI.Controllers
{
    public class LoginAndRegisterController : Controller
    {


        private readonly IAppUserManager _appUserManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginAndRegisterController(IAppUserManager appUserManager, UserManager<AppUser> userManager)
        {
            _appUserManager = appUserManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPageVM vM)
        {
            AppUserDTO loginDTO = new AppUserDTO()
            {
                UserName = vM.UserName,
                Password = vM.Password,
                RememberMe = vM.RememberMe

            };

            string result = await _appUserManager.LoginAsync(loginDTO);

            if (result.Contains("Email adresiniz doğrulanmamış"))
            {
                return RedirectToAction("AccessDenied", "LoginAndRegister");
            }

            if (result.StartsWith("Error"))
            {
                string[] messages = result.Replace("Error|", "").Split('|');
                foreach (string message in messages)
                {
                    ModelState.AddModelError("", message);
                }
                return View(vM);
            }


            string[] roles = result.Replace("Success|", "").Split(',');

            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            else if (roles.Contains("Insan Kaynaklari Muduru"))
            {
                return RedirectToAction("DashBoard", "Hr", new { area = "Manager" });

            }
            else if (roles.Contains("Restaurant Muduru\""))
            {
                return RedirectToAction("Index", "Restaurant", new { area = "Manager" });
            }
            else if (roles.Contains("Mutfak Sefi"))
            {
                return RedirectToAction("Index", "Kitchen", new { area = "Manager" });
            }
            else if (roles.Contains("Bar Sefi"))
            {
                return RedirectToAction("Index", "Bar", new { area = "Manager" });
            }
            else if (roles.Contains("İdari Personel"))
            {
                return RedirectToAction("Index", "Administration", new { area = "Manager" });
            }
            return RedirectToAction("AccessDenied", "LoginAndRegister");


        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vM)
        {
            if (!ModelState.IsValid)
            {
                return View(vM);
            }
            AppUserDTO dto = new()
            {
                UserName = vM.UserName,
                Email = vM.Email,
                ConfirmEmail = vM.ConfirmedEmail,
                Password = vM.Password,
                RoleIds = new List<int> { vM.Role }
            };
            string result = await _appUserManager.CreateAsync(dto);

            if (result.StartsWith("Error"))
            {
                string[] messages = result.Replace("Error|", "").Split('|');
                foreach (string message in messages)
                {
                    ModelState.AddModelError("", message);
                }
                return View(vM);
            }
            return RedirectToAction("Login", "LoginAndRegister");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string result = await _appUserManager.ConfirmEmailAsync(userId, token);

            if (result.StartsWith("Success"))
                return View("ConfirmEmail");
            else
                return View("ConfirmEmailFailed"); 
        }


    }
}
