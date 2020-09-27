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
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(TDto dto);
        Task DeleteAsync(object keyValues);
        Task<TDto> GetByIdAsync(object keyValues);
        Task<PaginatedList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int page = 1);
    }
}
