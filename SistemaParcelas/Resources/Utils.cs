using System.Security.Cryptography;
using System.Text;

namespace SistemaParcelas.Resources
{
    public class Utils
    {
        public  static string EncryptPassword(string password)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 sha256 = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result =  sha256.ComputeHash(enc.GetBytes(password));

                foreach(byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
