using System;
using System.Collections.Generic;
using System.Linq;
using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AHNet.Web.Infrastructure.Data
{
    public class AHNetRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AHNetDbContext _dbContext;

        public AHNetRepository(AHNetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().FirstOrDefault(i => i.Id == id);
        }

        public List<T> List()
        {
            return _dbContext.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}