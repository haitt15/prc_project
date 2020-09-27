using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using System.Threading.Tasks;

namespace PRC_Project.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        Task SaveAsync();
    }
}
