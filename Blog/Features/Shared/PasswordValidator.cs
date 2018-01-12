using System.Text.RegularExpressions;

namespace Blog.Features.Shared
{
    public static class PasswordValidator
    {
        public static readonly string RegExPasswordString = @"^(?=.*\d)(?=.*[\p{Ll}])(?=.*[\p{Lu}]).{8,64}$";
        private static readonly Regex RegExPassword = new Regex(RegExPasswordString, RegexOptions.Compiled);

        public static bool IsPassWordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return RegExPassword.IsMatch(password);
        }
    }
}
