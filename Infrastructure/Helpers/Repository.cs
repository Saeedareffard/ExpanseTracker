using System.Linq.Expressions;
using AutoMapper;
using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Persistence.contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private  readonly  ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        }
        public TEntity? GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Find(ISpecification<TEntity> specification)
        {
           return ApplySpecification(specification);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.Entry(entity).State=EntityState.Modified;
        }

        public bool Contains(ISpecification<TEntity>? specification)
        {
            return Count(specification) > 0;
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        public int Count(ISpecification<TEntity>? specification)
        {
            return ApplySpecification(specification!).Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Count(predicate);
        }
    }
}