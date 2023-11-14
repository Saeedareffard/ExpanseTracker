using Application.Common;
using Application.Dtos;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Goals;

public class CreateGoalRequest : IRequest<int>
{
    public GoalDto Dto { get; set; }
}

public class CreateGoalRequestValidator : CustomValidation<CreateGoalRequest>
{
    public CreateGoalRequestValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Dto.Name)
            .MustAsync(async (name, token) =>
                !await unitOfWork.Repository<Goal>().ContainsAsync(new GetGoalByNameSpec(name)))
            .WithMessage("A Goal with this name is already exists please provide another name");
    }
}

public class CreateGoalRequestHandler : IRequestHandler<CreateGoalRequest, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGoalRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<int> Handle(CreateGoalRequest request, CancellationToken cancellationToken)
    {
        var goal = _mapper.Map<Goal>(request.Dto);
        await _unitOfWork.Repository<Goal>().AddAsync(goal);
        await _unitOfWork.Complete();
        return goal.Id;
    }
}