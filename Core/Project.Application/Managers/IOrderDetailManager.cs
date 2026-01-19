using Project.Application.DTOs;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IOrderDetailManager:IManager<OrderDetail,OrderDetailDTO>
    {
        Task UpdateDetailStateAsync(int detailId, OrderDetailStatus newState); //OrderDetail durumunu güncellemek için yazıldı

    }
}
