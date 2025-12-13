using Microsoft.AspNetCore.Identity;

namespace LibraryManagerApp.Helpers
{
    public static class IdentityPasswordHelper
    {
        private static readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        // Dùng để verify hash của ASP.NET Identity
        public static bool VerifyIdentityPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(providedPassword))
                return false;

            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }

        // (Tùy chọn) Nếu bạn muốn hash mật khẩu mới trong WinForms
        public static string HashIdentityPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }
    }
}
