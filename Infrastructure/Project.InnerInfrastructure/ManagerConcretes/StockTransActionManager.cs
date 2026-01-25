using AutoMapper;
using FluentValidation;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class StockTransActionManager(IStockTransActionRepository stockTransActionRepository,IMapper mapper,IValidator<StockTransActionDTO> stockTransActionValidator):BaseManager<StockTransAction,StockTransActionDTO>(stockTransActionRepository,mapper,stockTransActionValidator),IStockTransActionManager
    {
        private readonly IStockTransActionRepository _stockTransActionRepository = stockTransActionRepository;
        private readonly IMapper _mapper = mapper;

        public async Task CreateDeletionOrderActionAsync(OrderDetail detail, OrderType type)
        {
            StockTransAction action = new StockTransAction
            {
                ProductId = detail.ProductId,
                OrderDetailId = detail.Id,
                Quantity = detail.Quantity,
                UnitPrice = detail.UnitPrice,
                Description = $"Fatura satırı silindi - {(type == OrderType.Purchase ? "Alım" : "Satış")} iptali",
                InsertedDate = DateTime.Now,
                Status = DataStatus.Deleted,
         
                Type = TransActionType.Return
            };

            await _stockTransActionRepository.CreateAsync(action);
        }

        public async Task CreateInitialOrderActionAsync(OrderDetail detail, OrderType type)
        {
            StockTransAction action = new StockTransAction
            {
                ProductId = detail.ProductId,
                OrderDetailId = detail.Id,
                Quantity = detail.Quantity, 
                UnitPrice = detail.UnitPrice,
                Type = type == OrderType.Purchase ? TransActionType.Purchase : TransActionType.Sale,
                Description = "Fatura oluşturuldu - İlk kayıt",
                InsertedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _stockTransActionRepository.CreateAsync(action);
        }

        public async Task CreateManualActionAsync(StockTransActionDTO dto)
        {
            
            StockTransAction action = _mapper.Map<StockTransAction>(dto);

            action.InsertedDate = DateTime.Now;
            action.Status = DataStatus.Inserted;
           

            await _stockTransActionRepository.CreateAsync(action);
        }

        public async Task CreateUpdateOrderActionAsync(OrderDetail detail, decimal quantityDiff, OrderType type)
        {
            if (quantityDiff == 0) return;

            StockTransAction action = new StockTransAction
            {
                ProductId = detail.ProductId,
                OrderDetailId = detail.Id,
                Quantity = Math.Abs(quantityDiff), 
                UnitPrice = detail.UnitPrice,
                Description = $"Fatura güncellendi - Miktar farkı yansıtıldı ({quantityDiff})",
                InsertedDate = DateTime.Now,
                Status = DataStatus.Updated
            };

           
            if (type == OrderType.Purchase)
            {
                action.Type = quantityDiff > 0 ? TransActionType.Purchase : TransActionType.Return;
              
            }
            else
            {
                action.Type = quantityDiff > 0 ? TransActionType.Sale : TransActionType.Return;
               
            }

            await _stockTransActionRepository.CreateAsync(action);
        }
    }
}
