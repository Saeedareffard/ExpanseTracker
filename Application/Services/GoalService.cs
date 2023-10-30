using Application.Dtos;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GoalService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GoalService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

    public async Task<bool> Update(GoalDto dto)
    {
        var goal = _mapper.Map<Goal>(dto);
        if (GetById(goal.Id) is null) return false;
        _unitOfWork.Repository<Goal>().Update(goal);

        await _unitOfWork.Complete();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var result = _unitOfWork.Repository<Goal>().GetById(id);
        if (result is null) return false;
        _unitOfWork.Repository<Goal>().Remove(result);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<Goal> Add(GoalDto dto)
    {
        var goal = _mapper.Map<Goal>(dto);
        _unitOfWork.Repository<Goal>().Add(goal);
        await _unitOfWork.Complete();
        return GetById(goal.Id)!;
    }
}