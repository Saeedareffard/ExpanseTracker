﻿using System.Collections;
using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Persistence.contexts;

namespace Infrastructure.Helpers;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repository<>);
            var repositoryInstance =Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)),_context);
            _repositories.Add(type,repositoryInstance);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

}