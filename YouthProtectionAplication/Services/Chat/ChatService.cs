﻿using System;
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

        public async Task<List<ChatUser>> ObterMensagensAsync(long idPostagem)
        {
            string urlComplementar = string.Format("/GetChatBy" + "/{0}", idPostagem);
            var ChatId = await _request.GetAsync<Models.ChatUser>(apiUrlBase + urlComplementar, _token);
         
            urlComplementar = string.Format("/messages" + "/{0}", ChatId);
            ObservableCollection<Models.ChatUser> chatMessages =  await _request.GetAsync<ObservableCollection <Models.ChatUser>> ( apiUrlBase + urlComplementar, _token);
            return new List<ChatUser>(chatMessages); 
        }

        public async Task EnviarMensagemAsync(ChatUser novaMensagem)
        {
            string urlComplementar = "/send";
            await _request.PostAsync(apiUrlBase + urlComplementar, novaMensagem, _token);
        }

        public async Task<ChatUser> ObterChatId(long idPostagem)
        {
            string urlComplementar = string.Format("/GetChatBy" + "/{0}", idPostagem);
            var chatId = await _request.GetAsync<Models.ChatUser>(apiUrlBase + urlComplementar, _token);
            return chatId;
        }



    }
}
