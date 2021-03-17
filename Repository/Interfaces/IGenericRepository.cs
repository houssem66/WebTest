using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
   public interface IGenericRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(string id);

        IEnumerable<TEntity> GetAll();

        Task InsertAsync(TEntity entity);

        Task DeleteAsync(int id);
        Task DeleteAsync(string id);
        Task PutAsync(int id, TEntity entity);
        Task PutAsync(string id, TEntity entity);
    }   
}
