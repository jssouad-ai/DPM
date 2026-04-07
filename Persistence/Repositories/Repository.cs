using Domain.Commun;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : DomainBase
    {
        protected readonly AppDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.Where(c => !c.IsDeleted).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Unable to create {entity} : " + msg);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
             entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByUrlAsync(string url)
        {
            return await _context.Images.AnyAsync(i => i.ImgURL == url);
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Projects.AnyAsync(i => i.ProjectName == name);
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
        }
    }
}
