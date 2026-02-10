using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Application.Managers;
using Project.Domain.Enums;
using Project.UI.Areas.Manager.Models.HRVMs;



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

        private List<SelectListItem> MapRolesToSelectList(List<AppRoleDTO> roles)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (AppRoleDTO role in roles)
            {
                selectList.Add(new SelectListItem
                {
                    Value = role.Id.ToString(),
                    Text = role.Name
                });
            }
            return selectList;
        }

        
        private List<string> MapUserRolesToNames(List<AppUserRoleDTO> userRoles, List<AppRoleDTO> roles)
        {
            List<string> roleNames = new List<string>();
            foreach (AppUserRoleDTO userRole in userRoles)
            {
                AppRoleDTO role = roles.FirstOrDefault(r => r.Id == userRole.RoleId);
                if (role != null && !string.IsNullOrEmpty(role.Name))
                {
                    roleNames.Add(role.Name);
                }
            }
            return roleNames;
        }

      
        private List<(int RoleId, string RoleName)> MapUserRolesToTuple(List<AppUserRoleDTO> userRoles, List<AppRoleDTO> roles)
        {
            List<(int RoleId, string RoleName)> tuples = new List<(int RoleId, string RoleName)>();
            foreach (AppUserRoleDTO userRole in userRoles)
            {
                AppRoleDTO role = roles.FirstOrDefault(r => r.Id == userRole.RoleId);
                if (role != null && !string.IsNullOrEmpty(role.Name))
                {
                    tuples.Add((userRole.RoleId, role.Name));
                }
            }
            return tuples;
        }


        public IActionResult DashBoard()
        {
            return View();
        }

        public async Task<IActionResult> PersonelList()
        {
           
            List<AppUserDTO> users = await _appUserManager.GetConfirmedUsersAsync();

         
            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetActives();

         
            List<AppRoleDTO> allRoles = await _appRoleManager.GetAllAsync();

            HrListPageVM vm = new HrListPageVM
            {
                Persons = users.Select(u => new PersonelVm
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    InsertedDate = u.InsertedDate,
                    Roles = string.Join(", ",
                        allUserRoles.Where(ur => ur.UserId == u.Id)
                                    .Select(ur => allRoles.FirstOrDefault(r => r.Id == ur.RoleId)?.Name ?? string.Empty)
                                    .Where(name => !string.IsNullOrEmpty(name)))
                }).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> CompleteProfileList()
        {
      
            List<AppUserDTO> allUsers = await _appUserManager.GetConfirmedUsersAsync();

        
            List<AppUserProfileDTO> allProfiles = await _appUserProfileManager.GetAllAsync();

         
            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetActives();

        
            List<AppUserDTO> incompleteUsers = allUsers
                .Where(u => !allProfiles.Any(p => p.AppUserId == u.Id) ||
                            !allUserRoles.Any(r => r.UserId == u.Id))
                .ToList();

            ProfileCompleteListVm vm = new ProfileCompleteListVm
            {
                Users = incompleteUsers
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> CompleteProfile(int id)
        {
            AppUserDTO user = await _appUserManager.GetByIdAsync(id);
            if (user == null || !user.EmailConfirmed)
                return NotFound();

            ProfileCompleteVm vm = new ProfileCompleteVm
            {
                AppUserId = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

         
            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
            vm.Roles = roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteProfile(ProfileCompleteVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

      
            AppUserProfileDTO profileDto = new AppUserProfileDTO
            {
                AppUserId = vm.AppUserId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                TCKNo = vm.TCKNo,
                Salary = vm.Salary,
                BirthDate = vm.BirthDate,
                HireDate = vm.HireDate
            };

            Result profileResult = await _appUserProfileManager.CreateAsync(profileDto);
            if (!profileResult.IsSuccess)
            {
                TempData["Error"] = "Profil oluþturulamadý.";
                return View(vm);
            }

       
            if (vm.SelectedRoleId > 0)
            {
                AppUserRoleDTO roleDto = new AppUserRoleDTO
                {
                    UserId = vm.AppUserId,
                    RoleId = vm.SelectedRoleId
                };

                Result roleResult = await _appUserRoleManager.CreateAsync(roleDto);

                if (!roleResult.IsSuccess)
                {
                    TempData["Error"] = roleResult.Status == OperationStatus.AlreadyExists
                        ? "Bu rol zaten atanmýþ."
                        : "Rol atanamadý.";
                    return View(vm);
                }
            }

            TempData["Success"] = "Profil baþarýyla tamamlandý.";
            return RedirectToAction("CompleteProfileList");
        }

        [HttpGet]
        public async Task<IActionResult> RoleList()
        {
     
            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();

            RoleListVM vm = new RoleListVM
            {
                Roles = roles.Select(r => new RoleVm
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList()
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Reports()
        {
         
            List<AppUserDTO> users = await _appUserManager.GetAllAsync();

       
            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();

         
            List<AppUserRoleDTO> userRoles = await _appUserRoleManager.GetActives();

            ReportVm vm = new ReportVm
            {
                TotalUsers = users.Count,
                ActiveUsers = users.Count(u => u.Status == DataStatus.Inserted)
            };

           
            foreach (AppRoleDTO role in roles)
            {
                int count = userRoles.Count(ur => ur.RoleId == role.Id);
                vm.RoleDistribution.Add(role.Name, count);
            }

            return View(vm);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

         
            AppRoleDTO dto = new AppRoleDTO
            {
                Name = vm.Name
            };

         
            Result result = await _appRoleManager.CreateAsync(dto);

            if (result.IsSuccess)
            {
                TempData["Success"] = "Rol baþarýyla oluþturuldu.";
                return RedirectToAction("RoleList");
            }

            TempData["Error"] = result.Status == OperationStatus.AlreadyExists
                ? "Bu rol zaten mevcut."
                : "Rol oluþturulamadý.";

            return View(vm);
        }







        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AppUserDTO userDto = await _appUserManager.GetByIdAsync(id);
            if (userDto == null) return NotFound();

            AppUserProfileDTO profileDto = (await _appUserProfileManager.GetAllAsync())
                .FirstOrDefault(p => p.AppUserId == id);
            if (profileDto == null) return NotFound();

            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
            List<SelectListItem> rolesList = MapRolesToSelectList(roles);

            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetActives();
            List<AppUserRoleDTO> userRoles = allUserRoles.Where(ur => ur.UserId == id).ToList();

            int currentRoleId = userRoles.FirstOrDefault()?.RoleId ?? 0;
            List<string> currentRoles = MapUserRolesToNames(userRoles, roles);
            List<(int RoleId, string RoleName)> roleList = MapUserRolesToTuple(userRoles, roles);

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
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Form validation error. Please check all fields.";
                return View(vm);
            }

           
            AppUserProfileDTO originalProfile = await _appUserProfileManager.GetByIdAsync(vm.ProfileId);
            if (originalProfile == null)
            {
                TempData["Error"] = "Profile not found.";
                return View(vm);
            }

          
            AppUserProfileDTO newProfile = new AppUserProfileDTO
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

            
            Result updateResult = await _appUserProfileManager.UpdateAsync(originalProfile, newProfile);
            if (!updateResult.IsSuccess)
            {
                TempData["Error"] = updateResult.Status switch
                {
                    OperationStatus.NotFound => "Profile not found.",
                    OperationStatus.ValidationError => "Validation failed.",
                    _ => "Update failed."
                };
                return View(vm);
            }

          
            if (vm.SelectedRoleId > 0)
            {
                AppUserRoleDTO newRoleDto = new AppUserRoleDTO
                {
                    UserId = vm.AppUserId,
                    RoleId = vm.SelectedRoleId
                };

                Result addResult = await _appUserRoleManager.CreateAsync(newRoleDto);

                if (addResult.IsSuccess)
                    TempData["Success"] = "Role added successfully.";
                else if (addResult.Status == OperationStatus.AlreadyExists)
                    TempData["Error"] = "This role already exists.";
                else
                    TempData["Error"] = "Failed to add role.";
            }

            return RedirectToAction("PersonelList");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveRoleFromEdit(int userId, int roleId)
        {
            if (roleId <= 0)
            {
                TempData["Error"] = "Please select a valid role.";
                return RedirectToAction("Edit", new { id = userId });
            }

            Result result = await _appUserRoleManager.HardDeleteByCompositeKeyAsync(userId, roleId);

            if (result.IsSuccess)
                TempData["Success"] = "Role removed successfully.";
            else if (result.Status == OperationStatus.NotFound)
                TempData["Error"] = "Role not found.";
            else
                TempData["Error"] = "Failed to remove role.";

            return RedirectToAction("Edit", new { id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleFromEdit(int userId, int roleId)
        {
            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetActives();
            bool hasRole = allUserRoles.Any(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (hasRole)
            {
                TempData["Error"] = "This role already exists.";
            }
            else if (roleId > 0)
            {
                AppUserRoleDTO newRoleDto = new AppUserRoleDTO { UserId = userId, RoleId = roleId };
                Result addResult = await _appUserRoleManager.CreateAsync(newRoleDto);

                if (addResult.IsSuccess)
                    TempData["Success"] = "Role added successfully.";
                else
                    TempData["Error"] = "Failed to add role.";
            }
            else
            {
                TempData["Error"] = "Please select a valid role.";
            }

            return RedirectToAction("Edit", new { id = userId });
        }

    }
}