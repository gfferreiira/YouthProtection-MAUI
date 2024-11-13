using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YouthProtectionAplication.Models;
using YouthProtectionAplication.Services.Diario;
using YouthProtectionAplication.Views.Chat;

namespace YouthProtectionAplication.ViewModels.Voluntario
{
    public class DiarioListagemViewModelVoluntary : BaseViewModel
    {
        private DiarioService diarioService;


        public ObservableCollection<Postagem> Diario { get; set; }
        public ObservableCollection<Postagem> FilteredItems { get; set; }
        public ObservableCollection<Postagem> AnotacoesFiltradas { get; set; }

        public DiarioListagemViewModelVoluntary()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);

            diarioService = new DiarioService(token);
            Diario = new ObservableCollection<Postagem>();
            FilteredItems = new ObservableCollection<Postagem>();
            AnotacoesFiltradas = new ObservableCollection<Postagem>();


            _ = ObterPostagens();

            InicializarChatCommand =
                new Command<Postagem>(async (Postagem postagem) => { await InicializarChat(postagem); });

        }
        public ICommand InicializarChatCommand { get; set; }


        private long userId = Preferences.Get("UsuarioId", 0L);
        private string dataConvertida;
        private Postagem _postagemSelecionadaChat;


        #region Atributos


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

        #endregion
        #region Métodos

        public async Task ObterPostagens()
        {
            try
            {

                Diario = await diarioService.GetPostagemAsyncVoluntary();

                FilteredItems = new ObservableCollection<Postagem>(Diario.Where(diario => diario.PublicationStatus == 0).ToList());

                AnotacoesFiltradas = new ObservableCollection<Postagem>(FilteredItems.Where(diario => diario.PublicationsRole == 0).ToList());

                OnPropertyChanged(nameof(AnotacoesFiltradas));

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                  .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private async Task InicializarChat(Postagem postagem)
        {
            try
            {
                if (await Application.Current.MainPage
                     .DisplayAlert("Confirmação", "Deseja Iniciar o Atendimento?", "Sim", "Não"))
                {
                    PostagemSelecionadaChat = postagem;
                    Application.Current.MainPage = new ChatViewVoluntary(postagem, userId);
                }
            }
            catch 
            {
                
            }
        }

        #endregion
    }
}
