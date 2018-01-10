using System.Text;

namespace Blog.Features.Shared
{
    internal static class UrlHelper
    {
        internal static string GenerateUrlFriendlyId(string title)
        {
            var sb = new StringBuilder(title.Length);
            foreach (var ch in title)
            {
                if (ch == ' ' || ch == '.')
                    sb.Append('-');
                else if ("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_".Contains(ch.ToString()))
                    sb.Append(ch);
            }
            return sb.ToString();
        }
    }
}
