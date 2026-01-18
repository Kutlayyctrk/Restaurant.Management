using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract.Repositories
{
    public interface IRecipeRepository:IRepository<Recipe>
    {
        Task UpdateAsync(Recipe entity); //Tek Entity ile güncelleme yapmam gerekiyordu o yüzden  Recipe için ayrı bir ekleme metodu yaptım.
    }
}
