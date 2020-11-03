using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using System;
using System.Threading.Tasks;

namespace PRC_Project.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            InitRepository();
        }

        private bool _disposed = false;
        public IGenericRepository<Product> ProductRepository { get; set; }

        public IGenericRepository<Category> CategoryRepository { get; set; }

        public IGenericRepository<Orders> OrdersRepository { get; set; }

        public IGenericRepository<OrderDetail> OrderDetailRepository { get; set; }

        public IUserRepository UsersRepository { get; set; }

        public IGenericRepository<Role> RoleRepository { get; set; }

        public IGenericRepository<UserDevice> UserDeviceRepository { get; set; }

        public IGenericRepository<Users> UserGenRepository { get; set; }

        private void InitRepository()
        {
            ProductRepository = new GenericRepository<Product>(_context);
            CategoryRepository = new GenericRepository<Category>(_context);
            OrdersRepository = new GenericRepository<Orders>(_context);
            OrderDetailRepository = new GenericRepository<OrderDetail>(_context);
            UsersRepository = new UserRepository(_context);
            RoleRepository = new GenericRepository<Role>(_context);
            UserDeviceRepository = new GenericRepository<UserDevice>(_context);
            UserGenRepository = new GenericRepository<Users>(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
