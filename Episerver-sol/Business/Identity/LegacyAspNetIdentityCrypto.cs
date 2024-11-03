﻿using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace EpiserverBH.Business.Identity
{
    internal static class LegacyAspNetIdentityCrypto
    {
        public static string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            byte[] salt;
            byte[] bytes;

            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }

            byte[] array = new byte[49];

            Buffer.BlockCopy(salt, 0, array, 1, 16);
            Buffer.BlockCopy(bytes, 0, array, 17, 32);

            return Convert.ToBase64String(array);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
                return false;

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            byte[] array = Convert.FromBase64String(hashedPassword);

            if (array.Length != 49 || array[0] != 0)
                return false;

            byte[] array2 = new byte[16];
            Buffer.BlockCopy(array, 1, array2, 0, 16);
            byte[] array3 = new byte[32];
            Buffer.BlockCopy(array, 17, array3, 0, 32);
            byte[] bytes;

            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, array2, 1000))
            {
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }

            return ByteArraysEqual(array3, bytes);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == b)
                return true;

            if (a == null || b == null || a.Length != b.Length)
                return false;

            bool equal = true;

            for (int i = 0; i < a.Length; i++)
            {
                equal &= a[i] == b[i];
            }

            return equal;
        }
    }
}
