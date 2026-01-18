using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.InnerInfrastructure.ManagerConcretes;
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

        public KitchenController(IRecipeManager recipeManager, ICategoryManager categoryManager, IProductManager productManager, IUnitManager unitManager, IOrderManager orderManager)
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

            List<RecipeListVm> vmList = recipes.Select(r => new RecipeListVm
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                ProductName = r.ProductName,
                CategoryName = r.CategoryName,
                InsertedDate = r.InsertedDate,
                Status = r.Status.ToString()
            }).ToList();

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
            if (dto.RecipeItem == null || !dto.RecipeItem.Any())
            {
                ModelState.AddModelError("", "En az bir malzeme eklemelisiniz.");
              
                List<ProductDTO> products = await _productManager.GetAllAsync();
                List<CategoryDTO> categories = await _categoryManager.GetAllAsync();
                List<UnitDTO> units = await _unitManager.GetAllAsync();

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
        public async Task<IActionResult> SendToChef(int orderId)
        {
            try
            {
                await _orderManager.ChangeOrderStateAsync(orderId, OrderStatus.SentToKitchen);
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "Sipariş güncellenemedi.");
            }

            return RedirectToAction("ActiveOrders");

        }



    }
}
