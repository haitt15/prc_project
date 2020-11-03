using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using System.Threading.Tasks;

namespace PRC_Project.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Orders> OrdersRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        IUserRepository UsersRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<UserDevice> UserDeviceRepository { get; }
        IGenericRepository<Users> UserGenRepository { get; }


        Task<int> SaveAsync();
    }
}
