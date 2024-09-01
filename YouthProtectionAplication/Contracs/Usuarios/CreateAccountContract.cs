using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models.Login;

namespace YouthProtectionAplication.Contracs.Usuarios
{
    public class CreateAccountContract : Contract<Usuario>
    {
        public CreateAccountContract(Usuario usuario)
        {
            Requires()
                .IsEmail(usuario.Email, nameof(usuario.Email), "Email Invalido")
                .IsNotNullOrEmpty(usuario.Email , nameof(usuario.Email), "Email Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(usuario.PhoneNumber, nameof(usuario.PhoneNumber), "Telefone Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(usuario.Uf, nameof(usuario.Uf), "UF Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(usuario.City, nameof(usuario.City), "Cidade Não pode estar vazio");


            Requires()
                .IsNotNullOrEmpty(usuario.Password, nameof(usuario.Password), "Senha Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(usuario.Name, nameof(usuario.Name), "Nome Não pode estar vazio");
        }
    }
}
