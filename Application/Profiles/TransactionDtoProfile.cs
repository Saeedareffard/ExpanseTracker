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
    public class TransactionDtoProfile : Profile
    {
        public TransactionDtoProfile()
        {
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
