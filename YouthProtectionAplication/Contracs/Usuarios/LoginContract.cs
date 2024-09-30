using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models;

namespace YouthProtectionAplication.Contracs.Usuarios
{
    public class LoginContract : Contract<Usuario>
    {
        public LoginContract(Usuario usuario)
        {
        
        Requires()
                .IsEmail(usuario.Email, nameof(usuario.Email), "Por gentileza, digitar um e-mail valido")
                .IsNotNullOrEmpty(usuario.Email, nameof(usuario.Email), "E-mail não pode estar vazio");

        Requires()
                .IsNotNullOrEmpty(usuario.Password, nameof(usuario.Password), "Senha Não pode estar vazio");
    }
}
}

