using System;
using System.Security.Cryptography;
using System.Text;

namespace Fiap.Stack.BLL.Util
{
    public class Criptografia
    {
        public static string Cript(string texto)
        {
            var sha256Managed = new SHA256Managed();
            byte[] hashBytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(texto));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
