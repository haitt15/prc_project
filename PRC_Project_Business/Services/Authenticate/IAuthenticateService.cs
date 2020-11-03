using FirebaseAdmin.Auth;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services.Authenticate
{
    public interface IAuthenticateService
    {
        public Task<UserModel> LoginGoogle(FirebaseToken userToken);

    }
}
