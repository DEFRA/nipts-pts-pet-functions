﻿using Defra.PTS.Pet.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defra.PTS.Pet.Repositories.Implementation
{
    [ExcludeFromCodeCoverage]
    public class Repository<TEntity>(DbContext dbContext) : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _dbContext = dbContext;

        public async Task Add(TEntity entity)
        {
           await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public DbContext Get_dbContext()
        {
            return _dbContext;
        }

        public void Delete(object id)
        {
            TEntity? entity = _dbContext.Set<TEntity>().Find(id);
            if (entity != null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
        }

        public TEntity Find(object id)
        {
            return _dbContext.Set<TEntity>().Find(id)!;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return [.. _dbContext.Set<TEntity>()];
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
