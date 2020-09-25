using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using System;
using System.Threading.Tasks;

namespace PRC_Project.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork   
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            InitRepository();
        }

        private bool _disposed = false;
        public IGenericRepository<Product> ProductRepository { get; set; }

        private void InitRepository()
        {
            ProductRepository = new GenericRepository<Product>(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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
