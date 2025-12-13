using Microsoft.AspNetCore.Identity;

namespace Library_Manager.Helpers
{
    public class PasswordHelper
    {
        public static string HashPassword(string username, string plainPassword)
        {
            var user = new IdentityUser { UserName = username };
            var passwordHasher = new PasswordHasher<IdentityUser>();
            return passwordHasher.HashPassword(user, plainPassword);
        }

        public static bool VerifyPassword(string username, string plainPassword, string hashedPassword)
        {
            // Nếu mật khẩu trong DB không phải Base64 hợp lệ → coi như là mật khẩu cũ (plain text)
            if (!IsBase64String(hashedPassword))
            {
                return plainPassword == hashedPassword; // So sánh trực tiếp (cũ)
            }
            var user = new IdentityUser { UserName = username };
            var passwordHasher = new PasswordHasher<IdentityUser>();
            var result = passwordHasher.VerifyHashedPassword(user, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }

        public static bool IsBase64String(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            Span<byte> buffer = new Span<byte>(new byte[input.Length]);
            return Convert.TryFromBase64String(input, buffer, out _);
        }
    }
}

