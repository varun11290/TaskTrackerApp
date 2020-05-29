using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTracker.API.Data;
using TaskTracker.API.Models;

namespace TaskTracker.API.Repo
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataDBContext _context;

        public AuthRepo(DataDBContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userName);
            if (user == null)
                return null;

            if (!VarifyUserPassword(password, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        private bool VarifyUserPassword(string passwordToCheck, byte[] passwordHash, byte[] passwordKey)
        {
            using (var hashset = new System.Security.Cryptography.HMACSHA512(passwordKey))
            {
                var computedPassword = hashset.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordToCheck));

                for (int i = 0; i < computedPassword.Length; i++)
                {
                    if (computedPassword[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordKey;

            CreatePasswordHash(password, out passwordHash, out passwordKey);

            user.Password = passwordHash;
            user.PasswordKey = passwordKey;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey)
        {
            using (var hashset = new System.Security.Cryptography.HMACSHA512())
            {
                passwordKey = hashset.Key;
                passwordHash = hashset.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }


        }

        public async Task<bool> UserExist(string userName)
        {
            if (await _context.Users.AnyAsync(u => u.Name == userName))
                return true;

            return false;
        }
    }
}