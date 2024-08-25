using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models.Login;

namespace YouthProtectionAplication.Contracs.Usuarios
{
    public class CreateAccountContract : Contract<CreateAccountRequest>
    {
        public CreateAccountContract(CreateAccountRequest createAccount)
        {
            Requires()
                .IsEmail(createAccount.Email, nameof(createAccount.Email), "Email Invalido")
                .IsNotNullOrEmpty(createAccount.Email , nameof(createAccount.Email), "Email Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(createAccount.PhoneNumber, nameof(createAccount.PhoneNumber), "Telefone Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(createAccount.Uf, nameof(createAccount.Uf), "UF Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(createAccount.City, nameof(createAccount.City), "Cidade Não pode estar vazio");


            Requires()
                .IsNotNullOrEmpty(createAccount.Password, nameof(createAccount.Password), "Senha Não pode estar vazio");

            Requires()
                .IsNotNullOrEmpty(createAccount.Name, nameof(createAccount.Name), "Nome Não pode estar vazio");
        }
    }
}
