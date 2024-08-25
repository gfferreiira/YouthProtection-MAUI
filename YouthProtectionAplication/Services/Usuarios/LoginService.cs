using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models.Login;
using YouthProtectionAplication.Helper;
using Flurl;
using Flurl.Http;

namespace YouthProtectionAplication.Services.Usuarios
{
    public class LoginService : ILoginService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }


        //Request na API para criar Conta
        public async Task<bool> CreateAccount(CreateAccountRequest createAccountRequest)
        {
            try
            {
                var response = await Constants.BASE_URL
                      .AppendPathSegment("/users")  //Colocar direção da API
                      .PostJsonAsync(createAccountRequest);

                return response.ResponseMessage.IsSuccessStatusCode;
            }
            catch (FlurlHttpException ex)
            {
                Console.Write(ex.Message);
            }
            return false;
        }
    }
}
