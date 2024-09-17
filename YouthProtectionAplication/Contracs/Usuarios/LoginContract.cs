using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models.Login;

namespace YouthProtectionAplication.Contracs.Usuarios
{
    public class LoginContract : Contract<Usuario>
    {
        public LoginContract(Usuario usuario)
        {
        
        Requires()
                .IsEmail(usuario.Email, nameof(usuario.Email), "E-mail is invalid")
                .IsNotNullOrEmpty(usuario.Email, nameof(usuario.Email), "E-mail is invalid");

        Requires()
                .IsNotNullOrEmpty(usuario.Password, nameof(usuario.Password), "Password is invalid");
    }
}
}

