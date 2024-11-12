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
using YouthProtectionAplication.Models.Enums;
using System.ComponentModel;
using YouthProtectionAplication.Views.Chat;


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

            ReativarPostagemCommand =
                new Command<Postagem>(async (Postagem p) => { await ReativarPostagem(p); });

            EditarCommand = new Command<Postagem>(OnEditarPostagem);

            InicializarChatCommand =
                new Command<Postagem>(async (Postagem postagem) => { await InicializarChat(postagem); });


        }

        public void InicializarCommands()
        {
            ExibirPerfilCommand = new Command(async () => await PerfilExibir());
        }

        public ICommand NovaPostagemPopUpCommand { get; }
        public ICommand RemoverPostagemCommand { get; set; }

        public ICommand InicializarChatCommand { get; set; }
        public ICommand ExibirPerfilCommand { get; set; }
        public ICommand ReativarPostagemCommand {  get; set; }
        public ICommand EditarCommand { get; set; }





        #region AtributosPropriedades

        private string nome = Preferences.Get("UsuarioUsername", string.Empty);
        private long userId = Preferences.Get("UsuarioId", 0L);
        private string dataConvertida;
        private Postagem _postagemSelecionada;
        private bool _isPopupVisible;
        private Postagem _postagemSelecionadaChat;


        public Postagem PostagemSelecionadaChat
        {
            get => _postagemSelecionadaChat;
            set
            {
                if (_postagemSelecionadaChat != value)
                {
                    _postagemSelecionadaChat = value;
                    OnPropertyChanged();
                    InicializarChat(_postagemSelecionadaChat);
                }
            }
        }

        public string Nome
        {
            get => nome;
            set
            {
                if (nome != value) 
                {
                    nome = value;
                    OnPropertyChanged();
                }
            }
        }

        public long UserId
        {
            get => userId;
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged();
                }
            }
        }


        public Postagem PostagemSelecionada
        {
            get => _postagemSelecionada;
        set
        {
            if (_postagemSelecionada != value)
            {
                _postagemSelecionada = value;
                OnPropertyChanged();
            }
         }
        }

        public string DataConvertida
        {
            get => dataConvertida;
            set
            {
                if (dataConvertida != value)
                {
                    dataConvertida = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsPopupVisible
        {
            get => _isPopupVisible;
            set
            {
                if (_isPopupVisible != value)
                {
                    _isPopupVisible = value;
                    OnPropertyChanged();
                }
            }
        }




        private async void OnEditarPostagem(Postagem postagem)
        {
            
                PostagemSelecionada = postagem;
            Application.Current.MainPage = new DiarioEditPostUser(postagem);


        }

        private async Task InicializarChat(Postagem postagem)
        {

            PostagemSelecionadaChat = postagem;
            Application.Current.MainPage = new ChatViewUser(postagem, userId);

        }



        public void CancelarEdicao()
        {
            IsPopupVisible = false;
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


        public async Task ReativarPostagem(Postagem p)
        {
            try
            {
                if (await Application.Current.MainPage
                    .DisplayAlert("Confirmação", "Confirma a reativação desta postagem?", "Sim", "Não"))
                {
                    Postagem pp = new Postagem()
                    {
                        publicationId = p.publicationId,
                        PublicationContent = p.PublicationContent,
                        PublicationStatus = 0,
                        

                    };
                    await pdiarioService.PutPostagemAsync(pp);

                    await Application.Current.MainPage
                   .DisplayAlert("Mensagem", "Publicação Reativada com Sucesso", "Ok");

                    _ = ObterPostagensExcluidas();
                }
            }
            catch(Exception ex) 
            {
                await Application.Current.MainPage
                   .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
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