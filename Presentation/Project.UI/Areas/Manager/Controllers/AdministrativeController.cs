using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.InnerInfrastructure.ManagerConcretes;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.CategoryManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.UnitManagement;
using Project.UI.Areas.Manager.Models.HRVMs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class AdministrativeController : Controller
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppUserProfileManager _appUserProfileManager;
        private readonly IAppUserRoleManager _appUserRoleManager;
        private readonly IAppRoleManager _appRoleManager;
        private readonly IUnitManager _unitManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IProductManager _productManager;

        public AdministrativeController(IAppUserManager appUserManager, IAppUserProfileManager appUserProfileManager, IAppUserRoleManager appUserRoleManager, IAppRoleManager appRoleManager, IUnitManager unitManager, ICategoryManager categoryManager, IProductManager productManager)
        {
            _appUserManager = appUserManager;
            _appUserProfileManager = appUserProfileManager;
            _appUserRoleManager = appUserRoleManager;
            _appRoleManager = appRoleManager;
            _unitManager = unitManager;
            _categoryManager = categoryManager;
            _productManager = productManager;
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
                PersonnelFilterType.Mutfak => new[] { "Mutfak Sefi", "Ascı", "Ascı Yardımcısı" },
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
        private List<int> GetAllCategoryIdsRecursive(List<CategoryDTO> categories, int parentId)
        {
            List<int> ids = new List<int> { parentId };
            List<int> children = categories.Where(c => c.ParentCategoryId == parentId).Select(c => c.Id).ToList();
            foreach (int childId in children)
            {
                ids.AddRange(GetAllCategoryIdsRecursive(categories, childId));
            }
            return ids;
        }

        [HttpGet]
        public async Task<IActionResult> ProductManagement(
     int? categoryId = null,
     string searchTerm = null,
     bool? isSellable = null,
     bool? isExtra = null,
     bool? canBeProduced = null,
     bool? isReadyMade = null)
        {
            List<ProductDTO> products = await _productManager.GetAllAsync();
            List<CategoryDTO> categories = await _categoryManager.GetAllAsync();


            List<int> categoryIds = new();
            if (categoryId.HasValue)
            {
                categoryIds = GetAllCategoryIdsRecursive(categories, categoryId.Value);
            }


            ViewBag.SellableValues = products.Select(p => p.IsSellable).Distinct().ToList();
            ViewBag.ExtraValues = products.Select(p => p.IsExtra).Distinct().ToList();
            ViewBag.CanBeProducedValues = products.Select(p => p.CanBeProduced).Distinct().ToList();
            ViewBag.IsReadyMadeValues = products.Select(p => p.IsReadyMade).Distinct().ToList();


            if (categoryId.HasValue)
                products = products.Where(p => categoryIds.Contains(p.CategoryId)).ToList();
            if (!string.IsNullOrWhiteSpace(searchTerm))
                products = products.Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            if (isSellable.HasValue)
                products = products.Where(p => p.IsSellable == isSellable.Value).ToList();
            if (isExtra.HasValue)
                products = products.Where(p => p.IsExtra == isExtra.Value).ToList();
            if (canBeProduced.HasValue)
                products = products.Where(p => p.CanBeProduced == canBeProduced.Value).ToList();
            if (isReadyMade.HasValue)
                products = products.Where(p => p.IsReadyMade == isReadyMade.Value).ToList();

            ProductListVm vm = new ProductListVm
            {
                Products = products,
                Categories = categories,
                SelectedCategoryId = categoryId,
                SearchTerm = searchTerm,
                IsSellable = isSellable,
                IsExtra = isExtra,
                CanBeProduced = canBeProduced,
                IsReadyMade = isReadyMade
            };

            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> ProductTablePartial(
            int? categoryId = null,
            string searchTerm = null,
            bool? isSellable = null,
            bool? isExtra = null,
            bool? canBeProduced = null,
            bool? isReadyMade = null)
        {
            List<ProductDTO> products = await _productManager.GetAllAsync();
            List<CategoryDTO> categories = await _categoryManager.GetAllAsync();

            List<int> categoryIds = new();
            if (categoryId.HasValue)
            {
                categoryIds = GetAllCategoryIdsRecursive(categories, categoryId.Value);
            }

            if (categoryId.HasValue)
                products = products.Where(p => categoryIds.Contains(p.CategoryId)).ToList();
            if (!string.IsNullOrWhiteSpace(searchTerm))
                products = products.Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            if (isSellable.HasValue)
                products = products.Where(p => p.IsSellable == isSellable.Value).ToList();
            if (isExtra.HasValue)
                products = products.Where(p => p.IsExtra == isExtra.Value).ToList();
            if (canBeProduced.HasValue)
                products = products.Where(p => p.CanBeProduced == canBeProduced.Value).ToList();
            if (isReadyMade.HasValue)
                products = products.Where(p => p.IsReadyMade == isReadyMade.Value).ToList();

            ProductListVm vm = new ProductListVm
            {
                Products = products
            };

            return PartialView("ProductTablePartial", vm);
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            ProductDTO product = await _productManager.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ProductDetailVm vm = new ProductDetailVm
            {
                Id = product.Id,
                ProductName = product.ProductName,
                IsSellable = product.IsSellable,
                IsExtra = product.IsExtra,
                CanBeProduced = product.CanBeProduced,
                IsReadyMade = product.IsReadyMade,
                UnitPrice = product.UnitPrice,
                UnitId = product.UnitId,
                UnitName = product.UnitName,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                InsertedDate = product.InsertedDate,
                UpdatedDate = product.UpdatedDate,
                DeletionDate = product.DeletionDate,
                Status = product.Status.ToString()
            };
            return View(vm);
        }



        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
            List<UnitDTO> units = await _unitManager.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
            ViewBag.UnitList = new SelectList(units, "Id", "UnitName");
            return View(new ProductCreateVm());
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");
                return View(vm);
            }

            ProductDTO dto = new ProductDTO
            {
                ProductName = vm.ProductName,
                UnitPrice = vm.UnitPrice,
                UnitId = vm.UnitId,
                CategoryId = vm.CategoryId,
                IsSellable = vm.IsSellable,
                IsExtra = vm.IsExtra,
                CanBeProduced = vm.CanBeProduced,
                IsReadyMade = vm.IsReadyMade
            };

            OperationStatus result = await _productManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Ürün eklenemedi.");
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");
                return View(vm);
            }

            TempData["Success"] = "Ürün başarıyla eklendi.";
            return RedirectToAction("ProductManagement");
        }
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            ProductDTO product = await _productManager.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ProductEditVm vm = new ProductEditVm
            {
                Id = product.Id,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitId = product.UnitId,
                CategoryId = product.CategoryId,
                IsSellable = product.IsSellable,
                IsExtra = product.IsExtra,
                CanBeProduced = product.CanBeProduced,
                IsReadyMade = product.IsReadyMade,
                Status = product.Status.ToString()
            };

            List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
            List<UnitDTO> units = await _unitManager.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName", product.CategoryId);
            ViewBag.UnitList = new SelectList(units, "Id", "UnitName", product.UnitId);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName", vm.CategoryId);
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName", vm.UnitId);
                return View(vm);
            }

            ProductDTO original = await _productManager.GetByIdAsync(vm.Id);
            if (original == null)
                return NotFound();

            ProductDTO updated = new ProductDTO
            {
                Id = vm.Id,
                ProductName = vm.ProductName,
                UnitPrice = vm.UnitPrice,
                UnitId = vm.UnitId,
                CategoryId = vm.CategoryId,
                IsSellable = vm.IsSellable,
                IsExtra = vm.IsExtra,
                CanBeProduced = vm.CanBeProduced,
                IsReadyMade = vm.IsReadyMade
            };

            OperationStatus result = await _productManager.UpdateAsync(original, updated);
            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Ürün güncellenemedi.");
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName", vm.CategoryId);
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName", vm.UnitId);
                return View(vm);
            }

            TempData["Success"] = "Ürün başarıyla güncellendi.";
            return RedirectToAction("ProductManagement");
        }




        [HttpPost]
        public async Task<IActionResult> ActivateProduct(int id)
        {
            ProductDTO original = await _productManager.GetByIdAsync(id);
            if (original == null)
            {
                TempData["Error"] = "Ürün bulunamadı.";
                return RedirectToAction("ProductManagement");
            }

            ProductDTO updated = new ProductDTO
            {
                Id = original.Id,
                ProductName = original.ProductName,
                UnitPrice = original.UnitPrice,
                UnitId = original.UnitId,
                CategoryId = original.CategoryId,
                IsSellable = original.IsSellable,
                IsExtra = original.IsExtra,
                CanBeProduced = original.CanBeProduced,
                IsReadyMade = original.IsReadyMade,
                Status = Domain.Enums.DataStatus.Updated,
                InsertedDate = original.InsertedDate,
                UpdatedDate = DateTime.Now,
                DeletionDate = null,
                UnitName = original.UnitName,
                CategoryName = original.CategoryName
            };

            OperationStatus result = await _productManager.UpdateAsync(original, updated);
            if (result != OperationStatus.Success)
                TempData["Error"] = "Ürün aktifleştirilemedi.";
            else
                TempData["Success"] = "Ürün aktifleştirildi.";
            return RedirectToAction("ProductManagement");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteProduct(int id)
        {
            OperationStatus result = await _productManager.SoftDeleteByIdAsync(id);
            if (result != OperationStatus.Success)
                TempData["Error"] = "Ürün pasife alınamadı.";
            else
                TempData["Success"] = "Ürün pasife alındı.";
            return RedirectToAction("ProductManagement");
        }

        [HttpPost]
        public async Task<IActionResult> HardDeleteProduct(int id)
        {
            OperationStatus result = await _productManager.HardDeleteByIdAsync(id);
            if (result != OperationStatus.Success)
                TempData["Error"] = "Ürün silinemedi. İlişkili kayıtlar olabilir.";
            else
                TempData["Success"] = "Ürün kalıcı olarak silindi.";
            return RedirectToAction("ProductManagement");
        }
        [HttpGet]
        public async Task<IActionResult> UnitManagement()
        {
            List<UnitDTO> allUnits = await _unitManager.GetAllAsync();

            UnitListVm vm = new UnitListVm
            {
                Units = allUnits.Select(u => new UnitVm
                {
                    Id = u.Id,
                    UnitName = u.UnitName,
                    UnitAbbreviation = u.UnitAbbreviation
                }).ToList()
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult AddUnit()
        {
            return View(new UnitCreateVm());
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(UnitCreateVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            UnitDTO dto = new UnitDTO
            {
                UnitName = vm.UnitName,
                UnitAbbreviation = vm.UnitAbbreviation
            };

            OperationStatus result = await _unitManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Birim eklenemedi.");
                return View(vm);
            }

            TempData["Success"] = "Birim başarıyla eklendi.";
            return RedirectToAction("UnitManagement");
        }

        [HttpGet]
        public async Task<IActionResult> EditUnit(int id)
        {
            UnitDTO unit = await _unitManager.GetByIdAsync(id);
            if (unit == null)
                return NotFound();

            UnitEditVm vm = new UnitEditVm
            {
                Id = unit.Id,
                UnitName = unit.UnitName,
                UnitAbbreviation = unit.UnitAbbreviation
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUnit(UnitEditVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            UnitDTO original = await _unitManager.GetByIdAsync(vm.Id);
            if (original == null)
                return NotFound();

            UnitDTO updated = new UnitDTO
            {
                Id = vm.Id,
                UnitName = vm.UnitName,
                UnitAbbreviation = vm.UnitAbbreviation
            };

            OperationStatus result = await _unitManager.UpdateAsync(original, updated);
            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Birim güncellenemedi.");
                return View(vm);
            }

            TempData["Success"] = "Birim başarıyla güncellendi.";
            return RedirectToAction("UnitManagement");
        }

        [HttpGet]
        public async Task<IActionResult> UnitDetail(int id)
        {
            UnitDTO unit = await _unitManager.GetByIdAsync(id);
            if (unit == null)
                return NotFound();

            UnitDetailVm vm = new UnitDetailVm
            {
                Id = unit.Id,
                UnitName = unit.UnitName,
                UnitAbbreviation = unit.UnitAbbreviation

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            OperationStatus result = await _unitManager.HardDeleteByIdAsync(id);
            if (result != OperationStatus.Success)
                TempData["Error"] = "Birim silinemedi. İlişkili kayıtlar olabilir.";
            else
                TempData["Success"] = "Birim başarıyla silindi.";

            return RedirectToAction("UnitManagement");
        }
        [HttpGet]
        public async Task<IActionResult> CategoryManagement(int? parentCategoryId = null)
        {
            List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
            List<CategoryVm> parentCategories = allCategories
                .Where(c => c.ParentCategoryId == null)
                .Select(c => new CategoryVm
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    ParentCategoryId = c.ParentCategoryId,
                    ParentCategoryName = null,
                    SubCategoryCount = allCategories.Count(x => x.ParentCategoryId == c.Id),
                    ProductCount = 0
                }).ToList();

            List<CategoryDTO> filteredCategories = allCategories;
            if (parentCategoryId.HasValue)
            {
                List<int> categoryIds = GetAllCategoryIdsRecursive(allCategories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    ParentCategoryId = x.ParentCategoryId
                }).ToList(), parentCategoryId.Value);

                filteredCategories = allCategories.Where(c => categoryIds.Contains(c.Id)).ToList();
            }

            List<CategoryVm> categoryVms = filteredCategories.Select(c => new CategoryVm
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Description = c.Description,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategoryName = allCategories.FirstOrDefault(x => x.Id == c.ParentCategoryId)?.CategoryName,
                SubCategoryCount = allCategories.Count(x => x.ParentCategoryId == c.Id),
                ProductCount = 0
            }).ToList();

            CategoryListVm vm = new CategoryListVm
            {
                Categories = categoryVms,
                SelectedParentCategoryId = parentCategoryId,
                ParentCategories = parentCategories
            };

            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> CategoryTablePartial(int? parentCategoryId = null)
        {
            List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
            List<CategoryDTO> filteredCategories = allCategories;
            if (parentCategoryId.HasValue)
            {
                List<int> categoryIds = GetAllCategoryIdsRecursive(allCategories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    ParentCategoryId = x.ParentCategoryId
                }).ToList(), parentCategoryId.Value);

                filteredCategories = allCategories.Where(c => categoryIds.Contains(c.Id)).ToList();
            }

            List<CategoryVm> categoryVms = filteredCategories.Select(c => new CategoryVm
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Description = c.Description,
                ParentCategoryId = c.ParentCategoryId,
                ParentCategoryName = allCategories.FirstOrDefault(x => x.Id == c.ParentCategoryId)?.CategoryName,
                SubCategoryCount = allCategories.Count(x => x.ParentCategoryId == c.Id),
                ProductCount = 0
            }).ToList();

            return PartialView("CategoryTablePartial", categoryVms);
        }


        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
            List<SelectListItem> parentCategoryList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Üst Kategori Seçme" }
            };
            parentCategoryList.AddRange(allCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }));

            CategoryCreateVm vm = new CategoryCreateVm
            {
                ParentCategoryList = parentCategoryList
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
                List<SelectListItem> parentCategoryList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "Üst Kategori Seçme" }
                };
                parentCategoryList.AddRange(allCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }));
                vm.ParentCategoryList = parentCategoryList;
                return View(vm);
            }

            CategoryDTO dto = new CategoryDTO
            {
                CategoryName = vm.CategoryName,
                Description = vm.Description,
                ParentCategoryId = vm.ParentCategoryId
            };

            OperationStatus result = await _categoryManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Kategori eklenemedi. Lütfen tüm alanları kontrol edin.");
                List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
                List<SelectListItem> parentCategoryList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "Üst Kategori Seçme" }
                };
                parentCategoryList.AddRange(allCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }));
                vm.ParentCategoryList = parentCategoryList;
                return View(vm);
            }

            TempData["Success"] = "Kategori başarıyla eklendi.";
            return RedirectToAction("CategoryManagement");
        }


        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            CategoryDTO category = await _categoryManager.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
            List<SelectListItem> parentCategoryList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Üst Kategori Seçme" }
            };
            parentCategoryList.AddRange(allCategories
                .Where(c => c.Id != id)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }));

            CategoryEditVm vm = new CategoryEditVm
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryList = parentCategoryList
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryEditVm vm)
        {
            if (vm == null || !ModelState.IsValid)
            {
                return NotFound();
            }

            CategoryDTO original = await _categoryManager.GetByIdAsync(vm.Id);
            if (original == null)
                return NotFound();

            CategoryDTO updated = new CategoryDTO
            {
                Id = vm.Id,
                CategoryName = vm.CategoryName,
                Description = vm.Description,
                ParentCategoryId = vm.ParentCategoryId
            };

            OperationStatus result = await _categoryManager.UpdateAsync(original, updated);
            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Kategori güncellenemedi. Lütfen tüm alanları kontrol edin.");
                List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
                List<SelectListItem> parentCategoryList = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Üst Kategori Seçme" }
        };
                parentCategoryList.AddRange(allCategories
                    .Where(c => c.Id != vm.Id)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.CategoryName
                    }));
                vm.ParentCategoryList = parentCategoryList;
                return View(vm);
            }

            TempData["Success"] = "Kategori başarıyla güncellendi.";
            return RedirectToAction("CategoryManagement");
        }


        [HttpGet]
        public async Task<IActionResult> CategoryDetail(int id)
        {
            List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
            CategoryDTO category = allCategories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            List<CategoryVm> subCategories = allCategories
                .Where(c => c.ParentCategoryId == id)
                .Select(c => new CategoryVm
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    ParentCategoryId = c.ParentCategoryId,
                    ParentCategoryName = category.CategoryName,
                    SubCategoryCount = allCategories.Count(x => x.ParentCategoryId == c.Id),
                    ProductCount = 0
                }).ToList();

            List<CategoryVm> parentCategories = new List<CategoryVm>();
            int? parentId = category.ParentCategoryId;
            while (parentId.HasValue)
            {
                CategoryDTO parent = allCategories.FirstOrDefault(c => c.Id == parentId.Value);
                if (parent != null)
                {
                    parentCategories.Add(new CategoryVm
                    {
                        Id = parent.Id,
                        CategoryName = parent.CategoryName,
                        Description = parent.Description,
                        ParentCategoryId = parent.ParentCategoryId,
                        ParentCategoryName = allCategories.FirstOrDefault(x => x.Id == parent.ParentCategoryId)?.CategoryName,
                        SubCategoryCount = allCategories.Count(x => x.ParentCategoryId == parent.Id),
                        ProductCount = 0
                    });
                    parentId = parent.ParentCategoryId;
                }
                else
                {
                    break;
                }
            }

            CategoryDetailVm vm = new CategoryDetailVm
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                ParentCategoryName = allCategories.FirstOrDefault(c => c.Id == category.ParentCategoryId)?.CategoryName,
                SubCategories = subCategories,
                ParentCategories = parentCategories
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            List<CategoryDTO> allCategories = await _categoryManager.GetAllAsync();
            bool hasSubCategory = allCategories.Any(c => c.ParentCategoryId == id);
            if (hasSubCategory)
            {
                TempData["Error"] = "Alt kategorisi olan bir kategori silinemez.";
                return RedirectToAction("CategoryManagement");
            }

            List<ProductDTO> allProducts = await _productManager.GetAllAsync();
            bool hasProduct = allProducts.Any(p => p.CategoryId == id);
            if (hasProduct)
            {
                TempData["Error"] = "Ürünü olan bir kategori silinemez.";
                return RedirectToAction("CategoryManagement");
            }

            OperationStatus result = await _categoryManager.SoftDeleteByIdAsync(id);
            if (result != OperationStatus.Success)
            {
                TempData["Error"] = "Kategori silinemedi.";
            }
            else
            {
                TempData["Success"] = "Kategori başarıyla silindi.";
            }
            return RedirectToAction("CategoryManagement");
        }




    }
}

