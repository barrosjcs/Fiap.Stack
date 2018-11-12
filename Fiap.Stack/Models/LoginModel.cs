using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiap.Stack.MOD;

namespace Fiap.Stack.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public static explicit operator UsuarioMOD(LoginModel login)
        {
            return new UsuarioMOD
            {
                Login = login.Login,
                Senha = login.Senha
            };
        }
    }
}
