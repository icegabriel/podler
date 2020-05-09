using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationContext _context;

        protected DbSet<T> DbSet { get => _context.Set<T>(); }

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await DbSet.Where(c => c.Id == id).SingleOrDefaultAsync();
        }

        public virtual async Task<List<T>> GetListAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task IncludeAsync(T obj)
        {
            await DbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(T obj)
        {
            DbSet.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T obj)
        {
            DbSet.Update(obj);
            await _context.SaveChangesAsync();
        }
    }

    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> GetAsync(int id);
        Task<List<T>> GetListAsync();
        Task IncludeAsync(T obj);
        Task RemoveAsync(T obj);
        Task UpdateAsync(T obj);
    }
}