using AutoMapper;
using FirebaseAdmin.Auth;
using PRC_Project.Data.Models;
using PRC_Project.Data.UnitOfWork;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services.Authenticate
{
    public class AuthenticateService: IAuthenticateService
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public AuthenticateService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<UserModel> LoginGoogle(FirebaseToken userToken)
        {
            string email = userToken.Claims["email"].ToString();

            Users users = await _uow.UserGenRepository.GetFirst(filter: el => el.Email == email, includeProperties: "Role");

            if (users == null)
            {
                _uow.UserGenRepository.Add(new Users()
                {
                    Username = "Customer",
                    Email = email,
                    FullName = userToken.Claims["name"].ToString(),
                    InsBy = "Admin",
                    InsDatetime = DateTime.Now,
                    UpdBy = "Admin",
                    UpdDatetime = DateTime.Now,
                });

                if (await _uow.SaveAsync() > 0)
                {
                    users = await _uow.UserGenRepository.GetFirst(filter: el => el.Email == email, includeProperties: "Role");
                }
                else
                {
                    throw new Exception("Create new account failed");
                }
            }
            return _mapper.Map<UserModel>(users);
        }
    }
}
