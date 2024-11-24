using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models;
using System.Net.WebSockets;
using System.Collections.ObjectModel;

namespace YouthProtectionAplication.Services.Chat
{
     public class ChatService
    {
        private ClientWebSocket _clientSocket;

        private readonly Request _request;

        private string _token;

        private const string apiUrlBase = "https://www.well.somee.com/Chat";

        public ChatService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<List<ChatUser>> ObterMensagensAsync(long IdChat)
        {
           string urlComplementar = string.Format("/messages" + "/{0}", IdChat);
            ObservableCollection<Models.ChatUser> chatMessages =  await _request.GetAsync<ObservableCollection <Models.ChatUser>> ( apiUrlBase + urlComplementar, _token);
            return new List<ChatUser>(chatMessages); 
        }

        public async Task EnviarMensagemAsync(ChatUser novaMensagem)
        {
            string urlComplementar = "/send";
            await _request.PostAsync(apiUrlBase + urlComplementar, novaMensagem, _token);
        }

        public async Task<long> ObterChatId(long idPostagem)
        {
            string urlComplementar = string.Format("/GetChatBy" + "/{0}", idPostagem);
            var response = await _request.GetAsync<Models.Response.ResponseModel>(apiUrlBase + urlComplementar, _token);
            return response.Id;
        }

        public async Task<Response.ResponseModel> ObterChat(long idPostagem)
        {
            string urlComplementar = string.Format("/GetChatBy" + "/{0}", idPostagem);
            var response = await _request.GetAsync<Models.Response.ResponseModel>(apiUrlBase + urlComplementar, _token);
            return response;
        }



    }
}
