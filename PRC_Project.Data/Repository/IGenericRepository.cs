
using PRC_Project.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PRC_Project.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<PaginatedList<TEntity>> Get(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
                                         string includeProperties = "");
        Task<TEntity> GetLast(Expression<Func<TEntity, bool>> filter = null,string includeProperties = "");
        Task<TEntity> GetById(object id);
        IQueryable<TEntity> GetByObject(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void Delete(object id);
        void Update(TEntity entityToUpdate);
    }
}
