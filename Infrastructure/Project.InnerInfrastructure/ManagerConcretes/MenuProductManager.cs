using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Application.Results;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class MenuProductManager(IMenuProductRepository menuProductRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<MenuProductDTO> validator, ILogger<MenuProductManager> logger) : BaseManager<MenuProduct, MenuProductDTO>(menuProductRepository, unitOfWork, mapper, validator, logger), IMenuProductManager
    {
        private readonly IMenuProductRepository _menuProductRepository = menuProductRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<List<MenuProductDTO>> GetWithMenuAndProduct()
        {
            List<MenuProduct> menuProducts = await _menuProductRepository.GetWithMenuAndProduct();
            return _mapper.Map<List<MenuProductDTO>>(menuProducts);
        }

        public override async Task<Result> UpdateAsync(MenuProductDTO originalDto, MenuProductDTO newDto)
        {
            MenuProduct entity = await _menuProductRepository.GetByIdAsync(originalDto.Id);
            if (entity == null)
                return Result.Failure(OperationStatus.NotFound, "Menü ürünü bulunamadı.");

            _mapper.Map(newDto, entity);

            entity.UnitPrice = newDto.UnitPrice;

            entity.Status = Domain.Enums.DataStatus.Updated;
            entity.UpdatedDate = DateTime.Now;

            await _menuProductRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Succeed("Menü ürünü güncellendi.");
        }
    }
}