using AutoMapper;
using PRC_Project.Data.Helper;
using PRC_Project.Data.Models;
using PRC_Project.Data.Repository;
using PRC_Project.Data.UnitOfWork;
using PRC_Project.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services
{
    public class RoleService : BaseService<Role, RoleModel>, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        protected override IGenericRepository<Role> _reponsitory => _unitOfWork.RoleRepository;

        public override async Task<RoleModel> CreateAsync(RoleModel dto)
        {
            var entity = _mapper.Map<Role>(dto);
            entity.DelFlg = false;
            entity.InsDatetime = DateTime.Now;
            entity.InsBy = Constants.Roles.ROLE_ADMIN;
            entity.UpdDatetime = DateTime.Now;
            entity.UpdBy = Constants.Roles.ROLE_ADMIN;

            _reponsitory.Add(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RoleModel>(entity);
        }

        public string GetRole(Users user)
        {
            var role = _reponsitory.GetByObject(u => u.RoleId == user.RoleId).SingleOrDefault();
            return role.RoleNm;
        }
    }
}
