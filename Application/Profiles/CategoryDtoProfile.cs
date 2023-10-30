using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class CategoryDtoProfile : Profile
    {
        public CategoryDtoProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
