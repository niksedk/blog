using SubIt.Data.Security;

namespace SubIt.Features.Security
{
    public interface IJwt
    {
        string GenerateJsonWebToken(SubItUser user);
    }
}