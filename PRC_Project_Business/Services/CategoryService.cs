using AutoMapper;
using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using PRC_Project.Data.UnitOfWork;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project_Business.Services
{
    public class CategoryService : BaseService<Category, CategoryModel>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        protected override IGenericRepository<Category> _reponsitory => _unitOfWork.CategoryRepository;
    }
}
