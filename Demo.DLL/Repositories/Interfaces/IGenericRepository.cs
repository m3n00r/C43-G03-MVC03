using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DLL.Models.Shared;

namespace Demo.DLL.Repositories.Interfaces
{
    public  interface IGenericRepository<TEntity> where TEntity :BaseEntity
    {
        int Add(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> Predicate);
        TEntity? GetById(int id);
        int Remove(TEntity entity);
        int Update(TEntity entity);

    }
}
