using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class OrderDetailManager(IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderDetailDTO> orderDetailValidator, ILogger<OrderDetailManager> logger)
        : BaseManager<OrderDetail, OrderDetailDTO>(orderDetailRepository, unitOfWork, mapper, orderDetailValidator, logger), IOrderDetailManager
    {
        private readonly IOrderDetailRepository _orderDetailRepository = orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task UpdateDetailStateAsync(int detailId, OrderDetailStatus newState)
        {
            await _orderDetailRepository.UpdateDetailStateAsync(detailId, newState);
            await _unitOfWork.CommitAsync();
        }
    }
}