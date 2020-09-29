using PRC_Project.Data.Models;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetByUserName(string username, string action = "");
        Task<Users> CreateUser(RegisterModel model, string password);
        Task<bool> CheckPassWord(string username, string password);
        //Task<bool> DeleteUser(string username);
    }
}
