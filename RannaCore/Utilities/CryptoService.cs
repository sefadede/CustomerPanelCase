using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RannaCore.Utilities
{
    public static class CryptoService
    {
        public static string CreateMD5(string input)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }
        public static bool CompareMD5(string input, string hashInput)
        {
            bool same = false;
            string convertedHash = CreateMD5(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(convertedHash, hashInput)) same = true;
            convertedHash = String.Empty;
            return same;

        }
    }
}
