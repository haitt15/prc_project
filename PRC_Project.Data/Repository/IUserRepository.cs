using PRC_Project.Data.Models;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project.Data.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetByUsername(string username);
        Task<Users> Create(Users user, string password);
        void Delete(Users user);

    }
}
