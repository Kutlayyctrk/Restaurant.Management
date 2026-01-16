using Project.Application.DTOs;
using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IManager<TEntity,TDto> where TEntity:class,IEntity where TDto: BaseDto
    {
        Task<List<TDto>> GetAllAsync();
        Task<List<TDto>> GetActives();
        Task<List<TDto>> GetPassives();
        Task<TDto> GetByIdAsync(int id);
        Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression); //Burayı IQueryable yapmadım çünkü dışarıya entity sızdırmak istemiyorum. Bunun yerine DTO döndürdüm. Bu ciddi bir güvenlik açığı ve onion mimarisine aykırıydı. Best Pratice ve onion architecture için araştırdıgımda karşıma çıktı.


        Task<string> CreateAsync(TDto dto);
        Task<string> UpdateAsync(int id, TDto dto);
        Task<string> SoftDeleteAsync(int id); //Soft delete bir manager'da tutup repository'e update'le ilettim çünkü bussines logic içerir. Dogurdan repository'de soft delete yapmak istemedim.
        Task<string> HardDeleteAsync(int id);
    }
}
