using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.UI.Areas.Manager.Models.AdministrativeVMs;
using Project.UI.Areas.Manager.Models.HRVMs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Project.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class AdministrativeController : Controller
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly IAppUserRoleManager _appUserRoleManager;
        private readonly IAppRoleManager _appRoleManager;

        public AdministrativeController(
            IAppUserManager appUserManager,
            IAppUserProfileManager appUserProfileManager,
            IAppUserRoleManager appUserRoleManager,
            IAppRoleManager appRoleManager)
        {
            _appUserManager = appUserManager;
            _appUserProfileManager = appUserProfileManager;
            _appUserRoleManager = appUserRoleManager;
            _appRoleManager = appRoleManager;
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        public async Task<IActionResult> PersonnelManagement(PersonnelFilterType filter = PersonnelFilterType.TumPersoneller)
        {
            List<AppUserDTO> users = await _appUserManager.GetConfirmedUsersAsync();
            List<AppUserProfileDTO> profiles = await _appUserProfileManager.GetAllAsync();
            List<AppUserRoleDTO> userRoles = await _appUserRoleManager.GetActives();
            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();

            AppRoleDTO adminRole = roles.FirstOrDefault(r => r.Name == "Admin");
            HashSet<int> adminUserIds = userRoles
                .Where(ur => adminRole != null && ur.RoleId == adminRole.Id)
                .Select(ur => ur.UserId)
                .ToHashSet();

            string[] filterRoles = filter switch
            {
                PersonnelFilterType.Mutfak => new[] { "Mutfak Sefi", "Aşçı", "Aşçı Yardımcısı" },
                PersonnelFilterType.Salon => new[] { "Garson", "Komi" },
                PersonnelFilterType.Yönetim => new[] { "Insan Kaynaklari Muduru", "Restaurant Muduru", "Idari Personel" },
                PersonnelFilterType.Hizmet => new[] { "Temizlikçi", "Bulaşıkçı" },
                _ => Array.Empty<string>()
            };

            List<PersonnelVm> personnel = users
                .Where(u => !adminUserIds.Contains(u.Id))
                .Where(u =>
                    filter == PersonnelFilterType.TumPersoneller ||
                    userRoles.Where(ur => ur.UserId == u.Id)
                             .Select(ur => roles.FirstOrDefault(r => r.Id == ur.RoleId)?.Name)
                             .Any(roleName => filterRoles.Contains(roleName)))
                .Select(u =>
                {
                    AppUserProfileDTO profile = profiles.FirstOrDefault(p => p.AppUserId == u.Id);
                    IEnumerable<string> userRoleNames = userRoles
                        .Where(ur => ur.UserId == u.Id)
                        .Select(ur => roles.FirstOrDefault(r => r.Id == ur.RoleId)?.Name)
                        .Where(name => !string.IsNullOrEmpty(name));
                    return new PersonnelVm
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Email = u.Email,
                        FullName = profile != null ? $"{profile.FirstName} {profile.LastName}" : "",
                        Roles = string.Join(", ", userRoleNames),
                        HireDate = profile?.HireDate ?? DateTime.MinValue
                    };
                })
                .ToList();

            PersonnelListVm vm = new PersonnelListVm
            {
                Personnel = personnel,
                Filter = filter
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddPersonnel()
        {
            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
            AddPersonnelVm vm = new AddPersonnelVm
            {
                Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonnel(AddPersonnelVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                vm.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();
                return View(vm);
            }

            AppUserDTO userDto = new AppUserDTO
            {
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password,
                EmailConfirmed = true
            };

            OperationStatus userResult = await _appUserManager.CreateAsync(userDto);
            if (userResult != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Kullanıcı eklenemedi. Lütfen kullanıcı adı, email ve şifreyi kontrol edin.");
                List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                vm.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();
                return View(vm);
            }

            AppUserDTO createdUser = (await _appUserManager.GetConfirmedUsersAsync())
                .FirstOrDefault(u => u.UserName == vm.UserName);

            if (createdUser == null)
            {
                ModelState.AddModelError("", "Kullanıcı eklenmiş gibi görünüyor ama bulunamadı. Lütfen tekrar deneyin.");
                List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                vm.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();
                return View(vm);
            }

            AppUserProfileDTO profileDto = new AppUserProfileDTO
            {
                AppUserId = createdUser.Id,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                TCKNo = vm.TCKNo,
                Salary = vm.Salary,
                HireDate = vm.HireDate,
                BirthDate = vm.BirthDate
            };
            OperationStatus profileResult = await _appUserProfileManager.CreateAsync(profileDto);
            if (profileResult != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Profil eklenemedi. Lütfen tüm alanları kontrol edin.");
                List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                vm.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();
                return View(vm);
            }

            if (vm.SelectedRoleId > 0)
            {
                AppUserRoleDTO existingRole = await _appUserRoleManager.GetByCompositeKeyAsync(createdUser.Id, vm.SelectedRoleId);
                if (existingRole != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcıya bu rol zaten atanmış.");
                    List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                    vm.Roles = roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList();
                    return View(vm);
                }

                AppUserRoleDTO roleDto = new AppUserRoleDTO
                {
                    UserId = createdUser.Id,
                    RoleId = vm.SelectedRoleId
                };
                OperationStatus roleResult = await _appUserRoleManager.CreateAsync(roleDto);
                if (roleResult != OperationStatus.Success)
                {
                    ModelState.AddModelError("", "Rol atanamadı. Lütfen rol seçimini kontrol edin.");
                    List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                    vm.Roles = roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList();
                    return View(vm);
                }
            }

            TempData["Success"] = "Personel başarıyla eklendi.";
            return RedirectToAction("PersonnelManagement");
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View(new AddRoleVm());
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            AppRoleDTO dto = new AppRoleDTO
            {
                Name = vm.Name
            };

            OperationStatus result = await _appRoleManager.CreateAsync(dto);

            if (result == OperationStatus.Success)
            {
                TempData["Success"] = "Rol başarıyla eklendi.";
                return RedirectToAction("PersonnelManagement");
            }

            ModelState.AddModelError("", result == OperationStatus.AlreadyExists
                ? "Bu rol zaten mevcut."
                : "Rol eklenemedi.");
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole()
        {
            TempData["Error"] = null;
            TempData["Success"] = null;
            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
            List<AppUserRoleDTO> userRoles = await _appUserRoleManager.GetActives();

            List<DeleteRoleVm> vm = roles.Select(r => new DeleteRoleVm
            {
                Id = r.Id,
                Name = r.Name,
                UserCount = userRoles.Count(ur => ur.RoleId == r.Id)
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                AppRoleDTO role = await _appRoleManager.GetByIdAsync(id);
                if (role == null)
                {
                    TempData["Error"] = "Silinmek istenen rol bulunamadı.";
                    return RedirectToAction("DeleteRole");
                }
                OperationStatus result = await _appRoleManager.SoftDeleteByIdAsync(role.Id);

                result = await _appRoleManager.HardDeleteByIdAsync(role.Id);

                if (result == OperationStatus.Success)
                {
                    TempData["Success"] = "Rol başarıyla silindi.";
                }
                else if (result == OperationStatus.Failed)
                {
                    TempData["Error"] = "Rol silinemedi. Bu role bağlı kullanıcılar olabilir.";
                }
                else
                {
                    TempData["Error"] = "Rol silinemedi (bilinmeyen hata).";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Rol silinemedi. Hata: {ex.Message}";
            }

            return RedirectToAction("DeleteRole");
        }

        [HttpGet]
        public async Task<IActionResult> PersonnelDetail(int id)
        {
            AppUserDTO user = await _appUserManager.GetByIdAsync(id);
            AppUserProfileDTO profile = (await _appUserProfileManager.GetAllAsync()).FirstOrDefault(p => p.AppUserId == id);
            List<AppUserRoleDTO> userRoles = await _appUserRoleManager.GetActives();
            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();

            if (user == null || profile == null)
                return NotFound();

            List<string> roleNames = userRoles
                .Where(ur => ur.UserId == id)
                .Select(ur => roles.FirstOrDefault(r => r.Id == ur.RoleId)?.Name)
                .Where(r => !string.IsNullOrEmpty(r))
                .ToList();

            int daysWorked = (DateTime.Now - profile.HireDate).Days;
            int yearsWorked = daysWorked / 365;
            int totalAnnualLeave = yearsWorked * 14;

            int usedAnnualLeave = 0;

            decimal severancePay = yearsWorked * profile.Salary;

            int noticePeriods = daysWorked / 182;
            decimal noticePay = noticePeriods * (profile.Salary * 0.5m);

            PersonnelDetailVm vm = new PersonnelDetailVm
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = $"{profile.FirstName} {profile.LastName}",
                TCKNo = profile.TCKNo,
                Salary = profile.Salary,
                HireDate = profile.HireDate,
                BirthDate = profile.BirthDate,
                Roles = roleNames,
                TotalAnnualLeaveDays = totalAnnualLeave,
                UsedAnnualLeaveDays = usedAnnualLeave,
                EstimatedSeverancePay = severancePay,
                EstimatedNoticePay = noticePay
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnualLeave(int userId, int days)
        {
            TempData["Success"] = $"{days} gün yıllık izin eklendi.";
            return RedirectToAction("PersonnelDetail", new { id = userId });
        }
    }
}