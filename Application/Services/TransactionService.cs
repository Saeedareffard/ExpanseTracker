using Application.Dtos;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class TransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Transaction>> Get(int? pageNumber, int? size, string? orderBy, string? orderByDesc)
    {
        var specification =
            new TransactionSpecification.TransactionsPagedAndOrdered(pageNumber, size, orderBy, orderByDesc);
        specification.Includes.Add(x=> x.Category!);

        return await _unitOfWork.Repository<Transaction>()
            .FindAsync(specification);
    }

    public async Task<Transaction?> GetById(int id)
    {
        return await _unitOfWork.Repository<Transaction>().GetByIdAsync(id);
    }

    public async Task<IEnumerable<Transaction>> GetByCategoryIds(List<int> categoryIds)
    {
        return await _unitOfWork.Repository<Transaction>()
            .FindAsync(new TransactionSpecification.TransactionsByCategoryIdSpecification(categoryIds));
    }

    public async Task<IEnumerable<Transaction>> GetByCategory(int categoryId)
    {
        return await _unitOfWork.Repository<Transaction>()
            .FindAsync(new TransactionSpecification.TransactionsByCategoryIdSpecification(categoryId));
    }

    public async Task Add(TransactionDto dto)
    {
        var transaction = _mapper.Map<Transaction>(dto);
        await _unitOfWork.Repository<Transaction>().AddAsync(transaction);
        await _unitOfWork.Complete();
    }

    public async Task<bool> Update(TransactionDto dto)
    {
        var transaction = _mapper.Map<Transaction>(dto);
        if (GetById(transaction.Id) is null) return false;
        _unitOfWork.Repository<Transaction>().Update(transaction);
        await _unitOfWork.Complete();
        return true;
    }


    public async Task<bool> Delete(int id)
    {
        var result = await GetById(id);
        if (result is null) return false;
        await _unitOfWork.Repository<Transaction>().RemoveAsync(result);
        await _unitOfWork.Complete();
        return true;
    }
}