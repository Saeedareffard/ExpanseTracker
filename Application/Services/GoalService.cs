using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GoalService
{
    private readonly IUnitOfWork _unitOfWork;

    public GoalService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<Goal> Get(int? pageNumber, int? size, string? orderBy, string? orderByDesc)
    {
        return _unitOfWork.Repository<Goal>()
            .Find(new GoalSpecification.PaginatedGoalsSpec(pageNumber, size, orderBy, orderByDesc));
    }

    public Goal? GetById(int id)
    {
        return _unitOfWork.Repository<Goal>().GetById(id);
    }

    public bool Update(Goal goal)
    {
        if (GetById(goal.Id) is null) return false;
        _unitOfWork.Repository<Goal>().Update(goal);

        _unitOfWork.Complete();
        return true;
    }

    public bool Delete(int id)
    {
        var result = _unitOfWork.Repository<Goal>().GetById(id);
        if (result is null) return false;
        _unitOfWork.Repository<Goal>().Remove(result);
        _unitOfWork.Complete();
        return true;
    }

    public Goal Add(Goal goal)
    {
        _unitOfWork.Repository<Goal>().Add(goal);
        _unitOfWork.Complete();
        return GetById(goal.Id)!;
    }
}