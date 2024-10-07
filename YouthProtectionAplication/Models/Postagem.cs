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
       
        public long publicationId { get; set; }
        public string PublicationContent { get; set; }
        public int PublicationStatus { get; set; }
        public DateTime DataPostagem { get; set; }

        public TipoPostagemEnum TipoPost { get; set; } // enum de tipo de postagem

    }
}

