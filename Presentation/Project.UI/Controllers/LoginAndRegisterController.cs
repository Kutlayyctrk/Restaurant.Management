using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.Enums;   
using Project.Application.Managers;
using Project.Domain.Entities.Concretes;
using Project.UI.Models.LoginVMs;
using Project.UI.Models.RegisterVms;
using System.Collections.Generic;
using System.Linq;
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
            AppUserDTO loginDTO = new AppUserDTO
            {
                UserName = vM.UserName,
                Password = vM.Password,
                RememberMe = vM.RememberMe
            };

            OperationStatus result = await _appUserManager.LoginAsync(loginDTO);

            if (result == OperationStatus.Failed)
            {
                ModelState.AddModelError("", "Invalid username or password, or email not confirmed.");
                return View(vM);
            }

            if (result == OperationStatus.ValidationError)
            {
                ModelState.AddModelError("", "Validation failed.");
                return View(vM);
            }

            if (result == OperationStatus.NotFound)
            {
                ModelState.AddModelError("", "User not found.");
                return View(vM);
            }

           
            AppUser user = await _userManager.FindByNameAsync(loginDTO.UserName);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            else if (roles.Contains("Insan Kaynaklari Muduru"))
                return RedirectToAction("DashBoard", "Hr", new { area = "Manager" });
            else if (roles.Contains("Restaurant Muduru"))
                return RedirectToAction("DashBoard", "Restaurant", new { area = "Manager" });
            else if (roles.Contains("Mutfak Sefi"))
                return RedirectToAction("DashBoard", "Kitchen", new { area = "Manager" });
            else if (roles.Contains("Bar Sefi"))
                return RedirectToAction("DashBoard", "Bar", new { area = "Manager" });
            else if (roles.Contains("Idari Personel"))
                return RedirectToAction("DashBoard", "Administrative", new { area = "Manager" });
            else if (roles.Contains("Garson"))
                return RedirectToAction("Index", "Sales");

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
                Password = vM.Password,
                RoleIds = new List<int> { vM.Role }
            };

            OperationStatus result = await _appUserManager.CreateAsync(dto);

            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Registration failed.");
                return View(vM);
            }

            return RedirectToAction("Login", "LoginAndRegister");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            OperationStatus result = await _appUserManager.ConfirmEmailAsync(userId, token);

            if (result == OperationStatus.Success)
                return View("ConfirmEmail");
            else
                return View("ConfirmEmailFailed");
        }
    }
}