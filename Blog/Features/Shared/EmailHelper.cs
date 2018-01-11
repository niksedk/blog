using System.Text.RegularExpressions;

namespace Blog.Features.Shared
{
    public static class EmailHelper
    {
        public const string RegExEmailString = @"^[ÆØÅæøåA-Za-z0-9._%+-]+@(?:[ÆØÅæøåA-Za-z0-9-]+\.)+[A-Za-z]{2,12}$";
        public static Regex RegExEmail = new Regex(RegExEmailString, RegexOptions.Compiled);


        internal static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return RegExEmail.IsMatch(email);
        }
    }
}
