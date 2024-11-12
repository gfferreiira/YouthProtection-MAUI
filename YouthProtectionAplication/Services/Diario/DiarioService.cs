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

        private const string apiUrlBase = "http://localhost:5189/Publications";

        public DiarioService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<int> PostPostagemAsync(Postagem p)
        {
            string urlComplementar = string.Format("/CreatePublication");
            return await _request.PostReturnIntAsync(apiUrlBase + urlComplementar, p, _token);
            
        }

        public async Task<ObservableCollection<Postagem>> GetPostagemAsyncPerId()
        {
            string urlComplementar = string.Format("/publications/user");
            ObservableCollection<Models.Postagem> listaPersonagens = await
            _request.GetAsync<ObservableCollection<Models.Postagem>>(apiUrlBase + urlComplementar,
            _token);
            return listaPersonagens;
        }


        public async Task<Postagem> PutPostagemAsync(Postagem p)
        {
            string urlComplementar = string.Format("/publicationsUpdate/{0}", p.publicationId);
            var result = await _request.PutAsync(apiUrlBase + urlComplementar, p, _token);
            return result;
        }

        public async Task<string> DeletePostagemAsync(long idPostagem)
        {
            string urlComplementar = string.Format("/publication/{0}", idPostagem);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }


        public async Task<ObservableCollection<Postagem>> GetPostagemAsyncVoluntary()
        {
            string urlComplementar = string.Format("/Publications/");
            ObservableCollection<Models.Postagem> listaPersonagens = await
            _request.GetAsync<ObservableCollection<Models.Postagem>>(apiUrlBase + urlComplementar,
            _token);
            return listaPersonagens;
        }


    }
}
