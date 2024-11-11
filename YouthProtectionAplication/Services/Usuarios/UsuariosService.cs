using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models;

namespace YouthProtectionAplication.Services.Usuarios
{
    public class UsuariosService
    {

        private readonly Request _request;

        private const string _apiUrlBase = "http://localhost:5189/Auth";  //URL DA API VAI FICAR AQUI

        public UsuariosService()
        {
            _request = new Request();
        }

        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Register";
            u =  await _request.PostAsync(_apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Login";
            u = await _request.PostAsync(_apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }


        public async Task<Usuario> GetUsuarioAsync(string token)
        {
            string urlComplementar = string.Format("/Auth/user/1", token);
            var usuario = await _request.GetAsync<Models.Usuario>(_apiUrlBase + urlComplementar, string.Empty);
            return usuario;
        }

        public async Task<Usuario> PutUsuarioAsync(Usuario u)
        {
            string urlComplementar = string.Format("/Update");
            var usuario = await _request.PutAsync(_apiUrlBase + urlComplementar,u, u.Token);
            return usuario;
        }
    }
}
