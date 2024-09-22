using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models.Enums;

namespace YouthProtectionAplication.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string FictionalName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;

        public UsuarioRole Role { get; set; } // enum de tipo de Usuário

        public string Token { get; set; } = string.Empty;
    }
}
