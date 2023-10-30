using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class GoalDtoProfile  : Profile
    {
        public GoalDtoProfile()
        {
            CreateMap<Goal, GoalDto>();
            CreateMap<GoalDto, Goal>();
        }
    }
}
