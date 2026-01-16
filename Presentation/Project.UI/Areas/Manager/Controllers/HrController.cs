using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.InnerInfrastructure.ManagerConcretes;
using Project.UI.Areas.Manager.Models.AppUserVMs;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Project.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HrController : Controller
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly IAppRoleManager _appRoleManager;

        public HrController(IAppUserManager appUserManager, IAppUserProfileManager appUserProfileManager, IAppRoleManager appRoleManager)
        {
            _appUserManager = appUserManager;
            _appUserProfileManager = appUserProfileManager;
            _appRoleManager = appRoleManager;
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

        public async Task<IActionResult> Edit(int id)
        {
            AppUserDTO dto = await _appUserManager.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            PersonelEditVm vm = new PersonelEditVm
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Email = dto.Email,
                RoleIds = dto.RoleIds ?? new List<int>()


            };
            List<AppUserProfileDTO> profiles = await _appUserProfileManager.GetAllAsync();
            AppUserProfileDTO profile = profiles.FirstOrDefault(p => p.AppUserId == dto.Id);
            if (profile != null)
            {
                vm.FirstName = profile.FirstName;
                vm.LastName = profile.LastName;
                vm.Salary = profile.Salary;
                vm.HireDate = profile.HireDate;
                vm.BirthDate = profile.BirthDate;
            }
            await FillRolesSelectList(vm);

            return View(vm);
        }

        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, PersonelEditVm vm)
        {
            if (id != vm.Id)
            {
                return BadRequest();
            }

            
            await FillRolesSelectList(vm);

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUserDTO dto = new AppUserDTO
            {
                Id = vm.Id,
                UserName = vm.UserName,
                Email = vm.Email,
                RoleIds = vm.RoleIds
            };

            string result = await _appUserManager.UpdateAsync(id, dto);

         
            if (!string.IsNullOrEmpty(result) && result.StartsWith("Error"))
            {
                string[] messages = result.Replace("Error|", "").Split('|', StringSplitOptions.RemoveEmptyEntries);
                foreach (string m in messages)
                    ModelState.AddModelError(string.Empty, m);

               
                await FillRolesSelectList(vm);
                return View(vm);
            }

           
            List<AppUserProfileDTO> profiles = await _appUserProfileManager.GetAllAsync();
            AppUserProfileDTO profile = profiles.FirstOrDefault(p => p.AppUserId == dto.Id);
            if (profile != null)
            {
              
                if (!string.IsNullOrWhiteSpace(vm.FirstName))
                    profile.FirstName = vm.FirstName;

                if (!string.IsNullOrWhiteSpace(vm.LastName))
                    profile.LastName = vm.LastName;

                if (vm.Salary.HasValue)
                    profile.Salary = vm.Salary.Value;

                if (vm.HireDate.HasValue)
                    profile.HireDate = vm.HireDate.Value;

                if (vm.BirthDate.HasValue)
                    profile.BirthDate = vm.BirthDate.Value;

                await _appUserProfileManager.UpdateAsync(profile.Id, profile);
            }
            else
            {
                
            }

            return RedirectToAction("PersonelList");
        }
        private async Task FillRolesSelectList(PersonelEditVm vm)
        {
            vm.RolesSelectList = new List<SelectListItem>();

            if (_appRoleManager == null)
                return;

            var roles = await _appRoleManager.GetAllAsync() ?? new List<AppRoleDTO>();
            vm.RolesSelectList = roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),
                    Selected = vm.RoleIds != null && vm.RoleIds.Contains(r.Id)
                })
                .ToList();
        }




    }
}
