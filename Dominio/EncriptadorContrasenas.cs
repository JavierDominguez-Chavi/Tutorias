using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EncriptadorContrasenas
    {
        public static string Encriptar(string contraseña) 
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder contraseñaEncriptada = new StringBuilder();
                for (int i = 0; i < (bytes.Length); i++)
                {
                    contraseñaEncriptada.Append(bytes[i].ToString("x2"));
                }
                return contraseñaEncriptada.ToString();
            }
        }
    }
}
