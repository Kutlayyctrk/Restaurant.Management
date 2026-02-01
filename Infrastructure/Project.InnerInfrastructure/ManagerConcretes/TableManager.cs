using AutoMapper;
using FluentValidation;
using Project.Application.DTOs;
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
    public class TableManager(ITableRepository tableRepository,IMapper mapper,IValidator<TableDTO> tableValidator):BaseManager<Table,TableDTO>(tableRepository,mapper,tableValidator),ITableManager
    {
        private readonly ITableRepository _tableRepository = tableRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<List<TableDTO>> GetTablesByUserIdAsync(string userId)
        {
            List<Table> tables = await _tableRepository.GetTablesByUserIdAsync(userId);
            return _mapper.Map<List<TableDTO>>(tables);
        }
    }
}
