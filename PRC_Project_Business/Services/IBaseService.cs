using PRC_Project.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services
{
    public interface IBaseService<TEntity, TDto> 
        where TEntity : class 
        where TDto : class
    {
        Task<bool> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(object id);
        Task<TDto> GetByIdAsync(object id);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<PaginatedList<TEntity>> GetWithPagingAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 5);
    }
}
