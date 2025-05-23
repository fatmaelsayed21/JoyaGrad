using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly JoyaDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(JoyaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }




        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T?> GetFirstOrDefaultAsync(
                     Expression<Func<T, bool>> filter,
                     Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(filter);
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

        }


        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }


      

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }


}

