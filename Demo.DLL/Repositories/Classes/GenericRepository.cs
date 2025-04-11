using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DLL.Data.Contexts;
using Demo.DLL.Models.DepartmentModel;
using Demo.DLL.Models.Shared;
using Demo.DLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DLL.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(keyValues: id);

        //GetAll
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Set<TEntity>().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();

        }
        //GetById
        //public Department? GetById(int id) => _dbContext.Departments.Find(id);

        //update
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            //return _dbContext.SaveChanges();
        }

        //Delete

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            //return _dbContext.SaveChanges();
        }

        //Insert
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            //return _dbContext.SaveChanges();
        }

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbContext.Set<TEntity>()
           .Where(e => e.IsDeleted != true)
          .Select(selector)
          .ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return _dbContext.Set<TEntity>()
                     .Where(Predicate)
                     .ToList();

        }

        int IGenericRepository<TEntity>.Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
