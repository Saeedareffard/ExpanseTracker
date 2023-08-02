using System.Linq.Expressions;
using Domain.Common;
using Domain.Entities;

namespace Application.Specifications
{
    public class TransactionSpecification
    {
        public class TransactionsByCategoryIdSpecification : BaseSpecification<Transaction>
        {
            public TransactionsByCategoryIdSpecification(int categoryId) : base(x=>x.CategoryId==categoryId)
            {
            }

            public TransactionsByCategoryIdSpecification(List<int> categoryIds) : base(x=>categoryIds.Contains(x.CategoryId))
            {
            }
        }

        public class TransactionsPagedAndOrdered : BaseSpecification<Transaction>
        {
            public TransactionsPagedAndOrdered(int? page, int? size, string? orderBy, string? orderByDes) : base(take: size , pageNumber:page , orderBy:orderBy,orderByDesc:orderByDes)
            {
                              
            }
        }

        public class TransactionSearch : BaseSpecification<Transaction>
        {
            public TransactionSearch(int id) : base(x => x.Id == id){}
        }
    }
}
