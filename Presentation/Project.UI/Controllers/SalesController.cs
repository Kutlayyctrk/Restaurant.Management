using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Application.Managers;
using Project.UI.Models.SaleOrderVms;
using System.Security.Claims;

namespace Project.UI.Controllers
{
    public class SalesController : Controller
    {
        private readonly ITableManager _tableManager;
        private readonly IMenuManager _menuManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IProductManager _productManager;
        private readonly IOrderManager _orderManager;
        private readonly IOrderDetailManager _orderDetailManager;
        private readonly IMenuProductManager _menuProductManager;
        private readonly IStockTransActionManager _stockTransActionManager;
        private readonly IMapper _mapper;

        public SalesController(
            ITableManager tableManager,
            IMenuManager menuManager,
            ICategoryManager categoryManager,
            IProductManager productManager,
            IOrderManager orderManager,
            IOrderDetailManager orderDetailManager,
            IMenuProductManager menuProductManager,
            IStockTransActionManager stockTransActionManager,
            IMapper mapper)
        {
            _tableManager = tableManager;
            _menuManager = menuManager;
            _categoryManager = categoryManager;
            _productManager = productManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
            _menuProductManager = menuProductManager;
            _stockTransActionManager = stockTransActionManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return RedirectToAction("Login", "LoginAndRegister");

            List<TableDTO> tableDtos = await _tableManager.GetTablesByUserIdAsync(userId);
            List<TableVm> tableVms = tableDtos.Select(dto => new TableVm
            {
                Id = dto.Id,
                TableNumber = dto.TableNumber,
                Status = dto.TableStatus.ToString(),
                WaiterId = dto.WaiterId
            }).ToList();

            return View(tableVms);
        }

        public async Task<IActionResult> SaleOrder(int tableId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return RedirectToAction("Login", "LoginAndRegister");

            TableDTO table = await _tableManager.GetByIdAsync(tableId);
            if (table == null) return NotFound();

            if (table.WaiterId.HasValue && table.WaiterId.Value.ToString() != userId)
            {
                TempData["Error"] = "Bu masa þu an baþka bir garson tarafýndan yönetiliyor!";
                return RedirectToAction("Index");
            }

           
            List<CategoryDTO> allRootCategories = await _categoryManager.GetRootCategoriesAsync();
            List<int> allowedRootIds = new List<int> { 1, 2 };

            OrderDTO activeOrder = await _orderManager.GetActiveOrderForTableAsync(tableId);

            OrderVm vm = new OrderVm
            {
                TableId = tableId,
                TableNumber = table.TableNumber,
                ActiveOrderId = activeOrder?.Id,
                Categories = allRootCategories
                    .Where(c => allowedRootIds.Contains(c.Id))
                    .Select(c => new CategoryVm
                    {
                        Id = c.Id,
                        Name = c.CategoryName
                    }).ToList(),

                ExistingDetails = activeOrder?.OrderDetails?.Select(od => new OrderDeatilVm
                { 
                    ProductId = od.ProductId,
                    ProductName = od.ProductName,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    DetailState = od.DetailState
                }).ToList() ?? new List<OrderDeatilVm>()
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuContent(int? categoryId)
        {
            List<CategoryVm> filteredSubCategories = new List<CategoryVm>();

            if (categoryId == null)
            {
                List<CategoryDTO> roots = await _categoryManager.GetRootCategoriesAsync();
                filteredSubCategories = roots
                    .Where(c => c.Id == 1 || c.Id == 2)
                    .Select(c => new CategoryVm { Id = c.Id, Name = c.CategoryName })
                    .ToList();
            }
            else
            {
                List<CategoryDTO> subCategories = await _categoryManager.GetSubCategoriesByParentIdAsync(categoryId.Value);
                filteredSubCategories = subCategories
                    .Select(c => new CategoryVm { Id = c.Id, Name = c.CategoryName })
                    .ToList();
            }

         
            List<ProductVm> products = new List<ProductVm>();

          
            if (categoryId != null && !filteredSubCategories.Any())
            {
                List<ProductDTO> productList = await _productManager.GetProductsByCategoryIdAsync(categoryId.Value);

                products = productList.Select(p => new ProductVm
                {
                    Id = p.Id,
                    Name = p.ProductName,
                    Price = p.UnitPrice
                }).ToList();
            }

            return Json(new
            {
                subCategories = filteredSubCategories,
                products = products
            });
        }

        [HttpPost]
        public async Task<IActionResult> SubmitOrder([FromBody] OrderSubmitVm vm)
        {
            if (vm == null || !vm.Details.Any())
                return Json(new { success = false, message = "Sepet boþ!" });

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Json(new { success = false, message = "Oturum hatasý!" });

            try
            {
                OrderDTO activeOrderDto = await _orderManager.GetActiveOrderForTableAsync(vm.TableId);
                Result status;

                if (activeOrderDto == null)
                {
                   
                    OrderDTO newOrder = new OrderDTO
                    {
                        TableId = vm.TableId,
                        WaiterId = int.Parse(userId),
                        OrderDate = DateTime.Now,
                        OrderState = Domain.Enums.OrderStatus.SentToKitchen,
                        Type = Domain.Enums.OrderType.Sale,
                        OrderDetails = vm.Details.Select(d => new OrderDetailDTO
                        {
                            ProductId = d.ProductId,
                            Quantity = d.Quantity,
                            UnitPrice = d.UnitPrice,
                            DetailState = Domain.Enums.OrderDetailStatus.SendToKitchen
                        }).ToList()
                    };
                    status = await _orderManager.CreateAsync(newOrder);
                }
                else
                {
                 
                    OrderDTO updatedOrder = _mapper.Map<OrderDTO>(activeOrderDto);
                    foreach (OrderDeatilVm item in vm.Details)
                    {
                        updatedOrder.OrderDetails.Add(new OrderDetailDTO
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            DetailState = Domain.Enums.OrderDetailStatus.SendToKitchen
                        });
                    }
                    status = await _orderManager.UpdateAsync(activeOrderDto, updatedOrder);

                  
                }

                if (status.IsSuccess)
                {
                  
                    TableDTO tableDto = await _tableManager.GetByIdAsync(vm.TableId);
                    tableDto.TableStatus = Domain.Enums.TableStatus.Occupied;
                    tableDto.WaiterId = int.Parse(userId);
                    await _tableManager.UpdateAsync(tableDto, tableDto);

                    return Json(new { success = true, message = "Ýþlem baþarýlý." });
                }

                return Json(new { success = false, message = "Ýþlem baþarýsýz oldu." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CloseOrder(int tableId)
        {
            try
            {
                OrderDTO orderDto = await _orderManager.GetActiveOrderForTableAsync(tableId);
                if (orderDto == null)
                    return Json(new { success = false, message = "Kapatýlacak aktif sipariþ bulunamadý." });

                await _orderManager.CloseOrderState(orderDto.Id);

                TableDTO tableDto = await _tableManager.GetByIdAsync(tableId);
                if (tableDto != null)
                {
                    tableDto.TableStatus = Domain.Enums.TableStatus.Free;
                    tableDto.WaiterId = null;
                    await _tableManager.UpdateAsync(tableDto, tableDto);
                }

                return Json(new { success = true, message = "Hesap kapatýldý." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message });
            }
        }
    }
}