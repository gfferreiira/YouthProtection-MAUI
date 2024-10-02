using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models;

namespace YouthProtectionAplication.Services.Diario
{
    public class DiarioService
    {
        private readonly Request _request;

        private string _token;

        private const string apiUrlBase = "";

        public DiarioService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<int> PostPersonagemAsync(Postagem p)
        {
            return await _request.PostReturnIntAsync(apiUrlBase, p, _token);
        }

        public async Task<ObservableCollection<Postagem>> GetPostagemAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Postagem> listaPersonagens = await
            _request.GetAsync<ObservableCollection<Models.Postagem>>(apiUrlBase + urlComplementar,
            _token);
            return listaPersonagens;
        }

        public async Task<Postagem> GetPersonagemAsync(int personagemId)
        {
            string urlComplementar = string.Format("/{0}", personagemId);
            var personagem = await _request.GetAsync<Models.Postagem>(apiUrlBase + urlComplementar, _token);
            return personagem;
        }

        public async Task<int> PutPersonagemAsync(Postagem p)
        {
            var result = await _request.PutAsync(apiUrlBase, p, _token);
            return result;
        }

        public async Task<int> DeletePersonagemAsync(int PostagemId)
        {
            string urlComplementar = string.Format("/{0}", PostagemId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }

    }
}
