using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        // Get All Category
        Task<List<Category>> GetAllAsync();

        // Get Category by Id 
        Task<Category?> GetByIdAsync(string id);

        // Add Category
        Task AddAsync(Category category);

        // Update Category
        Task UpdateAsync(Category category);

        //Delete Category
        Task DeleteAsync(Category category);
    }
}
