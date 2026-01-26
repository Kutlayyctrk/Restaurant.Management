using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract.Repositories
{
    public interface IMenuProductRepository:IRepository<MenuProduct>
    {
        Task<List<MenuProduct>> GetWithMenuAndProduct();
    }
}
