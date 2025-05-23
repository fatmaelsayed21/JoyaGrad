using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Contracts
{
    public interface IGenericRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetFirstOrDefaultAsync(
                Expression<Func<T, bool>> filter,
                Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null  );

        IQueryable<T> Query();

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
