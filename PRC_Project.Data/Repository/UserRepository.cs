using Microsoft.EntityFrameworkCore;
using PRC_Project.Data.Helper;
using PRC_Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC_Project.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Users> Create(Users user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }


            if (_context.Users.Any(x => x.Username == user.Username))
            {
                throw new AppException("Username \"" + user.Username + "\" is already taken");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.InsBy = Constants.Roles.ROLE_ADMIN;
            user.InsDatetime = DateTime.Now;
            user.UpdBy = Constants.Roles.ROLE_ADMIN;
            user.UpdDatetime = DateTime.Now;

            await _context.Users.AddAsync(user);
            return user;

        }

        public void Delete(Users user)
        {
            user.DelFlg = true;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetByUsername(string username)
        {
            var user = await _context.Users.Where(x => x.Username == username)
                                       .SingleOrDefaultAsync();
            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
