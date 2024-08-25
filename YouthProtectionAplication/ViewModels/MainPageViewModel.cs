using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YouthProtectionAplication.Services.Usuarios;
using YouthProtectionAplication.Views;
using YouthProtectionAplication.Views.Usuarios;

namespace YouthProtectionAplication.ViewModels
{
    public partial class MainPageViewModel
    {
        private readonly ILoginService _loginService;



        public MainPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;

        }


        public async Task Login()
        {
            await _loginService.LoginAsync(new Models.Login.LoginRequest
            {


            });


        }

        [RelayCommand]
        public async Task CreateAccount()
        {
            await Shell.Current.GoToAsync(nameof(CreateAccountPage));
        }
    }
}
