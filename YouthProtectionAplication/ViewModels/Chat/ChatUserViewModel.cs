using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using YouthProtectionAplication.Models;
using YouthProtectionAplication.Services.Chat;

namespace YouthProtectionAplication.ViewModels.Chat
{
    public class ChatUserViewModel : BaseViewModel
    {
        public ObservableCollection<ChatUser> Messages { get; set; }

      

        private ChatService chatService;
        private readonly System.Timers.Timer chatTimer;
        private long _lastMessageId;
        public ChatUserViewModel(Postagem postagem)
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            chatService = new ChatService(token);
            Postagem p = postagem;
            Preferences.Set("PostagemId", p.publicationId);
            Messages = new ObservableCollection<ChatUser>();

            PublicationContent = p.PublicationContent;
            PublicationId = p.publicationId;
            _lastMessageId = 0;

            EnviarMensagemCommand = new Command(async () => await EnviarMensagemAsync());

            chatTimer = new System.Timers.Timer(5000);
            chatTimer.Elapsed += async (sender, e) => await TimerElapsedAsync();
            chatTimer.Start();

        }
      public ICommand EnviarMensagemCommand { get; }



        private string nome = Preferences.Get("UsuarioUsername", string.Empty);
        private long publicationId;
        private string publicationContent = string.Empty;
        private string mensagemAtual;
        private string dataConvertida;
        private bool _isFetching;


        #region Atributos Propriedades


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

        public long PublicationId
        {
            get => publicationId;
            set
            {
                publicationId = value;
                OnPropertyChanged();
            }
        }

        public string PublicationContent
        {
            get => publicationContent;
            set
            {
                if (publicationContent != value)
                {
                    publicationContent = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MensagemAtual
        {
            get => mensagemAtual;
            set
            {
                mensagemAtual = value;
                OnPropertyChanged(nameof(MensagemAtual));
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


        #endregion



        #region Métodos

        
       

        public async Task GetAllMessagesAsync()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            try
            {
                _isFetching = true; // Marca que a busca está em andamento

                var mensagens = await chatService.ObterMensagensAsync();

                var novasMensagens = mensagens.Where(m => m.Id > _lastMessageId).ToList();

                if (novasMensagens.Count > 0)
                {
                    foreach (var mensagem in novasMensagens)
                    {
                        Messages.Add(mensagem);

                    }
                    _lastMessageId = novasMensagens.Max(m => m.Id); // Atualiza o último ID

                }

          
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }

            finally
            {
                _isFetching = false; // Marca que a busca foi concluída
            }
        }


        private async Task EnviarMensagemAsync()
        {
            var novaMensagem = new ChatUser
            {
                ChatId = 8,
                SenderId = 1,
                SentAt = DateTime.Now,
                Content = MensagemAtual
            };

            
            await chatService.EnviarMensagemAsync(novaMensagem);
            Messages.Add(novaMensagem);
            MensagemAtual = string.Empty;

            if (novaMensagem.Id < _lastMessageId)
            {
                _lastMessageId = _lastMessageId + 1;
            }

        }

        private async Task TimerElapsedAsync()
        {
            if (!_isFetching)
            {
                await GetAllMessagesAsync();
            }
        }







        #endregion
    }
}
