using Domain.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : DomainBase
    {

        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsByUrlAsync(string url);
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<string> ids);

    }
}
