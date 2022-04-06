using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CurrencyConvertor.V1.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Create(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entity);
        int Count(Expression<Func<TEntity, bool>> predicate);
        int Save();
    }
}
