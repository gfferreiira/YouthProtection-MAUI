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

        public async Task<List<ChatUser>> ObterMensagensAsync()
        {
            string urlComplementar = string.Format("/messages/8");
            ObservableCollection<Models.ChatUser> chatMessages =  await _request.GetAsync<ObservableCollection <Models.ChatUser>> ( apiUrlBase + urlComplementar, _token);
            return new List<ChatUser>(chatMessages); 
        }

        public async Task EnviarMensagemAsync(ChatUser novaMensagem)
        {
            string urlComplementar = "/send";
            await _request.PostAsync(apiUrlBase + urlComplementar, novaMensagem, _token);
        }



    }
}
