 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Services.Diario;
using YouthProtectionAplication.Models;
using System.Windows.Input;
using YouthProtectionAplication.Views.Diario;

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
            Diarios = new ObservableCollection<Postagem>();

            _ = ObterPostagens();


        }
        public ICommand NovaPostagem { get;}
        public ICommand RemoverPostagemCommand { get; set; }


        #region Métodos
        public async Task ObterPostagens()
        {
            try
            {
                Diarios = await pdiarioService.GetPostagemAsyncPerId();
                OnPropertyChanged(nameof(Diarios));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                  .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }


        public async Task RemoverPostagem(Postagem p)
        {
            try
            {
                if (await Application.Current.MainPage
                    .DisplayAlert("Confirmação", "Confirma a remoção desta postagem?", "Sim", "Não"))
                {
                    await pdiarioService.DeletePostagemAsync(p.idPostagem);

                    await Application.Current.MainPage.DisplayAlert("Mensagem",
                       "Postagem Removida com sucesso!", "OK");

                    _ = ObterPostagens();
                }
            }
            catch (Exception ex )
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }
        public async Task SelecionarPostagem()
        {
            try
            {
                Application.Current.MainPage = new DiarioCreatePostUser();
            }
            catch (Exception ex)
            {
                      await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }
    }

        #endregion
}