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
        private Postagem postagem;

        

        public long publicationId { get; set; }
        public string PublicationContent { get; set; }
        public string Nome { get; set; } = Preferences.Get("UsuarioUsername", string.Empty);



        public DateTime CreatedAt { get; set; }

        public string DataConvertida { get; set; }
        public DateTime ModificationDate { get; set; }



        public TipoPostagemEnum PublicationsRole { get; set; } // enum de tipo de postagem
        public TipoStatusEnum PublicationStatus { get; set; }

}
}

