using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task<bool> ContainsAsync(ISpecification<TEntity>? specification);
        Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(ISpecification<TEntity>? specification);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}