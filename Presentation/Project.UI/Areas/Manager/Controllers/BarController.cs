using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Domain.Enums;
using Project.UI.Areas.Manager.Models.BarAndKitchenVMs;


[Area("Manager")]
public class BarController : Controller
{
    private readonly IRecipeManager _recipeManager;
    private readonly ICategoryManager _categoryManager;
    private readonly IProductManager _productManager;
    private readonly IUnitManager _unitManager;
    private readonly IOrderManager _orderManager;
    private readonly IMenuManager _menuManager;
    private readonly IMenuProductManager _menuProductManager;

    private const int DrinkCategoryId = 2;

    public BarController(IRecipeManager recipeManager, ICategoryManager categoryManager, IProductManager productManager, IUnitManager unitManager, IOrderManager orderManager, IMenuManager menuManager, IMenuProductManager menuProductManager)
    {
        _recipeManager = recipeManager;
        _categoryManager = categoryManager;
        _productManager = productManager;
        _unitManager = unitManager;
        _orderManager = orderManager;
        _menuManager = menuManager;
        _menuProductManager = menuProductManager;
    }

    private async Task<bool> IsDrinkCategoryAsync(int categoryId)
    {
        CategoryDTO category = await _categoryManager.GetByIdAsync(categoryId);
        return category != null && (category.Id == DrinkCategoryId || category.ParentCategoryId == DrinkCategoryId);
    }

    private async Task<List<ProductDTO>> GetDrinkProductsAsync()
    {
        List<ProductDTO> products = await _productManager.GetAllAsync();
        List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
        HashSet<int> drinkCategoryIds = categories
            .Where(c => c.Id == DrinkCategoryId || c.ParentCategoryId == DrinkCategoryId)
            .Select(c => c.Id)
            .ToHashSet();
        return products.Where(p => drinkCategoryIds.Contains(p.CategoryId)).ToList();
    }

    private async Task<List<CategoryDTO>> GetDrinkCategoriesAsync()
    {
        List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
        return categories.Where(c => c.Id == DrinkCategoryId || c.ParentCategoryId == DrinkCategoryId).ToList();
    }

    public IActionResult DashBoard()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> RecipeList()
    {
        List<RecipeDTO> recipes = await _recipeManager.GetAllAsync();
        List<RecipeDTO> filteredRecipes = new List<RecipeDTO>();
        foreach (RecipeDTO r in recipes)
        {
            if (await IsDrinkCategoryAsync(r.CategoryId))
                filteredRecipes.Add(r);
        }

        List<RecipeListPageVm> vmList = new List<RecipeListPageVm>();
        foreach (RecipeDTO r in filteredRecipes)
        {
            ProductDTO product = await _productManager.GetByIdAsync(r.ProductId);
            CategoryDTO category = await _categoryManager.GetByIdAsync(r.CategoryId);
            vmList.Add(new RecipeListPageVm
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

    private async Task SetViewBagsAsync()
    {
        List<ProductDTO> products = await GetDrinkProductsAsync();
        List<CategoryDTO> categories = await GetDrinkCategoriesAsync();
        List<UnitDTO> units = await _unitManager.GetAllAsync();

        ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
        ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
        ViewBag.UnitList = new SelectList(units, "Id", "UnitName");
    }

    [HttpGet]
    public async Task<IActionResult> CreateRecipe()
    {
        await SetViewBagsAsync();

        RecipeEditPageVm vm = new RecipeEditPageVm
        {
            Items = new List<RecipeItemDTO> { new RecipeItemDTO() }
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe(RecipeEditPageVm vm)
    {
        if (!ModelState.IsValid)
        {
            await SetViewBagsAsync();
            return View(vm);
        }

        if (!await IsDrinkCategoryAsync(vm.CategoryId))
        {
            ModelState.AddModelError("", "Sadece içecek kategorisine ait ürünler seçilebilir.");
            await SetViewBagsAsync();
            return View(vm);
        }

        if (vm.Items.Any(x => x.ProductId == vm.ProductId))
        {
            ModelState.AddModelError("", "Reçetenin ana ürünü ile malzeme olarak eklenen ürünler aynı olamaz.");
            await SetViewBagsAsync();
            return View(vm);
        }

        RecipeDTO existingRecipe = await _recipeManager.GetByProductIdAsync(vm.ProductId);
        if (existingRecipe != null)
        {
            ModelState.AddModelError("", "Bu ürüne ait bir reçete zaten mevcut. Aynı ürüne ikinci bir reçete ekleyemezsiniz.");
            await SetViewBagsAsync();
            return View(vm);
        }

        RecipeDTO dto = new RecipeDTO
        {
            Name = vm.Name,
            Description = vm.Description,
            ProductId = vm.ProductId,
            CategoryId = vm.CategoryId,
            RecipeItems = vm.Items
        };

        OperationStatus result = await _recipeManager.CreateAsync(dto);

        if (result != OperationStatus.Success)
        {
            ModelState.AddModelError("", "Reçete eklenemedi.");
            await SetViewBagsAsync();
            return View(vm);
        }

        return RedirectToAction("RecipeList");
    }

    [HttpGet]
    public async Task<IActionResult> EditRecipe(int id)
    {
        RecipeDTO recipe = await _recipeManager.GetByIdWithItemsAsync(id);
        if (recipe == null || !await IsDrinkCategoryAsync(recipe.CategoryId)) return NotFound();

        await SetViewBagsAsync();

        RecipeEditPageVm vm = new RecipeEditPageVm
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            ProductId = recipe.ProductId,
            CategoryId = recipe.CategoryId,
            Items = recipe.RecipeItems.ToList()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> EditRecipe(RecipeEditPageVm vm)
    {
        if (!ModelState.IsValid)
        {
            await SetViewBagsAsync();
            return View(vm);
        }

        if (!await IsDrinkCategoryAsync(vm.CategoryId))
        {
            ModelState.AddModelError("", "Sadece içecek kategorisine ait ürünler seçilebilir.");
            await SetViewBagsAsync();
            return View(vm);
        }

        if (vm.Items.Any(x => x.ProductId == vm.ProductId))
        {
            ModelState.AddModelError("", "Reçetenin ana ürünü ile malzeme olarak eklenen ürünler aynı olamaz.");
            await SetViewBagsAsync();
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
            RecipeItems = vm.Items
        };

        OperationStatus result = await _recipeManager.UpdateAsync(existingDto, newDto);

        if (result != OperationStatus.Success)
        {
            ModelState.AddModelError("", "Reçete güncellenemedi.");
            await SetViewBagsAsync();
            return View(vm);
        }

        return RedirectToAction("RecipeList");
    }

    [HttpGet]
    public async Task<IActionResult> ActiveOrders()
    {
        List<OrderDTO> orders = await _orderManager.GetActiveOrdersAsync();

          List<OrderVm> vmList = orders.Select(o => new OrderVm
        {
            OrderId = o.Id,
            TableName = $"Masa {o.TableId}",
            TotalPrice = o.TotalPrice,
            OrderState = o.OrderState,
            OrderDetails = o.OrderDetails.Select(d => new OrderDetailPureVm
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
        List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
        HashSet<int> drinkCategoryIds = categories
            .Where(c => c.Id == DrinkCategoryId || c.ParentCategoryId == DrinkCategoryId)
            .Select(c => c.Id)
            .ToHashSet();

        List<MenuProductDTO> filteredMenuProducts = new List<MenuProductDTO>();
        foreach (MenuProductDTO mp in menuProducts)
        {
            ProductDTO product = await _productManager.GetByIdAsync(mp.ProductId);
            if (product != null && drinkCategoryIds.Contains(product.CategoryId))
                filteredMenuProducts.Add(mp);
        }

        List<MenuProductListPageVm> vmList = new List<MenuProductListPageVm>();
        foreach (MenuProductDTO mp in filteredMenuProducts)
        {
            ProductDTO product = await _productManager.GetByIdAsync(mp.ProductId);
            MenuDTO menu = await _menuManager.GetByIdAsync(mp.MenuId);
            CategoryDTO category = await _categoryManager.GetByIdAsync(product.CategoryId);

            vmList.Add(new MenuProductListPageVm
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
        List<ProductDTO> products = await GetDrinkProductsAsync();
        ViewBag.MenuList = new SelectList(menus, "Id", "MenuName");
        ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
        return View(new MenuProductCreatePageVm());
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuProduct(MenuProductCreatePageVm vm)
    {
        if (!ModelState.IsValid)
        {
            List<MenuDTO> menus = await _menuManager.GetAllAsync();
            List<ProductDTO> products = await GetDrinkProductsAsync();
            ViewBag.MenuList = new SelectList(menus, "Id", "MenuName");
            ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
            return View(vm);
        }

        MenuDTO menu = await _menuManager.GetByIdAsync(vm.MenuId);
        ProductDTO product = await _productManager.GetByIdAsync(vm.ProductId);

        if (product == null || !await IsDrinkCategoryAsync(product.CategoryId))
        {
            ModelState.AddModelError("", "Sadece içecek kategorisine ait ürünler seçilebilir.");
            List<MenuDTO> menus = await _menuManager.GetAllAsync();
            List<ProductDTO> products = await GetDrinkProductsAsync();
            ViewBag.MenuList = new SelectList(menus, "Id", "MenuName");
            ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
            return View(vm);
        }

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
            List<ProductDTO> products = await GetDrinkProductsAsync();
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

        ProductDTO product = await _productManager.GetByIdAsync(existingDto.ProductId);
        if (product == null || !await IsDrinkCategoryAsync(product.CategoryId))
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

        ProductDTO product = await _productManager.GetByIdAsync(existingDto.ProductId);
        if (product == null || !await IsDrinkCategoryAsync(product.CategoryId))
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

        ProductDTO product = await _productManager.GetByIdAsync(dto.ProductId);
        if (product == null || !await IsDrinkCategoryAsync(product.CategoryId))
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
        List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
        HashSet<int> drinkCategoryIds = categories
            .Where(c => c.Id == DrinkCategoryId || c.ParentCategoryId == DrinkCategoryId)
            .Select(c => c.Id)
            .ToHashSet();

        int menuProductCount = 0;
        foreach (MenuProductDTO mp in menuProducts)
        {
            ProductDTO product = await _productManager.GetByIdAsync(mp.ProductId);
            if (product != null && drinkCategoryIds.Contains(product.CategoryId) && mp.IsActive)
                menuProductCount++;
        }

        ViewBag.MenuProductCount = menuProductCount;
        return View();
    }
}