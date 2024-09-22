 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Services.Diario;
using YouthProtectionAplication.Models;

namespace YouthProtectionAplication.ViewModels.Diario
{
    public class DiarioListagemViewModel : BaseViewModel
    {
        private DiarioService pdiarioService;

        public ObservableCollection<Postagem> Diarios { get; set; }

        public DiarioListagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pdiarioService = new DiarioService(token);

        }


        #region Métodos
        public async Task ObterPostagens()
        {
         
        }
    }

        #endregion
}