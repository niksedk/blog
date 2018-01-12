using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Data;
using Blog.Data.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Features.Security
{
    public class UserService : IUserService
    {
        private readonly IJwt _jwt;
        private readonly IPasswordHasher<BlogUser> _passwordHasher;
        private readonly BlogDbContext _context;
        private const int MaxWrongPasswordAttempts = 5;

        public UserService(IJwt jwt, IPasswordHasher<BlogUser> passwordHasher, BlogDbContext context)
        {
            _jwt = jwt;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        /// <summary>
        /// Login via user/password
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="password">Input password from UI</param>
        /// <param name="userAgent">useragent from browser</param>
        /// <returns>Message message, null if success</returns>
        public string LoginPassword(BlogUser user, string password, string userAgent)
        {
            if (user.WrongPasswordAttempts > MaxWrongPasswordAttempts)
            {
                return "Account has been locked due to too many wrong login attempts - reset of password required"; // TODO: auto-send password reset email
            }

            var success = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
            if (success)
            {
                user.WrongPasswordAttempts = 0;
                user.LastLogin = DateTime.UtcNow;
                _context.SaveChanges();
                return null;
            }
            else
            {
                user.WrongPasswordAttempts++;
                _context.SaveChanges();
                return "Incorrect login"; // use same message for "unknown user" and "wrong password" to make hacking harder
            }
        }

        public string LoginRefreshToken(string username, string password, string userAgent)
        {
            return null;
        }

        public string LoginRefreshToken(string refreshToken, string clientId)
        {
            return null;
        }

        public BlogUser GetUser(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return _context.Users.Include(p => p.Claims).FirstOrDefault(u => u.Email == email && u.DeletedTime == null);
        }
        public BlogUser GetUser(int userId)
        {
            return _context.Users.Include(p => p.Claims).FirstOrDefault(u => u.UserId == userId && u.DeletedTime == null);
        }

        public List<BlogUser> GetUsers()
        {
            return _context.Users.Where(u => u.DeletedTime == null).OrderByDescending(u => u.Created).ToList();
        }

        public string GenerateJsonWebToken(BlogUser user)
        {
            return _jwt.GenerateJsonWebToken(user);
        }

        public bool Delete(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.DeletedTime == null && u.UserId == userId);
            if (user == null)
                return false;

            user.DeletedTime = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public BlogUser Register(string name, string email, bool showEmail, string password, string userAgent, string ipAddress)
        {
            var user = new BlogUser
            {
                Email = email,
                Name = name,
                Created = DateTime.UtcNow,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return GetUser(user.Email);
        }

    }
}
