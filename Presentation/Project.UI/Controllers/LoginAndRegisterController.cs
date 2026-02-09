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
        private readonly IAppUserRoleManager _appUserRoleManager;

        public LoginAndRegisterController(IAppUserManager appUserManager, UserManager<AppUser> userManager, IAppUserRoleManager appUserRoleManager)
        {
            _appUserManager = appUserManager;
            _userManager = userManager;
            _appUserRoleManager = appUserRoleManager;
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
            IList<int> roleIds = await _appUserRoleManager.GetRoleIdsByUserIdAsync(user.Id);

            if (roleIds.Contains(RoleIds.InsanKaynaklariMuduru))
                return RedirectToAction("DashBoard", "Hr", new { area = "Manager" });
            else if (roleIds.Contains(RoleIds.RestaurantMuduru))
                return RedirectToAction("DashBoard", "Restaurant", new { area = "Manager" });
            else if (roleIds.Contains(RoleIds.MutfakSefi))
                return RedirectToAction("DashBoard", "Kitchen", new { area = "Manager" });
            else if (roleIds.Contains(RoleIds.BarSefi))
                return RedirectToAction("DashBoard", "Bar", new { area = "Manager" });
            else if (roleIds.Contains(RoleIds.IdariPersonel))
                return RedirectToAction("DashBoard", "Administrative", new { area = "Manager" });
            else if (roleIds.Contains(RoleIds.Garson))
                return RedirectToAction("Index", "Sales");

            return RedirectToAction("AccessDenied", "LoginAndRegister");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _appUserManager.LogoutAsync();
            return RedirectToAction("Login", "LoginAndRegister");
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