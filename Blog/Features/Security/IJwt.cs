using Blog.Data.Security;

namespace Blog.Features.Security
{
    public interface IJwt
    {
        string GenerateJsonWebToken(BlogUser user);
    }
}