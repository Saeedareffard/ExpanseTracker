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

    public async Task<IEnumerable<Goal>> Get(int? pageNumber, int? size, string? orderBy, string? orderByDesc)
    {
        return await _unitOfWork.Repository<Goal>()
            .FindAsync(new GoalSpecification.PaginatedGoalsSpec(pageNumber, size, orderBy, orderByDesc));
    }

    public async Task<Goal?> GetById(int id)
    {
        return await _unitOfWork.Repository<Goal>().GetByIdAsync(id);
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
        var result = await _unitOfWork.Repository<Goal>().GetByIdAsync(id);
        if (result is null) return false;
        await _unitOfWork.Repository<Goal>().RemoveAsync(result);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<Goal> Add(GoalDto dto)
    {
        var goal = _mapper.Map<Goal>(dto);
        await _unitOfWork.Repository<Goal>().AddAsync(goal);
        await _unitOfWork.Complete();
        return (await GetById(goal.Id))!;
    }
}