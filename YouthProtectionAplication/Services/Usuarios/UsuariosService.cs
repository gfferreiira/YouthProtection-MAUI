using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models.Login;

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
            u.Id = await _request.PostReturnIntAsync(_apiUrlBase + urlComplementar, u);

            return u;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Login";
            u = await _request.PostAsync(_apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }
    }
}
