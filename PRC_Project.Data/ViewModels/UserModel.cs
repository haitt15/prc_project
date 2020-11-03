using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.ViewModels
{
    public class UserModel : BaseModel
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string RoleId { get; set; }
        public virtual RoleModel Role { get; set; }


    }
}
