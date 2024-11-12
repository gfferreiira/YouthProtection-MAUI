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

      
        public long UsuarioIdAtual { get; set; }
        private ChatService chatService;
        private readonly System.Timers.Timer chatTimer;
        private long _lastMessageId;

        public ChatUserViewModel(Postagem postagem, long userId)
        {
            UsuarioIdAtual = Preferences.Get("UsuarioId", 0L);
            string token = Preferences.Get("UsuarioToken", string.Empty);
            chatService = new ChatService(token);
            Postagem p = postagem;
            Preferences.Set("PostagemId", p.publicationId);
            Messages = new ObservableCollection<ChatUser>();

            PublicationContent = p.PublicationContent;
            PublicationId = p.publicationId;
            _lastMessageId = 0;
            UsuarioIdAtual = userId;

            EnviarMensagemCommand = new Command(async () => await EnviarMensagemAsync());
            _ = LoadFirstIdAsync(p);


            chatTimer = new System.Timers.Timer(5000);
            chatTimer.Elapsed += async (sender, e) => await TimerElapsedAsync();
            chatTimer.Start();
        }
        public ICommand EnviarMensagemCommand { get; }



        private string nome = Preferences.Get("UsuarioUsername", string.Empty);
        
        private long publicationId;
        private long idChat;
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

        public long IdChat
        {
            get => idChat;
            set
            {
                if (idChat != value)
                {
                    idChat = value;
                    OnPropertyChanged();
                }
            }
        }



        #endregion



        #region Métodos

        public async Task<long> LoadFirstIdAsync(Postagem postagem)
        {

            IdChat = await chatService.ObterChatId(Preferences.Get("PostagemId", 0L));

            return IdChat;

        }
        
       

        public async Task GetAllMessagesAsync()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            try
            {
                _isFetching = true; // Marca que a busca está em andamento

                var mensagens = await chatService.ObterMensagensAsync(IdChat);

                var novasMensagens = mensagens.Where(m => m.Id > _lastMessageId).ToList();

                if (novasMensagens.Count > 0)
                {
                    foreach (var mensagem in novasMensagens)
                    {
                       if (mensagem.SenderId == UsuarioIdAtual)
                        {
                            mensagem.IsMessageFromCurrentUser = true;
                        }
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
            long idSender = Preferences.Get("UsuarioId", 0L);
            var novaMensagem = new ChatUser
            {
                ChatId = IdChat,
                SenderId = idSender,
                SentAt = DateTime.Now,
                Content = MensagemAtual
            };

            novaMensagem.IsMessageFromCurrentUser = true;
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
