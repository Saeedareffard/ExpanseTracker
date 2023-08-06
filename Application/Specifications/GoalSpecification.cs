using Domain.Common;
using Domain.Entities;

namespace Application.Specifications;

public class GoalSpecification
{
    public class PaginatedGoalsSpec : BaseSpecification<Goal>
    {
        public PaginatedGoalsSpec(int? pageNumber, int? size, string? orderBy, string? orderByDesc) : base(
            pageNumber: pageNumber, take: size, orderBy: orderBy, orderByDesc: orderByDesc)
        {
        }
    }
}