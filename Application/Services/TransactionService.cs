using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class TransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<Transaction> Get(int? pageNumber, int? size, string? orderBy, string? orderByDesc)
    {
        return _unitOfWork.Repository<Transaction>()
            .Find(new TransactionSpecification.TransactionsPagedAndOrdered(pageNumber, size, orderBy, orderByDesc));
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

    public Transaction Add(Transaction transaction)
    {
        _unitOfWork.Repository<Transaction>().Add(transaction);
        _unitOfWork.Complete();
        return transaction;
    }

    public bool Update(Transaction transaction)
    {
        if (GetById(transaction.Id) is null) return false;
        _unitOfWork.Repository<Transaction>().Update(transaction);
        _unitOfWork.Complete();
        return true;
    }


    public bool Delete(int id)
    {
        var result = GetById(id);
        if (result is null)
        {
            return false;
        }
        _unitOfWork.Repository<Transaction>().Remove(result);
        _unitOfWork.Complete();
        return true;
    }
}