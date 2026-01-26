using AutoMapper;
using FluentValidation;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class MenuProductManager(IMenuProductRepository menuProductRepository,IMapper mapper,IValidator<MenuProductDTO> validator):BaseManager<MenuProduct,MenuProductDTO>(menuProductRepository,mapper,validator), IMenuProductManager
    {
        private readonly IMenuProductRepository _menuProductRepository = menuProductRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<MenuProductDTO>> GetWithMenuAndProduct()
        {
            List<MenuProduct> menuProducts = await _menuProductRepository.GetWithMenuAndProduct();
            return _mapper.Map<List<MenuProductDTO>>(menuProducts);
        }

        public override async Task<OperationStatus> UpdateAsync(MenuProductDTO originalDto, MenuProductDTO newDto)
        {
            MenuProduct entity = await _menuProductRepository.GetByIdAsync(originalDto.Id);
            if (entity == null)
                return OperationStatus.NotFound;

            
            _mapper.Map(newDto, entity);

           
            entity.UnitPrice = newDto.UnitPrice;

            entity.Status = Domain.Enums.DataStatus.Updated;
            entity.UpdatedDate = DateTime.Now;

            await _menuProductRepository.UpdateAsync(entity);
            return OperationStatus.Success;

        }

       
    }
}
