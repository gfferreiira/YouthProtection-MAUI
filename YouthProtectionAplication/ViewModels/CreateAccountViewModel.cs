using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YouthProtectionAplication.Contracs.Usuarios;
using YouthProtectionAplication.Models.Login;
using YouthProtectionAplication.Services.Usuarios;
using YouthProtectionAplication.Views;
using YouthProtectionAplication.Views.Usuarios;


namespace YouthProtectionAplication.ViewModels
{
    [ObservableObject]
    public partial class CreateAccountViewModel
    {


            [ObservableProperty]
            string name;

            [ObservableProperty]
            string password;

            [ObservableProperty]
            string email;

            [ObservableProperty]
            string city;

            [ObservableProperty]
            string uf;

            [ObservableProperty]
            string phoneNumber;

            private readonly ILoginService _loginService;
            public CreateAccountViewModel(ILoginService loginService)
            {
                _loginService = loginService;
            }
            [RelayCommand]
            public async Task Save()
            {
                var request = new CreateAccountRequest
                {
                    Name = name,
                    Email = email,
                    City = city,
                    Uf = uf,
                    PhoneNumber = phoneNumber,
                    Password = password
                };
            var validator = new CreateAccountContract(request);

            if (!validator.IsValid)
            {
                await Application.Current.MainPage
                     .DisplayAlert("Informação", "Dados Invalidos", "Ok");
                return;
            }

            var result = await _loginService.CreateAccount(request);

                if(result)
                 {
                await Application.Current.MainPage
                .DisplayAlert("Informação", "Usuario Cadastrado!", "Ok");
                await Shell.Current.GoToAsync(nameof(MainPage));    //Verificar se quando o cadastro é criado, volta para a tela de login pela escola
                 }
            }

        }
    }

