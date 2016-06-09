using System;
using System.Text.RegularExpressions;

namespace MyWeddingSystem.Utils
{
    public static class PasswordGenerator
    {
        public static string NewPassword()
        {
            var guid = Guid.NewGuid().ToString();
            Regex.Replace(guid, "[^0-9a-zA-Z]+", "");

            Random rnd = new Random();
            var start = rnd.Next(0, guid.Length-8);

            return guid.Substring(start, 8);
        }
    }
}