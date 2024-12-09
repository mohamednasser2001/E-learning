using Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext dbContext;
        DbSet<T> dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public IQueryable<T> GetAll(Expression<Func<T, object>>[]? includeProp = null, Expression<Func<T, bool>>? expression = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (includeProp != null)
            {
                foreach (var item in includeProp)
                {
                    query = query.Include(item);
                }
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public T? GetOne(Expression<Func<T, object>>[]? includeProp = null, Expression<Func<T, bool>>? expression = null, bool tracked = true)
        {
            return GetAll(includeProp, expression, tracked).FirstOrDefault();
        }

        public IQueryable<T> GetAll(string? include = null)
        {
            throw new NotImplementedException();
        }

        public T? GetOne(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll(string? include = null, string[] includeProperties = null, Func<object, bool> filter = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll(string? include = null, string[] includeProperties = null, Func<T, bool> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
