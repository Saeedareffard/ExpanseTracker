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

    public IEnumerable<Transaction> Get(int? pageNumber, int? size, string? orderBy, string? orderByDesc)
    {
        var specification =
            new TransactionSpecification.TransactionsPagedAndOrdered(pageNumber, size, orderBy, orderByDesc);

        return _unitOfWork.Repository<Transaction>()
            .Find(specification);
    }

    public Transaction? GetById(int id)
    {
        return _unitOfWork.Repository<Transaction>().GetById(id);
    }

    public IEnumerable<Transaction> GetByCategoryIds(List<int> categoryIds)
    {
        return _unitOfWork.Repository<Transaction>()
            .Find(new TransactionSpecification.TransactionsByCategoryIdSpecification(categoryIds));
    }

    public IEnumerable<Transaction> GetByCategory(int categoryId)
    {
        return _unitOfWork.Repository<Transaction>()
            .Find(new TransactionSpecification.TransactionsByCategoryIdSpecification(categoryId));
    }

    public async Task Add(TransactionDto dto)
    {
        var transaction = _mapper.Map<Transaction>(dto);
        _unitOfWork.Repository<Transaction>().Add(transaction);
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
        var result = GetById(id);
        if (result is null) return false;
        _unitOfWork.Repository<Transaction>().Remove(result);
        await _unitOfWork.Complete();
        return true;
    }
}