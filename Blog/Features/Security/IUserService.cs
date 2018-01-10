using System.Collections.Generic;
using Blog.Data.Security;

namespace Blog.Features.Security
{
    public interface IUserService
    {
        string LoginRefreshToken(string refreshToken, string clientId);
        string LoginRefreshToken(string username, string password, string userAgent);
        string LoginPassword(SubItUser user, string password, string userAgent);
        SubItUser GetUser(string email);
        SubItUser GetUser(int userId);
        string GenerateJsonWebToken(SubItUser user);
        List<SubItUser> GetUsers();
        bool Delete(int userId);
    }
}