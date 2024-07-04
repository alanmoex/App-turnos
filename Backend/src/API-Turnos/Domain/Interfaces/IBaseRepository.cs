using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T entity);
        List<T> GetAll();
        T? GetById<TId>(TId id);
        void Delete(T entity);
        T Update(T entity);
    }
}
