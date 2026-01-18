using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract.Repositories
{
    public interface IOrderDetailRepository:IRepository<OrderDetail>
    {
        Task UpdateDetailStateAsync(int detailId, OrderDetailStatus newState);

    }
}
