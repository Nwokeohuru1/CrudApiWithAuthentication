using System.Security.Cryptography;
using System.Text;

namespace CrudApiWithAuthentication.Helpers
{
    public class PasswordHash
    {
        public static string Hash(string password)
        {
            var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));              

            StringBuilder stringbuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringbuilder.Append(bytes[i].ToString("x2"));
            }
            return stringbuilder.ToString();
            // return Convert.ToBase64String(hashedPassword); 
        }






    }
}
