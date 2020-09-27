using AutoMapper;
using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using PRC_Project.Data.UnitOfWork;
using PRC_Project.Data.ViewModels;

namespace PRC_Project_Business.Services
{
    public class ProductService : BaseService<Product, ProductModel>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        protected override IGenericRepository<Product> _reponsitory => _unitOfWork.ProductRepository;
    }
}
