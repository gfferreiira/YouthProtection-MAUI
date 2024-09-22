using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models;

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
                .IsNotNullOrEmpty(usuario.CellPhone, nameof(usuario.CellPhone), "Telefone Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(usuario.Uf, nameof(usuario.Uf), "UF Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(usuario.City, nameof(usuario.City), "Cidade Não pode estar vazio");


            Requires()
                .IsNotNullOrEmpty(usuario.Password, nameof(usuario.Password), "Senha Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(usuario.FictionalName, nameof(usuario.FictionalName), "Nome Não pode estar vazio");
        }
    }
}
