using System.Linq.Expressions;
using System.Reflection;
using Domain.Interfaces;

namespace Domain.Common
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification() { }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected BaseSpecification(string? orderBy, string? orderByDesc,
            int? pageNumber, int? take)
        {
            if (orderBy != null && orderByDesc != null)
            {
                throw new ArgumentException("orderBy and orderByDesc can't be used together");
            }

            if (orderBy is not null)
            {
                OrderBy = CreateOrderByExpression(orderBy);
            }
            else if (orderByDesc != null)
            {
                OrderByDescending = CreateOrderByExpression(orderByDesc);
            }

            if (pageNumber != null || take != null)
            {
                ApplyPaging(pageNumber ?? 1 , take ?? 10);
            }
           
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPageEnabled { get; private set; } = false;

        protected virtual void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }

        protected virtual void AddInclude(string include)
        {
            IncludeStrings.Add(include);
        }

        protected virtual void ApplyPaging(int pageNumber, int size)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page Number Should be more than one");
            }
            Skip = (pageNumber-1) * size;
            Take = size;
            IsPageEnabled = true;
        }

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        private Expression<Func<T, object>> CreateOrderByExpression(string orderBy)
        {
            // Get the PropertyInfo of the property to be ordered by
            PropertyInfo propertyInfo = typeof(T).GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Invalid property name: {orderBy}");
            }

            // Create the parameter expression for the entity type
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

            // Create the property access expression
            Expression propertyAccess = Expression.Property(parameter, propertyInfo);

            // Convert the property access expression to object type
            Expression convertedExpression = Expression.Convert(propertyAccess, typeof(object));

            // Create the final order by expression
            return Expression.Lambda<Func<T, object>>(convertedExpression, parameter);
        }
    }
}
