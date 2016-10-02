using System.Collections.Generic;
using AHNet.Web.Core.Entities;

namespace AHNet.Web.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        List<T> List();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}