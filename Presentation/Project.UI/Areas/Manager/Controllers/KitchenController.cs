using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.UI.Areas.Manager.Models.KitchenVMs;

namespace Project.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class KitchenController : Controller
    {
        private readonly IRecipeManager _recipeManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IProductManager _productManager;
        private readonly IUnitManager _unitManager;
        private readonly IOrderManager _orderManager;
        private readonly IMenuManager _menuManager;
        private readonly IMenuProductManager _menuProductManager;

        public KitchenController(IRecipeManager recipeManager, ICategoryManager categoryManager,
                                 IProductManager productManager, IUnitManager unitManager,
                                 IOrderManager orderManager, IMenuManager menuManager,
                                 IMenuProductManager menuProductManager)
        {
            _recipeManager = recipeManager;
            _categoryManager = categoryManager;
            _productManager = productManager;
            _unitManager = unitManager;
            _orderManager = orderManager;
            _menuManager = menuManager;
            _menuProductManager = menuProductManager;
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RecipeList()
        {
            List<RecipeDTO> recipes = await _recipeManager.GetAllAsync();
            List<RecipeListVm> vmList = new List<RecipeListVm>();

            foreach (RecipeDTO r in recipes)
            {
                ProductDTO product = await _productManager.GetByIdAsync(r.ProductId);
                CategoryDTO category = await _categoryManager.GetByIdAsync(r.CategoryId);

                vmList.Add(new RecipeListVm
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    ProductName = product?.ProductName,
                    CategoryName = category?.CategoryName,
                    InsertedDate = r.InsertedDate,
                    Status = r.Status.ToString()
                });
            }

            return View(vmList);
        }


        [HttpGet]
        public async Task<IActionResult> CreateRecipe()
        {
            List<ProductDTO> products = await _productManager.GetAllAsync();
            List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
            List<UnitDTO> units = await _unitManager.GetAllAsync();

            ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
            ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

            EditRecipeVm vm = new EditRecipeVm
            {
                RecipeItems = new List<RecipeItemDTO> { new RecipeItemDTO() }
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(EditRecipeVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(vm);
            }

            // Ana ürün ile malzeme olarak eklenen ürünlerin aynı olmasını engelle
            if (vm.RecipeItems.Any(x => x.ProductId == vm.ProductId))
            {
                ModelState.AddModelError("", "Reçetenin ana ürünü ile malzeme olarak eklenen ürünler aynı olamaz.");

                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(vm);
            }

           
            RecipeDTO existingRecipe = await _recipeManager.GetByProductIdAsync(vm.ProductId);
            if (existingRecipe != null)
            {
                ModelState.AddModelError("", "Bu ürüne ait bir reçete zaten mevcut. Aynı ürüne ikinci bir reçete ekleyemezsiniz.");

                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(vm);
            }

            RecipeDTO dto = new RecipeDTO
            {
                Name = vm.Name,
                Description = vm.Description,
                ProductId = vm.ProductId,
                CategoryId = vm.CategoryId,
                RecipeItems = vm.RecipeItems
            };

            OperationStatus result = await _recipeManager.CreateAsync(dto);

            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Reçete eklenemedi.");

                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(vm);
            }

            return RedirectToAction("RecipeList");
        }

        [HttpGet]
        public async Task<IActionResult> EditRecipe(int id)
        {
            RecipeDTO recipe = await _recipeManager.GetByIdWithItemsAsync(id);
            if (recipe == null) return NotFound();

            List<ProductDTO> products = await _productManager.GetAllAsync();
            List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
            List<UnitDTO> units = await _unitManager.GetAllAsync();

            ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
            ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

            EditRecipeVm vm = new EditRecipeVm
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                ProductId = recipe.ProductId,
                CategoryId = recipe.CategoryId,
                RecipeItems = recipe.RecipeItems.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditRecipe(EditRecipeVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(vm);
            }

           
            if (vm.RecipeItems.Any(x => x.ProductId == vm.ProductId))
            {
                ModelState.AddModelError("", "Reçetenin ana ürünü ile malzeme olarak eklenen ürünler aynı olamaz.");

                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(vm);
            }

            RecipeDTO existingDto = await _recipeManager.GetByIdAsync(vm.Id);
            if (existingDto == null) return NotFound();

            RecipeDTO newDto = new RecipeDTO
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                ProductId = vm.ProductId,
                CategoryId = vm.CategoryId,
                RecipeItems = vm.RecipeItems
            };

            OperationStatus result = await _recipeManager.UpdateAsync(existingDto, newDto);

            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Reçete güncellenemedi.");

                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(vm);
            }

            return RedirectToAction("RecipeList");
        }





        [HttpGet]
        public async Task<IActionResult> ActiveOrders()
        {
            List<OrderDTO> orders = await _orderManager.GetActiveOrdersAsync();

            List<KitchenOrderVm> vmList = orders.Select(o => new KitchenOrderVm
            {
                OrderId = o.Id,
                TableName = $"Masa {o.TableId}",
                TotalPrice = o.TotalPrice,
                OrderState = o.OrderState,
                OrderDetails = o.OrderDetails.Select(d => new KitchenOrderDetailVm
                {
                    ProductName = d.ProductName,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    DetailState = d.DetailState
                }).ToList()
            }).ToList();

            return View(vmList);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            try
            {
                await _orderManager.ChangeOrderStateAsync(orderId, OrderStatus.Completed);
            }
            catch
            {
                ModelState.AddModelError("", "Sipariş güncellenemedi.");
            }

            return RedirectToAction("ActiveOrders");
        }

        [HttpGet]
        public async Task<IActionResult> MenuProducts()
        {
            List<MenuProductDTO> menuProducts = await _menuProductManager.GetAllAsync();
            List<MenuProductListVm> vmList = new List<MenuProductListVm>();

            foreach (MenuProductDTO mp in menuProducts)
            {
                ProductDTO product = await _productManager.GetByIdAsync(mp.ProductId);
                MenuDTO menu = await _menuManager.GetByIdAsync(mp.MenuId);
                CategoryDTO category = await _categoryManager.GetByIdAsync(product.CategoryId);

                vmList.Add(new MenuProductListVm
                {
                    Id = mp.Id,
                    MenuName = menu?.MenuName,
                    ProductName = product?.ProductName,
                    CategoryName = category?.CategoryName,
                    UnitPrice = mp.UnitPrice,
                    IsActive = mp.IsActive
                });
            }

            return View(vmList);
        }

        [HttpGet]
        public async Task<IActionResult> CreateMenuProduct()
        {
            List<MenuDTO> menus = await _menuManager.GetAllAsync();
            List<ProductDTO> products = await _productManager.GetAllAsync();
            ViewBag.MenuList = new SelectList(menus, "Id", "MenuName");
            ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
            return View(new CreateMenuProductVm());
        }

        [HttpPost]
        
        public async Task<IActionResult> CreateMenuProduct(CreateMenuProductVm vm)
        {
            if (!ModelState.IsValid)
            {
                List<MenuDTO> menus = await _menuManager.GetAllAsync();
                List<ProductDTO> products = await _productManager.GetAllAsync();
                ViewBag.MenuList = new SelectList(menus, "Id", "MenuName");
                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                return View(vm);
            }

            MenuDTO menu = await _menuManager.GetByIdAsync(vm.MenuId);
            ProductDTO product = await _productManager.GetByIdAsync(vm.ProductId);

            MenuProductDTO dto = new MenuProductDTO
            {
                MenuId = vm.MenuId,
                ProductId = vm.ProductId,
                UnitPrice = vm.UnitPrice,
                IsActive = vm.IsActive,
                MenuName = menu?.MenuName,
                ProductName = product?.ProductName,
                CategoryName = product?.CategoryName   
            };

            OperationStatus result = await _menuProductManager.CreateAsync(dto);
            if (result != OperationStatus.Success)
            {
                ModelState.AddModelError("", "Ürün eklenemedi.");
                List<MenuDTO> menus = await _menuManager.GetAllAsync();
                List<ProductDTO> products = await _productManager.GetAllAsync();
                ViewBag.MenuList = new SelectList(menus, "Id", "MenuName");
                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                return View(vm);
            }

            return RedirectToAction("MenuProducts");
        }


        [HttpPost]
    
        public async Task<IActionResult> DeactivateMenuProduct(int id)
        {
            MenuProductDTO existingDto = await _menuProductManager.GetByIdAsync(id);
            if (existingDto == null)
                return NotFound();

            if (!existingDto.IsActive)
                return RedirectToAction("MenuProducts");

            existingDto.IsActive = false;
            OperationStatus result = await _menuProductManager.UpdateAsync(existingDto, existingDto);

            if (result == OperationStatus.NotFound)
                return NotFound();

            if (result == OperationStatus.ValidationError)
            {
                TempData["Error"] = "Güncelleme sırasında doğrulama hataları oluştu.";
            }

            return RedirectToAction("MenuProducts");
        }

        [HttpPost]
      
        public async Task<IActionResult> ActivateMenuProduct(int id)
        {
            MenuProductDTO existingDto = await _menuProductManager.GetByIdAsync(id);
            if (existingDto == null)
                return NotFound();

            if (existingDto.IsActive)
                return RedirectToAction("MenuProducts");

            existingDto.IsActive = true;
            OperationStatus result = await _menuProductManager.UpdateAsync(existingDto, existingDto);

            if (result == OperationStatus.NotFound)
                return NotFound();

            if (result == OperationStatus.ValidationError)
            {
                TempData["Error"] = "Güncelleme sırasında doğrulama hataları oluştu.";
            }

            return RedirectToAction("MenuProducts");
        }





        [HttpPost]
        public async Task<IActionResult> RemoveMenuProduct(int id)
        {
            MenuProductDTO dto = await _menuProductManager.GetByIdAsync(id);
            if (dto == null)
                return NotFound();

            dto.IsActive = false;

            OperationStatus result = await _menuProductManager.UpdateAsync(dto, dto);

            if (result != OperationStatus.Success)
                ModelState.AddModelError("", "Menüden çıkarma işlemi başarısız.");

            return RedirectToAction("MenuProducts");
        }
        [HttpGet]
        public async Task<IActionResult> Reports()
        {
           
            List<MenuProductDTO> menuProducts = await _menuProductManager.GetAllAsync();
            int menuProductCount = menuProducts.Count(mp => mp.IsActive);

            ViewBag.MenuProductCount = menuProductCount;
            return View();
        }
    }
}
