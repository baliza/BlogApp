using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using Core.Domain;


namespace Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(string id);
    } 
}
