using AutoMapper;
using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using PRC_Project.Data.UnitOfWork;
using PRC_Project.Data.ViewModels;
using System;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services
{
    public class CategoryService : BaseService<Category, CategoryModel>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        protected override IGenericRepository<Category> _reponsitory => _unitOfWork.CategoryRepository;

        public override async Task<CategoryModel> CreateAsync(CategoryModel dto)
        {
            var entity = _mapper.Map<Category>(dto);
            entity.CategoryId = Guid.NewGuid().ToString();
            entity.DelFlg = false;
            entity.InsDatetime = DateTime.Now;
            entity.InsBy = "Admin";
            entity.UpdDatetime = DateTime.Now;
            entity.UpdBy = "Admin";

            _reponsitory.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CategoryModel>(entity);
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            var entity = await _reponsitory.GetById(id);

            if (entity == null) throw new Exception("Not found entity object with id: " + id);

            entity.DelFlg = true;

            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
