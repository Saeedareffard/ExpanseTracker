using Domain.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            // we modify the IQueryable using the specification's Criteria
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            // includes all expression-based includes
            query = specification.Includes.Aggregate(query, (current,include) => current.Include(include));
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));


            if (specification.OrderBy != null)
            {
                query= query.OrderBy(specification.OrderBy);
            }else if (specification.OrderByDescending != null)
            {
                query=query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            if (specification.IsPageEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }
            return query;
        }
    }
}