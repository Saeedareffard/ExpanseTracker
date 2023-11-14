using Application.Dtos;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CategoryService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Category>> Get()
    {
        return await _uow.Repository<Category>().FindAsync(new CategorySpecification());
    }


    public async Task<Category?> GetById(int id)
    {
        return await _uow.Repository<Category>().GetByIdAsync(id);
    }


    public async Task<bool> Update(CategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        if (GetById(category.Id) is null) return false;


        _uow.Repository<Category>().Update(category);
        await _uow.Complete();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var result = await GetById(id);
        if (result is null) return false;


        await _uow.Repository<Category>().RemoveAsync(result);
        await _uow.Complete();
        return true;
    }

    public async Task Add(CategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _uow.Repository<Category>().AddAsync(category);
        await _uow.Complete();
    }
}