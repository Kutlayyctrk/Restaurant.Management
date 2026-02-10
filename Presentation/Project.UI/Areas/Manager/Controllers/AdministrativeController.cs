using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Diagnostics;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Application.Managers;
using Project.Domain.Enums;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.CategoryManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.MenuProductManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.OrderManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.PersonnelManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.SupplierManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.TableManagement;
using Project.UI.Areas.Manager.Models.AdministrativeVMs.UnitManagement;



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
        private readonly IMenuManager _menuManager;
        private readonly IMenuProductManager _menuProductManager;
        private readonly IOrderManager _orderManager;
        private readonly IOrderDetailManager _orderDetailManager;
        private readonly ISupplierManager _supplierManager;
        private readonly ITableManager _tableManager;

        public AdministrativeController(IAppUserManager appUserManager, IAppUserProfileManager appUserProfileManager, IAppUserRoleManager appUserRoleManager, IAppRoleManager appRoleManager, IUnitManager unitManager, ICategoryManager categoryManager, IProductManager productManager, IMenuManager menuManager, IMenuProductManager menuProductManager, IOrderManager orderManager, IOrderDetailManager orderDetailManager, ISupplierManager supplierManager, ITableManager tableManager)
        {
            _appUserManager = appUserManager;
            _appUserProfileManager = appUserProfileManager;
            _appUserRoleManager = appUserRoleManager;
            _appRoleManager = appRoleManager;
            _unitManager = unitManager;
            _categoryManager = categoryManager;
            _productManager = productManager;
            _menuManager = menuManager;
            _menuProductManager = menuProductManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
            _supplierManager = supplierManager;
            _tableManager = tableManager;
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
                PersonnelFilterType.Mutfak => new[] { "Mutfak Sefi", "Ascý", "Ascý Yardýmcýsý" },
                PersonnelFilterType.Salon => new[] { "Garson", "Komi" },
                PersonnelFilterType.Yönetim => new[] { "Insan Kaynaklari Muduru", "Restaurant Muduru", "Idari Personel" },
                PersonnelFilterType.Hizmet => new[] { "Temizlikçi", "Bulaþýkçý" },
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

            Result userResult = await _appUserManager.CreateAsync(userDto);
            if (!userResult.IsSuccess)
            {
                ModelState.AddModelError("", "Kullanýcý eklenemedi. Lütfen kullanýcý adý, email ve þifreyi kontrol edin.");
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
                ModelState.AddModelError("", "Kullanýcý eklenmiþ gibi görünüyor ama bulunamadý. Lütfen tekrar deneyin.");
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
            Result profileResult = await _appUserProfileManager.CreateAsync(profileDto);
            if (!profileResult.IsSuccess)
            {
                ModelState.AddModelError("", "Profil eklenemedi. Lütfen tüm alanlarý kontrol edin.");
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
                    ModelState.AddModelError("", "Bu kullanýcýya bu rol zaten atanmýþ.");
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
                Result roleResult = await _appUserRoleManager.CreateAsync(roleDto);
                if (!roleResult.IsSuccess)
                {
                    ModelState.AddModelError("", "Rol atanamadý. Lütfen rol seçimini kontrol edin.");
                    List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                    vm.Roles = roles.Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList();
                    return View(vm);
                }
            }

            TempData["Success"] = "Personel baþarýyla eklendi.";
            return RedirectToAction("PersonnelManagement");
        }
        [HttpGet]
        public async Task<IActionResult> EditPersonnel(int id)
        {
            AppUserDTO user = await _appUserManager.GetByIdAsync(id);
            AppUserProfileDTO profile = (await _appUserProfileManager.GetAllAsync()).FirstOrDefault(p => p.AppUserId == id);
            List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
            List<AppUserRoleDTO> userRoles = await _appUserRoleManager.GetActives();

            if (user == null || profile == null)
                return NotFound();

            int? selectedRoleId = userRoles.FirstOrDefault(ur => ur.UserId == id)?.RoleId;

            EditPersonnelVm vm = new EditPersonnelVm
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                TCKNo = profile.TCKNo,
                Salary = profile.Salary,
                HireDate = profile.HireDate,
                BirthDate = profile.BirthDate,
                SelectedRoleId = selectedRoleId ?? 0,
                Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = r.Id == selectedRoleId
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonnel(EditPersonnelVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<AppRoleDTO> roles = await _appRoleManager.GetAllAsync();
                vm.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = r.Id == vm.SelectedRoleId
                }).ToList();
                return View(vm);
            }

            AppUserDTO user = await _appUserManager.GetByIdAsync(vm.Id);
            AppUserProfileDTO profile = (await _appUserProfileManager.GetAllAsync()).FirstOrDefault(p => p.AppUserId == vm.Id);
            if (user == null || profile == null)
                return NotFound();


            user.Email = vm.Email;
            user.UserName = vm.UserName;
            await _appUserManager.UpdateAsync(user, user);

            profile.FirstName = vm.FirstName;
            profile.LastName = vm.LastName;
            profile.TCKNo = vm.TCKNo;
            profile.Salary = vm.Salary;
            profile.HireDate = vm.HireDate;
            profile.BirthDate = vm.BirthDate;
            await _appUserProfileManager.UpdateAsync(profile, profile);


            List<AppUserRoleDTO> userRoles = await _appUserRoleManager.GetActives();
            AppUserRoleDTO currentRole = userRoles.FirstOrDefault(ur => ur.UserId == vm.Id);
            if (currentRole != null && currentRole.RoleId != vm.SelectedRoleId)
            {

                await _appUserRoleManager.HardDeleteByCompositeKeyAsync(currentRole.UserId, currentRole.RoleId);
                if (vm.SelectedRoleId > 0)
                {
                    await _appUserRoleManager.CreateAsync(new AppUserRoleDTO
                    {
                        UserId = vm.Id,
                        RoleId = vm.SelectedRoleId
                    });
                }
            }
            else if (currentRole == null && vm.SelectedRoleId > 0)
            {
                await _appUserRoleManager.CreateAsync(new AppUserRoleDTO
                {
                    UserId = vm.Id,
                    RoleId = vm.SelectedRoleId
                });
            }

            TempData["Success"] = "Personel baþarýyla güncellendi.";
            return RedirectToAction("PersonnelManagement");
        }
        [HttpPost]
        
        public async Task<IActionResult> SoftDeletePersonnel(int id)
        {
            Result result = await _appUserManager.SoftDeleteByIdAsync(id);
            if (result.IsSuccess)
            {
                TempData["Success"] = "Personel pasife alýndý.";
            }
            else
            {
                TempData["Error"] = "Personel pasife alýnamadý.";
            }
            return RedirectToAction("PersonnelManagement");
        }
        [HttpPost]
        public async Task<IActionResult> HardDeletePersonnel(int id)
        {
            Result result = await _appUserManager.HardDeleteByIdAsync(id);
            if (result.IsSuccess)
            {
                TempData["Success"] = "Personel kalýcý olarak silindi.";
            }
            else
            {
                TempData["Error"] = "Personel silinemedi.";
            }
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

            Result result = await _appRoleManager.CreateAsync(dto);

            if (result.IsSuccess)
            {
                TempData["Success"] = "Rol baþarýyla eklendi.";
                return RedirectToAction("PersonnelManagement");
            }

            ModelState.AddModelError("", result.Status == OperationStatus.AlreadyExists
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
                    TempData["Error"] = "Silinmek istenen rol bulunamadý.";
                    return RedirectToAction("DeleteRole");
                }
                Result result = await _appRoleManager.SoftDeleteByIdAsync(role.Id);

                result = await _appRoleManager.HardDeleteByIdAsync(role.Id);

                if (result.IsSuccess)
                {
                    TempData["Success"] = "Rol baþarýyla silindi.";
                }
                else if (result.Status == OperationStatus.Failed)
                {
                    TempData["Error"] = "Rol silinemedi. Bu role baðlý kullanýcýlar olabilir.";
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

            decimal severancePay = ((DateTime.Now - profile.HireDate).Days / 365) * profile.Salary;
            int noticePeriods = ((DateTime.Now - profile.HireDate).Days) / 182;
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
            List<ProductDTO> products = await _productManager.GetWithCategory();
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
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreateVm vm)
        {
         
            if (!ModelState.IsValid || vm.CategoryId < 1 || vm.UnitId < 1)
            {
                if (vm.CategoryId < 1)
                    ModelState.AddModelError("CategoryId", "Kategori seçimi zorunludur.");
                if (vm.UnitId < 1)
                    ModelState.AddModelError("UnitId", "Birim seçimi zorunludur.");

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

            Result result = await _productManager.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Ürün eklenemedi.");
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");
                return View(vm);
            }

            TempData["Success"] = "Ürün baþarýyla eklendi.";
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
                ViewBag.CategoryList = new SelectList(categories, "Id","CategoryName", vm.CategoryId);
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

            Result result = await _productManager.UpdateAsync(original, updated);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Ürün güncellenemedi.");
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName", vm.CategoryId);
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName", vm.UnitId);
                return View(vm);
            }

            TempData["Success"] = "Ürün baþarýyla güncellendi.";
            return RedirectToAction("ProductManagement");
        }




        [HttpPost]
        public async Task<IActionResult> ActivateProduct(int id)
        {
            ProductDTO original = await _productManager.GetByIdAsync(id);
            if (original == null)
            {
                TempData["Error"] = "Ürün bulunamadý.";
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

            Result result = await _productManager.UpdateAsync(original, updated);
            if (!result.IsSuccess)
                TempData["Error"] = "Ürün aktifleþtirilemedi.";
            else
                TempData["Success"] = "Ürün aktifleþtirildi.";
            return RedirectToAction("ProductManagement");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteProduct(int id)
        {
            Result result = await _productManager.SoftDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Ürün pasife alýnamadý.";
            else
                TempData["Success"] = "Ürün pasife alýndý.";
            return RedirectToAction("ProductManagement");
        }

        [HttpPost]
        public async Task<IActionResult> HardDeleteProduct(int id)
        {
            Result result = await _productManager.HardDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Ürün silinemedi. Ýliþkili kayýtlar olabilir.";
            else
                TempData["Success"] = "Ürün kalýcý olarak silindi.";
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

            Result result = await _unitManager.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Birim eklenemedi.");
                return View(vm);
            }

            TempData["Success"] = "Birim baþarýyla eklendi.";
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

            Result result = await _unitManager.UpdateAsync(original, updated);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Birim güncellenemedi.");
                return View(vm);
            }

            TempData["Success"] = "Birim baþarýyla güncellendi.";
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
        public async Task<IActionResult> SoftDeleteUnit(int id)
        {
            Result result = await _unitManager.SoftDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Birim silinemedi. Ýliþkili kayýtlar olabilir.";
            else
                TempData["Success"] = "Birim baþarýyla Pasife Alýndý.";

            return RedirectToAction("UnitManagement");
        }

        [HttpPost]
        public async Task<IActionResult> HardDeleteUnit(int id)
        {
            Result result = await _unitManager.HardDeleteByIdAsync(id);
            if(!result.IsSuccess)
            {
                TempData["Error"] = "Birim Pasife alýnamadý";
            }
            TempData["Success"] = "Birim  baþarýyla Silindi.";
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

            Result result = await _categoryManager.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Kategori eklenemedi. Lütfen tüm alanlarý kontrol edin.");
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

            TempData["Success"] = "Kategori baþarýyla eklendi.";
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

            Result result = await _categoryManager.UpdateAsync(original, updated);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Kategori güncellenemedi. Lütfen tüm alanlarý kontrol edin.");
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

            TempData["Success"] = "Kategori baþarýyla güncellendi.";
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
       public async Task<IActionResult> SofteDeleteCategory(int id)
        {
            Result result = await _categoryManager.SoftDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Kategori Pasife alýnamadý.";
            else
                TempData["Success"] = "Kategori baþarýyla Pasife alýndý.";
            return RedirectToAction("CategoryManagement");
        }

        [HttpPost]
        public async Task<IActionResult> HardDeleteCategory(int id)
        {
            Result result = await _categoryManager.HardDeleteByIdAsync(id);
            if(!result.IsSuccess)
            {
                TempData["Error"] = "Kategori Silinemedi";

            }
            TempData["Success"] = "Kategori Silme baþarýlý";
            return RedirectToAction("CategoryManagement");
        }

        [HttpGet]
        public async Task<IActionResult> MenuManagement()
        {
            List<MenuDTO> menus = await _menuManager.GetAllAsync();
            MenuListVm vm = new MenuListVm
            {
                Menus = menus.Select(m => new MenuVm
                {
                    Id = m.Id,
                    MenuName = m.MenuName,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    IsActive = m.IsActive
                }).ToList()
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult AddMenu()
        {
            return View(new MenuCreateVm());
        }

        [HttpPost]
        public async Task<IActionResult> AddMenu(MenuCreateVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            MenuDTO dto = new MenuDTO
            {
                MenuName = vm.MenuName,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                IsActive = vm.IsActive
            };

            Result result = await _menuManager.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Menü eklenemedi. Lütfen tüm alanlarý kontrol edin.");
                return View(vm);
            }

            TempData["Success"] = "Menü baþarýyla eklendi.";
            return RedirectToAction("MenuManagement");
        }

        [HttpGet]
        public async Task<IActionResult> EditMenu(int id)
        {
            MenuDTO menu = await _menuManager.GetByIdAsync(id);
            if (menu == null)
                return NotFound();

            MenuEditVm vm = new MenuEditVm
            {
                Id = menu.Id,
                MenuName = menu.MenuName,
                StartDate = menu.StartDate,
                EndDate = menu.EndDate,
                IsActive = menu.IsActive
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditMenu(MenuEditVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            MenuDTO original = await _menuManager.GetByIdAsync(vm.Id);
            if (original == null)
                return NotFound();

            MenuDTO updated = new MenuDTO
            {
                Id = vm.Id,
                MenuName = vm.MenuName,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                IsActive = vm.IsActive
            };

            Result result = await _menuManager.UpdateAsync(original, updated);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Menü güncellenemedi. Lütfen tüm alanlarý kontrol edin.");
                return View(vm);
            }

            TempData["Success"] = "Menü baþarýyla güncellendi.";
            return RedirectToAction("MenuManagement");
        }

        [HttpPost]
        public async Task <IActionResult> HardDeleteMenu(int id)
        {
            Result result = await _menuManager.HardDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Menü silinemedi.";
            else
                TempData["Success"] = "Menü baþarýyla silindi.";
            return RedirectToAction("MenuManagement");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteMenu(int id)
        {
            Result result = await _menuManager.SoftDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Menü pasife alýnamadý.";
            else
                TempData["Success"] = "Menü baþarýyla pasife alýndý.";
            return RedirectToAction("MenuManagement");
        }
       

        [HttpGet]
        public async Task<IActionResult> MenuDetail(int id)
        {
            MenuDTO menu = await _menuManager.GetByIdAsync(id);
            if (menu == null)
            {

                return NotFound();
            }

            List<MenuProductDTO> menuProducts = await _menuProductManager.WhereAsync(mp => mp.MenuId == id);

            MenuDetailVm vm = new MenuDetailVm
            {
                Id = menu.Id,
                MenuName = menu.MenuName,
                StartDate = menu.StartDate,
                EndDate = menu.EndDate,
                IsActive = menu.IsActive,
                MenuProducts = menuProducts.Select(mp => new MenuProductInMenuDetailVm
                {
                    Id = mp.Id,
                    ProductName = mp.ProductName,
                    UnitPrice = mp.UnitPrice,
                    IsActive = mp.IsActive
                }).ToList()
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> MenuProductManagement(int? SelectedMenuId = null, string SearchTerm = null)
        {
            List<MenuProductDTO> menuProducts = await _menuProductManager.GetWithMenuAndProduct();
            List<MenuDTO> menus = await _menuManager.GetAllAsync();

            if (SelectedMenuId.HasValue)
                menuProducts = menuProducts.Where(mp => mp.MenuId == SelectedMenuId.Value).ToList();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
                menuProducts = menuProducts.Where(mp => mp.ProductName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            MenuProductListVm vm = new MenuProductListVm
            {
                MenuProducts = menuProducts.Select(mp => new MenuProductVm
                {
                    Id = mp.Id,
                    MenuName = mp.MenuName,
                    ProductName = mp.ProductName,
                    CategoryName=mp.CategoryName,
                    
                    UnitPrice = mp.UnitPrice,
                    IsActive = mp.IsActive
                }).ToList(),
                SelectedMenuId = SelectedMenuId,
                MenuList = menus.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.MenuName
                }).ToList(),
                SearchTerm = SearchTerm
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddMenuProduct()
        {
            List<MenuDTO> menus = await _menuManager.GetAllAsync();
            List<ProductDTO> products = await _productManager.GetAllAsync();

            MenuProductCreateVm vm = new MenuProductCreateVm
            {
                MenuList = menus.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.MenuName
                }).ToList(),
                ProductList = products.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.ProductName
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuProduct(MenuProductCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<MenuDTO> menus = await _menuManager.GetAllAsync();
                List<ProductDTO> products = await _productManager.GetAllAsync();
                vm.MenuList = menus.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.MenuName }).ToList();
                vm.ProductList = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList();
                return View(vm);
            }

            MenuProductDTO dto = new MenuProductDTO
            {
                MenuId = vm.MenuId,
                ProductId = vm.ProductId,
                UnitPrice = vm.UnitPrice,
                IsActive = vm.IsActive
            };

            Result result = await _menuProductManager.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Menü ürünü eklenemedi.");
                List<MenuDTO> menus = await _menuManager.GetAllAsync();
                List<ProductDTO> products = await _productManager.GetAllAsync();
                vm.MenuList = menus.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.MenuName }).ToList();
                vm.ProductList = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList();
                return View(vm);
            }

            TempData["Success"] = "Menü ürünü baþarýyla eklendi.";
            return RedirectToAction("MenuProductManagement");
        }

        [HttpGet]
        public async Task<IActionResult> EditMenuProduct(int id)
        {
            MenuProductDTO menuProduct = await _menuProductManager.GetByIdAsync(id);
            if (menuProduct == null)
                return NotFound();

            List<MenuDTO> menus = await _menuManager.GetAllAsync();
            List<ProductDTO> products = await _productManager.GetAllAsync();

            MenuProductEditVm vm = new MenuProductEditVm
            {
                Id = menuProduct.Id,
                MenuId = menuProduct.MenuId,
                ProductId = menuProduct.ProductId,
                UnitPrice = menuProduct.UnitPrice,
                IsActive = menuProduct.IsActive,
                MenuList = menus.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.MenuName }).ToList(),
                ProductList = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditMenuProduct(MenuProductEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<MenuDTO> menus = await _menuManager.GetAllAsync();
                List<ProductDTO> products = await _productManager.GetAllAsync();
                vm.MenuList = menus.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.MenuName }).ToList();
                vm.ProductList = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList();
                return View(vm);
            }

            MenuProductDTO original = await _menuProductManager.GetByIdAsync(vm.Id);
            if (original == null)
                return NotFound();

            MenuProductDTO updated = new MenuProductDTO
            {
                Id = vm.Id,
                MenuId = vm.MenuId,
                ProductId = vm.ProductId,
                UnitPrice = vm.UnitPrice,
                IsActive = vm.IsActive
            };

            Result result = await _menuProductManager.UpdateAsync(original, updated);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Menü ürünü güncellenemedi.");
                List<MenuDTO> menus = await _menuManager.GetAllAsync();
                List<ProductDTO> products = await _productManager.GetAllAsync();
                vm.MenuList = menus.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.MenuName }).ToList();
                vm.ProductList = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList();
                return View(vm);
            }

            TempData["Success"] = "Menü ürünü baþarýyla güncellendi.";
            return RedirectToAction("MenuProductManagement");
        }
        [HttpPost]
        public async Task<IActionResult> HardDeleteMenuProduct(int id)
        {
            Result result = await _menuProductManager.HardDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Menü ürünü silinemedi. Ýliþkili kayýtlar olabilir.";
            else
                TempData["Success"] = "Menü ürünü kalýcý olarak silindi.";
            return RedirectToAction("MenuProductManagement");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteMenuProduct(int id)
        {
            Result result = await _menuProductManager.SoftDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Menü ürünü pasife alýnamadý.";
            else
                TempData["Success"] = "Menü ürünü pasife alýndý.";
            return RedirectToAction("MenuProductManagement");
        }

        [HttpGet]
        public async Task<IActionResult> MenuProductDetail(int id)
        {
            MenuProductDTO menuProduct = await _menuProductManager.GetByIdAsync(id);
            if (menuProduct == null)
                return NotFound();

            MenuProductDetailVm vm = new MenuProductDetailVm
            {
                Id = menuProduct.Id,
                MenuName = menuProduct.MenuName,
                ProductName = menuProduct.ProductName,
             
                UnitPrice = menuProduct.UnitPrice,
                IsActive = menuProduct.IsActive
            };

            return View(vm);
        }
        [HttpGet]
        public IActionResult OrderManagement()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PurchaseInvoiceList(int? SelectedSupplierId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<OrderDTO> allInvoices = await _orderManager.WhereAsync(o =>
                o.Type == OrderType.Purchase &&
                (!SelectedSupplierId.HasValue || o.SupplierId == SelectedSupplierId) &&
                (!startDate.HasValue || o.OrderDate >= startDate) &&
                (!endDate.HasValue || o.OrderDate <= endDate)
            );

            List<SupplierDTO> suppliers = await _supplierManager.GetAllAsync();

            foreach (OrderDTO invoice in allInvoices)
            {
                invoice.SupplierName = suppliers.FirstOrDefault(s => s.Id == invoice.SupplierId)?.SupplierName;
            }

            PurchaseInvoiceListVm vm = new PurchaseInvoiceListVm
            {
                Invoices = allInvoices,
                SelectedSupplierId = SelectedSupplierId,
                StartDate = startDate,
                EndDate = endDate,
                SupplierList = suppliers.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SupplierName,
                    Selected = SelectedSupplierId.HasValue && s.Id == SelectedSupplierId.Value
                }).ToList()
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> PurchaseInvoiceDetail(int id)
        {
            OrderDTO invoice = await _orderManager.GetByIdAsync(id);
            if (invoice == null || invoice.Type != OrderType.Purchase)
                return NotFound();

            string supplierName = null;
            if (invoice.SupplierId.HasValue)
            {
                SupplierDTO supplier = await _supplierManager.GetByIdAsync(invoice.SupplierId.Value);
                supplierName = supplier?.SupplierName;
            }

            PurchaseInvoiceDetailVm vm = new PurchaseInvoiceDetailVm
            {
                Invoice = invoice,
                Details = invoice.OrderDetails.ToList(),
                SupplierName = supplierName
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddPurchaseInvoice()
        {
            List<SupplierDTO> suppliers = await _supplierManager.GetAllAsync();
            List<ProductDTO> products = await _productManager.GetAllAsync();

            PurchaseInvoiceEditVm vm = new PurchaseInvoiceEditVm
            {
                OrderDate = DateTime.Now,
                SupplierList = suppliers.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SupplierName
                }).ToList(),
                ProductList = products.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.ProductName
                }).ToList()
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> AddPurchaseInvoice(PurchaseInvoiceEditVm vm)
        {
            vm.TotalPrice = vm.Details.Sum(d => d.Quantity * d.UnitPrice);

            if (!ModelState.IsValid)
            {
                List<SupplierDTO> suppliers = await _supplierManager.GetAllAsync();
                List<ProductDTO> products = await _productManager.GetAllAsync();
                vm.SupplierList = suppliers.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.SupplierName }).ToList();
                vm.ProductList = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList();
                return View(vm);
            }

            OrderDTO dto = new OrderDTO
            {
                SupplierId = vm.SupplierId,
                OrderDate = vm.OrderDate,
                TotalPrice = vm.TotalPrice,
                Type = OrderType.Purchase,
                OrderState = OrderStatus.Completed,
                OrderDetails = vm.Details
            };

            Result result = await _orderManager.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Fatura eklenemedi.");
                List<SupplierDTO> suppliers = await _supplierManager.GetAllAsync();
                List<ProductDTO> products = await _productManager.GetAllAsync();
                vm.SupplierList = suppliers.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.SupplierName }).ToList();
                vm.ProductList = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList();
                return View(vm);
            }

            TempData["Success"] = "Alým faturasý baþarýyla eklendi.";
            return RedirectToAction("PurchaseInvoiceList");
        }
        [HttpGet]
        public async Task<IActionResult> EditPurchaseInvoice(int id)
        {
            OrderDTO invoice = await _orderManager.GetByIdAsync(id);
            if (invoice == null || invoice.Type != OrderType.Purchase)
                return NotFound();
            if (!invoice.SupplierId.HasValue)
            {
                TempData["Error"] = "Alým faturasý için tedarikçi zorunludur. Lütfen eksik veriyi tamamlayýn.";
                return RedirectToAction("PurchaseInvoiceList");
            }
            List<SupplierDTO> suppliers = await _supplierManager.GetAllAsync();
            List<ProductDTO> products = await _productManager.GetAllAsync();

            PurchaseInvoiceEditVm vm = new PurchaseInvoiceEditVm
            {
                Id = invoice.Id,
                SupplierId = invoice.SupplierId,
                OrderDate = invoice.OrderDate,
                TotalPrice = invoice.TotalPrice,
                Details = invoice.OrderDetails.ToList(),
                SupplierList = suppliers.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.SupplierName }).ToList(),
                ProductList = products.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.ProductName }).ToList()
            };

            return View(vm);
        }
  
        private async Task PopulatePurchaseLists(PurchaseInvoiceEditVm vm)
        {
            List<SupplierDTO>  suppliers = await _supplierManager.GetAllAsync();
            List<ProductDTO> products = await _productManager.GetAllAsync();

            vm.SupplierList = suppliers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SupplierName
            }).ToList();

            vm.ProductList = products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.ProductName
            }).ToList();
        }

       
        private async Task PopulateSaleLists(SaleInvoiceEditVm vm)
        {
            List<ProductDTO> products = await _productManager.GetAllAsync();

            vm.ProductList = products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.ProductName
            }).ToList();

            OrderDTO original = await _orderManager.GetByIdAsync(vm.Id ?? 0);
            if (original != null)
            {
                vm.TableName = original.TableName;
                vm.WaiterName = original.WaiterFullName;
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditPurchaseInvoice(PurchaseInvoiceEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePurchaseLists(vm);
                return View(vm);
            }

           
            OrderDTO updated = new OrderDTO
            {
                Id = vm.Id ?? 0,
                SupplierId = vm.SupplierId,
                OrderDate = vm.OrderDate,
                TotalPrice = vm.TotalPrice,
                Type = OrderType.Purchase,
                OrderDetails = vm.Details ?? new List<OrderDetailDTO>()
            };

          
            Result result = await _orderManager.UpdateAsync(new OrderDTO { Id = vm.Id ?? 0 }, updated);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Fatura güncellenemedi.");
                await PopulatePurchaseLists(vm);
                return View(vm);
            }

            TempData["Success"] = "Alým faturasý baþarýyla güncellendi.";
            return RedirectToAction("PurchaseInvoiceList");
        }
        [HttpPost]
        public async Task<IActionResult> DeletePurchaseInvoice(int id)
        {

            Result softResult = await _orderManager.SoftDeleteByIdAsync(id);
            if (!softResult.IsSuccess)
            {
                TempData["Error"] = "Fatura silinemedi (soft delete baþarýsýz).";
                return RedirectToAction("EditPurchaseInvoice", new { id });
            }


            Result hardResult = await _orderManager.HardDeleteByIdAsync(id);
            if (!hardResult.IsSuccess)
            {
                TempData["Error"] = "Fatura silinemedi (hard delete baþarýsýz).";
                return RedirectToAction("EditPurchaseInvoice", new { id });
            }

            TempData["Success"] = "Fatura baþarýyla silindi.";
            return RedirectToAction("PurchaseInvoiceList");
        }
        [HttpGet]
        public async Task<IActionResult> SaleInvoiceList(int? SelectedTableId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<OrderDTO> allInvoices = await _orderManager.WhereAsync(o =>
                o.Type == OrderType.Sale &&
                (!SelectedTableId.HasValue || o.TableId == SelectedTableId) &&
                (!startDate.HasValue || o.OrderDate >= startDate) &&
                (!endDate.HasValue || o.OrderDate <= endDate)
            );

            List<TableDTO> tables = await _tableManager.GetAllAsync();

            SaleInvoiceListVm vm = new SaleInvoiceListVm
            {
                Invoices = allInvoices,
                SelectedTableId = SelectedTableId,
                StartDate = startDate,
                EndDate = endDate,
                TableList = tables.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Selected = SelectedTableId.HasValue && t.Id == SelectedTableId.Value
                }).ToList()
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> SaleInvoiceDetail(int id)
        {
            OrderDTO invoice = await _orderManager.GetByIdAsync(id);
            if (invoice == null || invoice.Type != OrderType.Sale)
                return NotFound();

            string tableName = null;
            if (invoice.TableId.HasValue)
            {
                TableDTO table = await _tableManager.GetByIdAsync(invoice.TableId.Value);
                tableName = table?.TableNumber;
            }

            SaleInvoiceDetailVm vm = new SaleInvoiceDetailVm
            {
                Invoice = invoice,
                Details = invoice.OrderDetails.ToList(),
                TableName = tableName
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> EditSaleInvoice(int id)
        {
            OrderDTO invoice = await _orderManager.GetByIdAsync(id);
            if (invoice == null || invoice.Type != OrderType.Sale)
                return NotFound();


            if (!invoice.TableId.HasValue || !invoice.WaiterId.HasValue)
            {
                TempData["Error"] = "Satýþ faturasý için masa ve garson zorunludur. Lütfen eksik veriyi tamamlayýn.";
                return RedirectToAction("SaleInvoiceList");
            }

            List<ProductDTO> products = await _productManager.GetAllAsync();

            string tableName = "";
            if (invoice.TableId > 0)
            {
                TableDTO table = await _tableManager.GetByIdAsync(invoice.TableId.Value);
                tableName = table?.TableNumber ?? "";
            }

            string waiterName = "";
            if (invoice.WaiterId > 0)
            {
                AppUserDTO waiter = await _appUserManager.GetByIdAsync(invoice.WaiterId.Value);
                waiterName = waiter?.UserName ?? "";
            }

            SaleInvoiceEditVm vm = new SaleInvoiceEditVm
            {
                Id = invoice.Id,
                TableId = invoice.TableId.Value,
                WaiterId = invoice.WaiterId.Value,
                TableName = tableName,
                WaiterName = waiterName,
                OrderDate = invoice.OrderDate,
                TotalPrice = invoice.TotalPrice,
                Details = invoice.OrderDetails.ToList(),
                ProductList = products.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.ProductName
                }).ToList()
            };

            return View(vm);
        }

     
        [HttpPost]
        public async Task<IActionResult> EditSaleInvoice(SaleInvoiceEditVm vm)
        {
            if (!ModelState.IsValid)
            {
             
                await PopulateSaleLists(vm);
                return View(vm);
            }

         
            OrderDTO updated = new OrderDTO
            {
                Id = vm.Id ?? 0,
                TableId = vm.TableId,
                WaiterId = vm.WaiterId,
                OrderDate = vm.OrderDate,
                TotalPrice = vm.TotalPrice,
                Type = OrderType.Sale,
                OrderDetails = vm.Details ?? new List<OrderDetailDTO>()
            };

           
            Result result = await _orderManager.UpdateAsync(new OrderDTO { Id = vm.Id ?? 0 }, updated);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Satýþ faturasý güncellenirken bir hata oluþtu.");
                await PopulateSaleLists(vm);
                return View(vm);
            }

            TempData["Success"] = "Satýþ faturasý baþarýyla güncellendi.";
            return RedirectToAction("SaleInvoiceList");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSaleInvoice(int id)
        {
            Result softResult = await _orderManager.SoftDeleteByIdAsync(id);
            if (!softResult.IsSuccess)
            {
                TempData["Error"] = "Fatura silinemedi (soft delete baþarýsýz).";
                return RedirectToAction("EditSaleInvoice", new { id });
            }

            Result hardResult = await _orderManager.HardDeleteByIdAsync(id);
            if (!hardResult.IsSuccess)
            {
                TempData["Error"] = "Fatura silinemedi (hard delete baþarýsýz).";
                return RedirectToAction("EditSaleInvoice", new { id });
            }

            TempData["Success"] = "Fatura baþarýyla silindi.";
            return RedirectToAction("SaleInvoiceList");
        }
        [HttpGet]
        public async Task<IActionResult> SupplierManagement()
        {
            List<SupplierDTO> suppliers = await _supplierManager.GetAllAsync();
            SupplierListVm vm = new SupplierListVm
            {
                Suppliers = suppliers.Select(s => new SupplierVm
                {
                    Id = s.Id,
                    SupplierName = s.SupplierName,
                    ContactName = s.ContactName,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email
                }).ToList()
            };
            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> SupplierDetail(int id)
        {
            SupplierDTO supplier = await _supplierManager.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();

            SupplierDetailVm vm = new SupplierDetailVm
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                ContactName = supplier.ContactName,
                PhoneNumber = supplier.PhoneNumber,
                Email = supplier.Email,
                Address = supplier.Address
            };
            return View(vm);
        }


        [HttpGet]
        public IActionResult AddSupplier()
        {
            return View(new SupplierCreateVm());
        }


        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierCreateVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            SupplierDTO dto = new SupplierDTO
            {
                SupplierName = vm.SupplierName,
                ContactName = vm.ContactName,
                PhoneNumber = vm.PhoneNumber,
                Email = vm.Email,
                Address = vm.Address
            };

            Result result = await _supplierManager.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Tedarikçi eklenemedi. Lütfen bilgileri kontrol edin.");
                return View(vm);
            }

            TempData["Success"] = "Tedarikçi baþarýyla eklendi.";
            return RedirectToAction("SupplierManagement");
        }


        [HttpGet]
        public async Task<IActionResult> EditSupplier(int id)
        {
            SupplierDTO supplier = await _supplierManager.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();

            SupplierEditVm vm = new SupplierEditVm
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                ContactName = supplier.ContactName,
                PhoneNumber = supplier.PhoneNumber,
                Email = supplier.Email,
                Address = supplier.Address
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditSupplier(SupplierEditVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            SupplierDTO original = await _supplierManager.GetByIdAsync(vm.Id);
            if (original == null)
                return NotFound();

            SupplierDTO updated = new SupplierDTO
            {
                Id = vm.Id,
                SupplierName = vm.SupplierName,
                ContactName = vm.ContactName,
                PhoneNumber = vm.PhoneNumber,
                Email = vm.Email,
                Address = vm.Address
            };

            Result result = await _supplierManager.UpdateAsync(original, updated);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Tedarikçi güncellenemedi. Lütfen bilgileri kontrol edin.");
                return View(vm);
            }

            TempData["Success"] = "Tedarikçi baþarýyla güncellendi.";
            return RedirectToAction("SupplierManagement");
        }


        [HttpPost]
        public async Task<IActionResult> HardDeleteSupplier(int id)
        {
            Result result = await _supplierManager.HardDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Tedarikçi silinemedi. Ýliþkili kayýtlar olabilir.";
            else
                TempData["Success"] = "Tedarikçi baþarýyla silindi.";

            return RedirectToAction("SupplierManagement");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteSupplier(int id)
        {
            Result result = await _supplierManager.SoftDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Tedarikçi silinemedi. Ýliþkili kayýtlar olabilir.";
            else
                TempData["Success"] = "Tedarikçi baþarýyla silindi.";
            return RedirectToAction("SupplierManagement");
        }
        [HttpGet]
        public async Task<IActionResult> TableManagement()
        {
            List<TableDTO> tables = await _tableManager.GetAllAsync();
            List<AppUserDTO> waiters = await _appUserManager.GetAllAsync();

            TableListVm vm = new TableListVm
            {
                Tables = tables.Select(t => new TableVm
                {
                    Id = t.Id,
                    TableNumber = t.TableNumber,
                 
                    WaiterName = waiters.FirstOrDefault(w => w.Id == t.WaiterId)?.UserName,
                    TableStatus = t.Status.ToString()
                }).ToList()
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> TableDetail(int id)
        {
            TableDTO table = await _tableManager.GetByIdAsync(id);
            if (table == null)
                return NotFound();

            List<AppUserDTO> waiters = await _appUserManager.GetAllAsync();

            TableDetailVm vm = new TableDetailVm
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
              
                WaiterName = waiters.FirstOrDefault(w => w.Id == table.WaiterId)?.UserName,
                TableStatus = table.Status.ToString()
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddTable()
        {
            TableEditVm vm = new TableEditVm();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddTable(TableEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            TableDTO dto = new TableDTO
            {
                TableNumber = vm.TableNumber,
              
            };

            Result result = await _tableManager.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Masa eklenemedi.");
                return View(vm);
            }

            TempData["Success"] = "Masa baþarýyla eklendi.";
            return RedirectToAction("TableManagement");
        }

        [HttpGet]
        public async Task<IActionResult> EditTable(int id)
        {
            TableDTO table = await _tableManager.GetByIdAsync(id);
            if (table == null)
                return NotFound();

            TableEditVm vm = new TableEditVm
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
             
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditTable(TableEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            TableDTO original = await _tableManager.GetByIdAsync(vm.Id);
            if (original == null)
                return NotFound();

            TableDTO updated = new TableDTO
            {
                Id = vm.Id,
                TableNumber = vm.TableNumber,
       
            };

            Result result = await _tableManager.UpdateAsync(original, updated);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Masa güncellenemedi.");
                return View(vm);
            }

            TempData["Success"] = "Masa baþarýyla güncellendi.";
            return RedirectToAction("TableManagement");
        }

        [HttpPost]
        public async Task<IActionResult> HardDeleteTable(int id)
        {

            Result result = await _tableManager.HardDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Masa silinemedi. Ýliþkili kayýtlar olabilir.";
            else
                TempData["Success"] = "Masa baþarýyla silindi.";

            return RedirectToAction("TableManagement");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteTable(int id)
        {
            Result result = await _tableManager.SoftDeleteByIdAsync(id);
            if (!result.IsSuccess)
                TempData["Error"] = "Masa silinemedi. Ýliþkili kayýtlar olabilir.";
            else
                TempData["Success"] = "Masa baþarýyla silindi.";
            return RedirectToAction("TableManagement");
        }
    }
}

