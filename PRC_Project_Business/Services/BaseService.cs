using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRC_Project.Data.Helper;
using PRC_Project.Data.Repository;
using PRC_Project.Data.UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services
{
    public abstract class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected abstract IGenericRepository<TEntity> _reponsitory { get; }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _reponsitory.Add(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TDto>(entity);
        }

        public Task DeleteAsync(object keyValues)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, 
                                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
                                                   int page = 1)
        {
            var query = _reponsitory.Get(filter);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            const int PageSize = 5;
            return await PaginatedList<TEntity>.CreateAsync(query.AsNoTracking(), page, PageSize);
        }

        public Task<TDto> GetByIdAsync(object keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> UpdateAsync(TDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
