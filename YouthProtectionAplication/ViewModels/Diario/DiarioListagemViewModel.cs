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
using YouthProtectionAplication.Views.Usuarios;

namespace YouthProtectionAplication.ViewModels.Diario
{
    public class DiarioListagemViewModel : BaseViewModel
    {
        private DiarioService pdiarioService;

        public ObservableCollection<Postagem> Diarios { get; set; }
        public ObservableCollection<Postagem> FilteredItems { get; set; }
        public ObservableCollection<Postagem> ExcludedItems { get; set; }

        public DiarioListagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            
            pdiarioService = new DiarioService(token);
            Diarios = new ObservableCollection<Postagem>();
            FilteredItems = new ObservableCollection<Postagem>();
            ExcludedItems = new ObservableCollection<Postagem>();

            _ = ObterPostagens();

            _ = ObterPostagensExcluidas();
            InicializarCommands();

            NovaPostagemPopUpCommand = new Command(ExibirPostagemPop);

            RemoverPostagemCommand =
                new Command<Postagem>(async (Postagem p) => { await RemoverPostagem(p); });


        }

        public void InicializarCommands()
        {
            ExibirPerfilCommand = new Command(async () => await PerfilExibir());
        }

        public ICommand NovaPostagemPopUpCommand { get; }
        public ICommand RemoverPostagemCommand { get; set; }
        public ICommand ExibirPerfilCommand { get; set; }



        #region AtributosPropriedades


        private string nome = Preferences.Get("UsuarioUsername", string.Empty);

        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged();
            }
        }



        private Postagem postagemSelecionado;
        public Postagem PostagemSelecionado
        {
            get { return postagemSelecionado; }
            set
            {
                if (value != null)
                {
                    postagemSelecionado = value;

                    Shell.Current
                        .GoToAsync($"diarioCreatePostUser?pId={postagemSelecionado.publicationId}");
                }
            }
        }




        #endregion

        #region Métodos
        public async Task ObterPostagens()
        {
            try
            {

                Diarios = await pdiarioService.GetPostagemAsyncPerId();

                FilteredItems = new ObservableCollection<Postagem>(Diarios.Where(diario => diario.PublicationStatus == 0).ToList());
                OnPropertyChanged(nameof(FilteredItems));

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
                    await pdiarioService.DeletePostagemAsync(p.publicationId);

                    await Application.Current.MainPage.DisplayAlert("Mensagem",
                       "Postagem Removida com sucesso!", "OK");

                    _ = ObterPostagens();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }


        public async void ExibirPostagemPop()
        {
            try
            {

                MessagingCenter.Send(this, "ExibirPostagemPop");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
              .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }

        public async Task ObterPostagensExcluidas()
        {
            try
            {

                Diarios = await pdiarioService.GetPostagemAsyncPerId();

                ExcludedItems = new ObservableCollection<Postagem>(Diarios.Where(diario => diario.PublicationStatus != 0).ToList());
                OnPropertyChanged(nameof(ExcludedItems));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                  .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }



        #endregion

        #region Navegação

        public async Task PerfilExibir()
        {
            try
            {
                Application.Current.MainPage = new EditarPerfilView();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }


        #endregion

    }
}