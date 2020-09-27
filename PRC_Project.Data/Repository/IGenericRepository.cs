
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
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetLast(Expression<Func<TEntity, bool>> filter = null,string includeProperties = "");
        Task<PaginatedList<TEntity>> GetWithPaging(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 5);
        Task<TEntity> GetById(object id);
        void Add(TEntity entity);
        void Delete(object id);
        void Update(TEntity entityToUpdate);
    }
}
