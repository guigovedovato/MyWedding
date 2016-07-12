using System;
using System.Text.RegularExpressions;

namespace MyWeddingSystem.Utils
{
    public static class PasswordGenerator
    {
        public static string NewPassword()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random rnd = new Random();
            var start = rnd.Next(0, guid.Length-8);

            return guid.Substring(start, 8);
        }
    }
}