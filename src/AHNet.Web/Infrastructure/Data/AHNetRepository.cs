using System.Collections.Generic;
using System.Linq;
using AHNet.Web.Core.Entities;
using AHNet.Web.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AHNet.Web.Infrastructure.Data
{
    public abstract class AHNetRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly AHNetDbContext _dbContext;

        protected AHNetRepository(AHNetDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(i => i.Id == id);
        }

        public List<T> List()
        {
            return _dbSet.ToList();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}