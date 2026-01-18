using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
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

        public KitchenController(IRecipeManager recipeManager, ICategoryManager categoryManager,
                                 IProductManager productManager, IUnitManager unitManager,
                                 IOrderManager orderManager)
        {
            _recipeManager = recipeManager;
            _categoryManager = categoryManager;
            _productManager = productManager;
            _unitManager = unitManager;
            _orderManager = orderManager;
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

            foreach (var r in recipes)
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
                    InsertedDate = DateTime.Now,
                    Status = "Active" ,
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


            return View(new RecipeDTO
            {
                RecipeItem = new List<RecipeItemDTO>()
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(RecipeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ValidationErrors"] = string.Join(" | ", errors);
            }

            if (dto.RecipeItem == null || !dto.RecipeItem.Any())
            {
                ModelState.AddModelError("", "En az bir malzeme eklemelisiniz.");

                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();
                TempData["DebugInfo"] = $"Recipe Name: {dto.Name}, Item Count: {dto.RecipeItem?.Count}";

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName");
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(dto);
            }

            if (ModelState.IsValid)
            {
                await _recipeManager.CreateAsync(dto);
                return RedirectToAction("RecipeList");
            }

            List<ProductDTO> products2 = await _productManager.GetAllAsync();
            List<CategoryDTO> categories2 = await _categoryManager.GetAllAsync();
            List<UnitDTO> units2 = await _unitManager.GetAllAsync();

            ViewBag.ProductList = new SelectList(products2, "Id", "ProductName");
            ViewBag.CategoryList = new SelectList(categories2, "Id", "CategoryName");
            ViewBag.UnitList = new SelectList(units2, "Id", "UnitName");

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> EditRecipe(int id)
        {
            
            RecipeDTO recipe = await _recipeManager.GetByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

          
            List<ProductDTO> products = await _productManager.GetAllAsync();
            List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
            List<UnitDTO> units = await _unitManager.GetAllAsync();

            ViewBag.ProductList = new SelectList(products, "Id", "ProductName", recipe.ProductId);
            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName", recipe.CategoryId);
            ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

            return View(recipe);
        }
        [HttpPost]
        public async Task<IActionResult> EditRecipe(RecipeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

                ViewBag.ProductList = new SelectList(products, "Id", "ProductName", dto.ProductId);
                ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName", dto.CategoryId);
                ViewBag.UnitList = new SelectList(units, "Id", "UnitName");

                return View(dto);
            }

            await _recipeManager.UpdateAsync(dto);
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
    }
}