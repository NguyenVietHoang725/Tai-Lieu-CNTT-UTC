using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LibraryManagerApp.Helpers
{
    /// <summary>
    /// Utility class để hash và verify mật khẩu
    /// Sử dụng PBKDF2 với SHA256
    /// Tương thích với .NET Framework 4.x
    /// </summary>
    public static class PasswordHasher
    {
        // Số lần lặp cho PBKDF2 (càng cao càng bảo mật nhưng chậm hơn)
        private const int IterationCount = 10000;

        // Độ dài salt (16 bytes)
        private const int SaltSize = 128 / 8;

        // Độ dài hash (32 bytes)
        private const int HashSize = 256 / 8;

        /// <summary>
        /// Hash mật khẩu với salt ngẫu nhiên
        /// </summary>
        /// <param name="password">Mật khẩu gốc</param>
        /// <returns>Chuỗi Base64 chứa: Version + IterationCount + Salt + Hash</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            // Tạo salt ngẫu nhiên
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash password với PBKDF2
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: HashSize);

            // Kết hợp: [Version(1byte)] + [IterationCount(4bytes)] + [Salt(16bytes)] + [Hash(32bytes)]
            byte[] hashBytes = new byte[1 + 4 + SaltSize + HashSize];

            hashBytes[0] = 0x01; // Version 1
            Buffer.BlockCopy(BitConverter.GetBytes(IterationCount), 0, hashBytes, 1, 4);
            Buffer.BlockCopy(salt, 0, hashBytes, 5, SaltSize);
            Buffer.BlockCopy(hash, 0, hashBytes, 5 + SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verify mật khẩu với hash đã lưu
        /// </summary>
        /// <param name="hashedPassword">Hash đã lưu trong database</param>
        /// <param name="providedPassword">Mật khẩu user nhập vào</param>
        /// <returns>True nếu khớp, False nếu không khớp</returns>
        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentNullException(nameof(hashedPassword));

            if (string.IsNullOrEmpty(providedPassword))
                return false;

            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);

                // Kiểm tra độ dài tối thiểu
                if (hashBytes.Length < 1 + 4 + SaltSize + HashSize)
                    return false;

                // Đọc version
                byte version = hashBytes[0];
                if (version != 0x01)
                    return false;

                // Đọc iteration count
                int iterationCount = BitConverter.ToInt32(hashBytes, 1);

                // Đọc salt
                byte[] salt = new byte[SaltSize];
                Buffer.BlockCopy(hashBytes, 5, salt, 0, SaltSize);

                // Đọc hash gốc
                byte[] expectedHash = new byte[HashSize];
                Buffer.BlockCopy(hashBytes, 5 + SaltSize, expectedHash, 0, HashSize);

                // Hash password được cung cấp với cùng salt
                byte[] actualHash = KeyDerivation.Pbkdf2(
                    password: providedPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: iterationCount,
                    numBytesRequested: HashSize);

                // So sánh an toàn (constant-time comparison - manual implementation cho .NET Framework)
                return FixedTimeEquals(expectedHash, actualHash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Constant-time comparison để tránh timing attack
        /// (Thay thế cho CryptographicOperations.FixedTimeEquals trong .NET Core)
        /// </summary>
        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null)
                return false;

            if (left.Length != right.Length)
                return false;

            int result = 0;
            for (int i = 0; i < left.Length; i++)
            {
                result |= left[i] ^ right[i];
            }

            return result == 0;
        }

        /// <summary>
        /// Kiểm tra xem password hash có phải định dạng hợp lệ không
        /// </summary>
        public static bool IsHashedPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            try
            {
                byte[] hashBytes = Convert.FromBase64String(password);
                return hashBytes.Length >= 1 + 4 + SaltSize + HashSize;
            }
            catch
            {
                return false;
            }
        }
    }
}