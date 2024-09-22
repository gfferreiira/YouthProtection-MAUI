using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Maps;
using YouthProtectionAplication.Services.Diario;

namespace YouthProtectionAplication.ViewModels.Diario
{
    public class DiarioPostagemViewModel : BaseViewModel
    {
        private DiarioService UserDiarioService;

        public DiarioPostagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            UserDiarioService = new DiarioService(token);
        }

        private int idUser;
        private int idPostagem;
        private string titulo = string.Empty;
        private string texto = string.Empty;



    }
}
