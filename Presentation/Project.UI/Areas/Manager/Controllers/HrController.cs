using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.UI.Areas.Manager.Models.AppUserVMs;
using Project.UI.Areas.Manager.Models.CandidateVms;


namespace Project.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HrController : Controller
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly IAppRoleManager _appRoleManager;
        private readonly IAppUserRoleManager _appUserRoleManager;

        public HrController(
            IAppUserManager appUserManager,
            IAppUserProfileManager appUserProfileManager,
            IAppRoleManager appRoleManager,
            IAppUserRoleManager appUserRoleManager)
        {
            _appUserManager = appUserManager;
            _appUserProfileManager = appUserProfileManager;
            _appRoleManager = appRoleManager;
            _appUserRoleManager = appUserRoleManager;
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        public async Task<IActionResult> PersonelList()
        {
            List<AppUserDTO> users = await _appUserManager.GetAllAsync();
            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetAllAsync();
            List<AppRoleDTO> allRoles = await _appRoleManager.GetAllAsync();

            HrListPageVM vm = new HrListPageVM
            {
                Persons = users.Select(u => new PersonelVm
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    InsertedDate = u.InsertedDate,
                    Roles = string.Join(", ", allUserRoles.Where(ur => ur.UserId == u.Id)
                        .Select(ur => allRoles.FirstOrDefault(r => r.Id == ur.RoleId)?.Name ?? "").Where(name => !string.IsNullOrEmpty(name)))
                }).ToList()
            };
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AppUserDTO userDto = await _appUserManager.GetByIdAsync(id);
            if (userDto == null) return NotFound();

            List<AppUserProfileDTO> allProfiles = await _appUserProfileManager.GetAllAsync();
            AppUserProfileDTO profileDto = allProfiles.FirstOrDefault(p => p.AppUserId == id);
            if (profileDto == null) return NotFound();

            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
            List<SelectListItem> rolesList = roles.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();

            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetAllAsync();
            int currentRoleId = allUserRoles.FirstOrDefault(ur => ur.UserId == id)?.RoleId ?? 0;
            List<string> currentRoles = allUserRoles.Where(ur => ur.UserId == id)
                .Select(ur => roles.FirstOrDefault(r => r.Id == ur.RoleId)?.Name ?? "").Where(name => !string.IsNullOrEmpty(name)).ToList();
            List<(int RoleId, string RoleName)> roleList = allUserRoles.Where(ur => ur.UserId == id)
                .Select(ur => (ur.RoleId, roles.FirstOrDefault(r => r.Id == ur.RoleId)?.Name ?? "")).Where(t => !string.IsNullOrEmpty(t.Item2)).ToList();

            PersonelEditVm vm = new PersonelEditVm
            {
                Id = userDto.Id,
                AppUserId = userDto.Id,
                ProfileId = profileDto.Id,
                FirstName = profileDto.FirstName,
                LastName = profileDto.LastName,
                TCKNo = profileDto.TCKNo,
                Salary = profileDto.Salary,
                BirthDate = profileDto.BirthDate,
                HireDate = profileDto.HireDate,
                SelectedRoleId = currentRoleId,
                Roles = rolesList,
                CurrentRoles = currentRoles,
                RoleList = roleList
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonelEditVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AppUserProfileDTO originalProfile = await _appUserProfileManager.GetByIdAsync(vm.ProfileId);
            AppUserProfileDTO profileDto = new AppUserProfileDTO
            {
                Id = vm.ProfileId,
                AppUserId = vm.AppUserId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                TCKNo = vm.TCKNo,
                Salary = vm.Salary,
                BirthDate = vm.BirthDate,
                HireDate = vm.HireDate
            };

            string updateResult = await _appUserProfileManager.UpdateAsync(originalProfile, profileDto);
            if (!string.IsNullOrEmpty(updateResult))
            {
                ModelState.AddModelError("", updateResult);
                return View(vm);
            }

            // Rol ekleme: Mevcut rolleri kontrol et, yeni rolü ekle (aynıysa ekleme)
            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetAllAsync();
            List<AppUserRoleDTO> currentRoles = allUserRoles.Where(ur => ur.UserId == vm.AppUserId).ToList();
            bool hasRole = currentRoles.Any(ur => ur.RoleId == vm.SelectedRoleId);

            if (!hasRole && vm.SelectedRoleId > 0)
            {
                AppUserRoleDTO newRoleDto = new AppUserRoleDTO { UserId = vm.AppUserId, RoleId = vm.SelectedRoleId };
                string addResult = await _appUserRoleManager.CreateAsync(newRoleDto);
                if (!string.IsNullOrEmpty(addResult) && !addResult.Contains("başarıyla"))
                {
                    TempData["Error"] = addResult;
                    return View(vm);
                }
                else
                {
                    TempData["Success"] = "Rol eklendi.";
                }
            }

            return RedirectToAction("PersonelList");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveRoleFromEdit(int userId, int roleId)
        {
            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetAllAsync();
            AppUserRoleDTO userRole = allUserRoles.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (userRole != null)
            {
                string result = await _appUserRoleManager.SoftDeleteAsync(userRole.Id);
                if (!string.IsNullOrEmpty(result))
                {
                    TempData["Error"] = result;
                }
            }
            return RedirectToAction("Edit", new { id = userId });
        }
        [HttpPost]
        public async Task<IActionResult> AddRoleFromEdit(int userId, int roleId)
        {
            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetAllAsync();
            bool hasRole = allUserRoles.Any(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (!hasRole && roleId > 0)
            {
                AppUserRoleDTO newRoleDto = new AppUserRoleDTO { UserId = userId, RoleId = roleId };
                string addResult = await _appUserRoleManager.CreateAsync(newRoleDto);
                if (!string.IsNullOrEmpty(addResult) && !addResult.Contains("başarıyla"))
                {
                    TempData["Error"] = addResult;
                }
                else
                {
                    TempData["Success"] = "Rol eklendi.";
                }
            }
            else if (hasRole)
            {
                TempData["Error"] = "Bu rol zaten mevcut.";
            }
            else
            {
                TempData["Error"] = "Geçerli bir rol seçiniz.";
            }

            return RedirectToAction("Edit", new { id = userId });
        }

    }
}