using Application.Specifications;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CategoryService
{
    private readonly IUnitOfWork _uow;

    public CategoryService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public IEnumerable<Category> Get()
    {
        return _uow.Repository<Category>().Find(new CategorySpecification());
    }


    public Category? GetById(int id)
    {
        return _uow.Repository<Category>().GetById(id);
    }


    public bool Update(Category category)
    {
        if (GetById(category.Id) is null) return false;


        _uow.Repository<Category>().Update(category);
        _uow.Complete();

        return true;
    }

    public bool Delete(int id)
    {
        var result = GetById(id);
        if (result is null) return false;


        _uow.Repository<Category>().Remove(result);
        _uow.Complete();
        return true;
    }

    public Category Add(Category category)
    {
        _uow.Repository<Category>().Add(category);
        _uow.Complete();
        return category;
    }
}