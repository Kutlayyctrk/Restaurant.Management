using AspNetCoreGeneratedDocument;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Project.Application.DTOs;
using Project.Application.Managers;

using Project.UI.Models.SaleOrderVms;
using System.Security.Cryptography.Xml;

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


        public SalesController(ITableManager tableManager, IMenuManager menuManager, ICategoryManager categoryManager, IProductManager productManager, IOrderManager orderManager, IOrderDetailManager orderDetailManager, IMenuProductManager menuProductManager, IStockTransActionManager stockTransActionManager, IMapper mapper)
        {
            _tableManager = tableManager;
            _menuManager = menuManager;
            _categoryManager = categoryManager;
            _productManager = productManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
            _menuProductManager = menuProductManager;
            _stockTransActionManager = stockTransActionManager;
            _mapper= mapper;
        }

        public async Task<IActionResult> Index()
        {
            string userId= User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if(userId==null)
            {
                return RedirectToAction("Login", "LoginAndRegister");
            }

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
         
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return RedirectToAction("Login", "LoginAndRegister");

          
            TableDTO table = await _tableManager.GetByIdAsync(tableId);
            if (table == null) return NotFound();

         
            if (table.WaiterId.HasValue && table.WaiterId.Value.ToString() != userId)
            {
                TempData["Error"] = "Bu masa şu an başka bir garson tarafından yönetiliyor!";
                return RedirectToAction("Index");
            }

         
            List<CategoryDTO> rootCategories = await _categoryManager.GetRootCategoriesAsync();
            OrderDTO activeOrder = await _orderManager.GetActiveOrderForTableAsync(tableId);

            string[] forbiddenCategories = new[] { "Demirbaş", "Demirbaşlar", "Sarf Malzemeleri", "Giderler" };

            OrderVm vm = new OrderVm
            {
                TableId = tableId,
                TableNumber = table.TableNumber,
                ActiveOrderId = activeOrder?.Id,

             
                Categories = rootCategories
                    .Where(c => !forbiddenCategories.Contains(c.CategoryName))
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
                    Quantity = od.Quantity
                }).ToList() ?? new List<OrderDeatilVm>() 
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> GetMenuContent(int? categoryId)
        {
            string[] forbiddenCategories = new[] { "Demirbaş", "Demirbaşlar", "Sarf Malzemeleri", "Giderler" };

          
            List<CategoryDTO> subCategories = categoryId == null
                ? await _categoryManager.GetRootCategoriesAsync()
                : await _categoryManager.GetSubCategoriesByParentIdAsync(categoryId.Value);

          
            var filteredSubCategories = subCategories
                .Where(c => !forbiddenCategories.Contains(c.CategoryName))
                .Select(c => new { c.Id, c.CategoryName })
                .ToList();

           
            object products = new List<object>();
            if (categoryId != null && !filteredSubCategories.Any())
            {
                List<ProductDTO> productList = await _productManager.GetProductsByCategoryIdAsync(categoryId.Value);
                products = productList.Select(p => new { p.Id, p.ProductName, p.UnitPrice }).ToList();
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
                return Json(new { success = false, message = "Sepet boş!" });

            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Json(new { success = false, message = "Oturum hatası!" });

            try
            {
                
                OrderDTO activeOrderDto = await _orderManager.GetActiveOrderForTableAsync(vm.TableId);

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
                            UnitPrice = d.UnitPrice
                        }).ToList()
                    };

                    
                    await _orderManager.CreateAsync(newOrder);
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
                            UnitPrice = item.UnitPrice
                        });
                    }

             
                    await _orderManager.UpdateAsync(activeOrderDto, updatedOrder);
                }

             
                TableDTO tableDto = await _tableManager.GetByIdAsync(vm.TableId);
                if (tableDto != null)
                {
                    tableDto.TableStatus = Domain.Enums.TableStatus.Occupied;
                    tableDto.WaiterId = int.Parse(userId);
              
                    await _tableManager.UpdateAsync(tableDto, tableDto);
                }

                return Json(new { success = true, message = "Sipariş başarıyla işlendi ve stoktan düşüldü." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CloseOrder(int tableId)
        {
            try
            {
                
                OrderDTO orderDto = await _orderManager.GetActiveOrderForTableAsync(tableId);
                if (orderDto == null) return Json(new { success = false, message = "Kapatılacak aktif sipariş bulunamadı." });

                
                await _orderManager.ChangeOrderStateAsync(orderDto.Id, Domain.Enums.OrderStatus.Closed);

               
                TableDTO tableDto = await _tableManager.GetByIdAsync(tableId);
                if (tableDto != null)
                {
                    tableDto.TableStatus = Domain.Enums.TableStatus.Free; 
                    tableDto.WaiterId = null;
                    await _tableManager.UpdateAsync(tableDto, tableDto);
                }

                return Json(new { success = true, message = "Hesap kapatıldı, masa artık boş." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message });
            }
        }
    }
}

