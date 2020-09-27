using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRC_Project.Data.Helper;
using PRC_Project.Data.Repository;
using PRC_Project.Data.UnitOfWork;
using System;
using System.Collections.Generic;
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

        public virtual async Task<bool> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _reponsitory.Add(entity);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            if (id != null)
            {
                _reponsitory.Delete(id);

            }
            return await _unitOfWork.SaveAsync() > 0;
        }

        public virtual async Task<bool> UpdateAsync(TDto dto)
        {
            if (dto != null)
            {
                var entity = _mapper.Map<TEntity>(dto);
                _reponsitory.Update(entity);
            }
            return await _unitOfWork.SaveAsync() > 0;
        }

        public virtual async Task<TDto> GetByIdAsync(object id)
        {
            if (id != null)
            {
                return _mapper.Map<TDto>(await _reponsitory.GetById(id));
            }
            return null;
        }

        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return _reponsitory.Get(filter, orderBy, includeProperties);
        }

        public Task<PaginatedList<TEntity>> GetWithPagingAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 5)
        {
            return _reponsitory.GetWithPaging(filter, orderBy, includeProperties, pageIndex, pageSize);
        }
    }
}
