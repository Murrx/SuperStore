using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStoreLibrary.Util
{
    public class PasswordUtils
    {
        public static string GeneratePassword(string username)
        {
            string password = "";
            foreach (char c in username)
            {
                password += ((char)(c + 1));
            }
            return password;
        }
        public static string HashPassword(string password)
        {
            return string.Format("{0:X}", password.GetHashCode());
        }
    }
}
