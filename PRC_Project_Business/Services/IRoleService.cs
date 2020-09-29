using PRC_Project.Data.Models;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services
{
    public interface IRoleService : IBaseService<Role, RoleModel>
    {
        string GetRole(Users user);
    }
}
