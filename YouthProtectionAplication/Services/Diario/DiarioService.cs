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

        public async Task<int> PostPostagemAsync(Postagem p)
        {
            return await _request.PostReturnIntAsync(apiUrlBase, p, _token);
        }

        public async Task<ObservableCollection<Postagem>> GetPostagemAsyncPerId()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Postagem> listaPersonagens = await
            _request.GetAsync<ObservableCollection<Models.Postagem>>(apiUrlBase + urlComplementar,
            _token);
            return listaPersonagens;
        }

        public async Task<Postagem> GetAllPostagemAsync(int idPostagem)
        {
            string urlComplementar = string.Format("/{0}", idPostagem);
            var postagem = await _request.GetAsync<Models.Postagem>(apiUrlBase + urlComplementar, _token);
            return postagem;
        }

        public async Task<int> PutPostagemAsync(Postagem p)
        {
            var result = await _request.PutAsync(apiUrlBase, p, _token);
            return result;
        }

        public async Task<int> DeletePostagemAsync(int idPostagem)
        {
            string urlComplementar = string.Format("/{0}", idPostagem);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }

    }
}
