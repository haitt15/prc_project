using AutoMapper;
using FirebaseAdmin.Auth;
using PRC_Project.Data.Helper;
using PRC_Project.Data.Models;
using PRC_Project.Data.UnitOfWork;
using PRC_Project.Data.ViewModels;
using System;
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
        public async Task<UserModel> LoginGoogle(string uid)
        {
            UserRecord user_firebase = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

            var currentUser = await _uow.UsersRepository.GetByUsername(uid);

            if (currentUser == null)
            {
                var user_info = new Users()
                {
                    Username = uid,
                    RoleId = Constants.Roles.ROLE_USER_ID,
                    Email = user_firebase.Email,
                    Phonenumber = user_firebase.PhoneNumber,
                    FullName = user_firebase.DisplayName,
                    Address = "",
                    DelFlg = false,
                    Photo = user_firebase.PhotoUrl,
                    InsBy = Constants.Roles.ROLE_ADMIN,
                    InsDatetime = DateTime.Now,
                    UpdBy = Constants.Roles.ROLE_ADMIN,
                    UpdDatetime = DateTime.Now
                };

                await _uow.UsersRepository.Create(user_info, "123456");

                if (await _uow.SaveAsync() > 0)
                {
                    return _mapper.Map<UserModel>(user_info);
                }
                else
                {
                    throw new Exception("Create new account failed");
                }
            }
            return _mapper.Map<UserModel>(currentUser);
        }
    }
}
