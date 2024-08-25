using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouthProtectionAplication.Models.Login;

namespace YouthProtectionAplication.Services.Usuarios
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<bool> CreateAccount(CreateAccountRequest CreateAccountRequest);
    }
}
