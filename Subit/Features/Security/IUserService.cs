using System.Collections.Generic;
using SubIt.Data.Security;

namespace SubIt.Features.Security
{
    public interface IUserService
    {
        string LoginRefreshToken(string refreshToken, string clientId);
        string LoginRefreshToken(string username, string password, string userAgent);
        string LoginPassword(SubItUser user, string password, string userAgent);
        SubItUser GetUser(string email);
        string GenerateJsonWebToken(SubItUser user);
        List<SubItUser> GetUsers();
        bool Delete(int userId);
    }
}