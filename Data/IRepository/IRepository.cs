using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(string? include = null, string[] includeProperties = null, Func<T, bool> filter = null);

        T? GetOne(Expression<Func<T, bool>> expression);

        void Create(T entity);

        void Edit(T entity);

        void Delete(T entity);

        void Commit();
    }
}
