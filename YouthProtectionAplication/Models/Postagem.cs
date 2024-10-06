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
       
        public int idPostagem { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string DataPostagem { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

        public TipoPostagemEnum TipoPost { get; set; } // enum de tipo de postagem

    }
}

