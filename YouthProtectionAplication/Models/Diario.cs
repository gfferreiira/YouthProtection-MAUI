using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models.Enums;

namespace YouthProtectionAplication.Models
{
    public class Postagem
    {
        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;

        public TipoPostagemEnum TipoPost { get; set; } // enum de tipo de postagem

    }
}

