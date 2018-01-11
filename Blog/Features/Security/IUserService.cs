using System.Collections.Generic;
using Blog.Data.Security;

namespace Blog.Features.Security
{
    public interface IUserService
    {
        string LoginRefreshToken(string refreshToken, string clientId);
        string LoginRefreshToken(string username, string password, string userAgent);
        string LoginPassword(BlogUser user, string password, string userAgent);
        BlogUser GetUser(string email);
        BlogUser GetUser(int userId);
        string GenerateJsonWebToken(BlogUser user);
        List<BlogUser> GetUsers();
        bool Delete(int userId);
        BlogUser Register(string name, string email, bool showEmail, string password, string userAgent, string ipAddress);
    }
}