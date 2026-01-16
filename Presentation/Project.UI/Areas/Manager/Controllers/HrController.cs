using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.UI.Areas.Manager.Models.AppUserVMs;


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
            HrListPageVM vm = new HrListPageVM
            {
                Persons = users.Select(u => new PersonelVm
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    InsertedDate = u.InsertedDate
                }).ToList()
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            List<AppUserProfileDTO> allProfiles = await _appUserProfileManager.GetAllAsync();
            AppUserProfileDTO userProfile = allProfiles.FirstOrDefault(x => x.AppUserId == id);

            if (userProfile == null)
            {
                return NotFound($"ID'si {id} olan kullanıcıya ait bir profil kaydı bulunamadı.");
            }

           
            List<AppUserRoleDTO> userRoles = await _appUserRoleManager.GetAllAsync();
            AppUserRoleDTO userRoleRel = userRoles.FirstOrDefault(x => x.UserId == userProfile.AppUserId && x.Status != DataStatus.Deleted);
            int selectedRoleId = userRoleRel?.RoleId ?? 0;

        
            List<AppRoleDTO> roleDtos = await _appRoleManager.GetAllAsync();

            PersonelEditVm vm = new PersonelEditVm
            {
                Id = userProfile.Id,
                AppUserId = userProfile.AppUserId,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                TCKNo = userProfile.TCKNo,
                Salary = userProfile.Salary,
                BirthDate = userProfile.BirthDate,
                HireDate = userProfile.HireDate,
                SelectedRoleId = selectedRoleId,
                Roles = roleDtos.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = (r.Id == selectedRoleId)
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonelEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<AppRoleDTO> rolesForInvalid = await _appRoleManager.GetAllAsync();
                vm.Roles = rolesForInvalid.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = (r.Id == vm.SelectedRoleId)
                }).ToList();
                return View(vm);
            }

            AppUserProfileDTO originalProfileDto = await _appUserProfileManager.GetByIdAsync(vm.Id);
            if (originalProfileDto == null)
            {
                ModelState.AddModelError("", "Güncellenecek profil bulunamadı.");
                List<AppRoleDTO> rolesForNotFound = await _appRoleManager.GetAllAsync();
                vm.Roles = rolesForNotFound.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = (r.Id == vm.SelectedRoleId)
                }).ToList();
                return View(vm);
            }

            AppUserProfileDTO newProfileDto = new AppUserProfileDTO
            {
                Id = vm.Id,
                AppUserId = vm.AppUserId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                TCKNo = vm.TCKNo,
                Salary = vm.Salary,
                BirthDate = vm.BirthDate,
                HireDate = vm.HireDate
            };

            string result = await _appUserProfileManager.UpdateAsync(originalProfileDto, newProfileDto);

            if (result != "Success")
            {
                ModelState.AddModelError("", result);
                List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                vm.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = (r.Id == vm.SelectedRoleId)
                }).ToList();
                return View(vm);
            }

           
            List<AppUserRoleDTO> allUserRoles = await _appUserRoleManager.GetAllAsync();
            List<AppUserRoleDTO> existingRels = allUserRoles.Where(x => x.UserId == vm.AppUserId && x.Status != DataStatus.Deleted).ToList();

            int currentRoleId = existingRels.FirstOrDefault()?.RoleId ?? 0;

            if (currentRoleId != vm.SelectedRoleId)
            {
               
                foreach (AppUserRoleDTO rel in existingRels)
                {
                    await _appUserRoleManager.RemoveByUserAndRoleAsync(rel.UserId, rel.RoleId);
                }

                
                if (vm.SelectedRoleId > 0)
                {
                    AppUserRoleDTO newRel = new AppUserRoleDTO
                    {
                        UserId = vm.AppUserId,
                        RoleId = vm.SelectedRoleId,
                        InsertedDate = DateTime.Now,
                        Status = DataStatus.Inserted
                    };
                    await _appUserRoleManager.CreateAsync(newRel);
                }
            }

            return RedirectToAction("PersonelList");
        }
    }
}