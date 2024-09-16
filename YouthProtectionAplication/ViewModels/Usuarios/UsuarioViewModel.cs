using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YouthProtectionAplication.Contracs.Usuarios;
using YouthProtectionAplication.Models.Login;
using YouthProtectionAplication.Models.Login.Enums;
using YouthProtectionAplication.Services.Usuarios;
using YouthProtectionAplication.Views;
using YouthProtectionAplication.Views.Diario;
using YouthProtectionAplication.Views.Usuarios;


namespace YouthProtectionAplication.ViewModels.Usuarios
{
    public partial class UsuarioViewModel : BaseViewModel
    {
        private UsuariosService _uService;
        public ICommand CreateAccountCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand AutenticarCommand { get; set; }

        public UsuarioViewModel()
        {
            _uService = new UsuariosService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            CreateAccountCommand = new Command(async () => await CreateAccount());
            AutenticarCommand = new Command(async () => await AutenticarUsuario());

        }




        #region AtributosPropriedades


        private string fictionalName = string.Empty;
        private string password = string.Empty;
        private string email = string.Empty;
        private string city = string.Empty;
        private string uf = string.Empty;
        private string cellPhone = string.Empty;
        private string birthDate = string.Empty;
        private UsuarioRole role;

        //usuario no ato de LOGIN
        private string login = string.Empty;
        private string senha = string.Empty;
     

        public string FictionalName
        {
            get { return fictionalName; }
            set
            {
                fictionalName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }

        public string Uf
        {
            get { return uf; }
            set
            {
                uf = value;
                OnPropertyChanged();
            }
        }

        public string CellPhone
        {
            get { return cellPhone; }
            set
            {
                cellPhone = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }

        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged();
            }
        }
        public UsuarioRole Role
        {
            get { return role; }
            set
            {
                role = value;
                OnPropertyChanged();
            }
        }

        
        #endregion


        #region Métodos
        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.FictionalName = FictionalName;
                u.Password = Password;
                u.Email = Email;
                u.City = City;
                u.Uf = Uf;
                u.CellPhone = CellPhone;
                u.Role = UsuarioRole.User;
                u.BirthDate = BirthDate;
              

                var validator = new CreateAccountContract(u);

                if (!validator.IsValid)
                {
                    var messages =
                            validator
                            .Notifications
                            .Select(x => x.Message);

                    var sb = new StringBuilder();
                    foreach (var message in messages)
                        sb.Append($"{message}\n");

                    await Shell.Current.DisplayAlert("Atenção", sb.ToString(), "OK");

                    return;
                }


                Usuario uRegistrado = await _uService.PostRegistrarUsuarioAsync(u);

            

                if (uRegistrado != null)
                {
                    string mensagem = "Usuário Registrado com sucesso";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "OK");

                    await Application.Current.MainPage
                           .DisplayAlert("Informação", mensagem, "OK");
                            await Shell.Current.GoToAsync(nameof(LoginView));
                    return;
                }
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + "Detalhes:" + ex.InnerException, "Ok");
            }


        }
        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Email = Login;
                usuario.Password = Senha;

                var validator = new LoginContract(usuario);

                if (!validator.IsValid)
                {
                    var messages =
                            validator
                            .Notifications
                            .Select(x => x.Message);

                    var sb = new StringBuilder();
                    foreach (var message in messages)
                        sb.Append($"{message}\n");

                    await Shell.Current.DisplayAlert("Atenção", sb.ToString(), "OK");

                    return;
                }

                Usuario uAutenticado = await _uService.PostAutenticarUsuarioAsync(usuario);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem Vindo {usuario.FictionalName}";



                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioToken", uAutenticado.Token);
                    Preferences.Set("UsuarioUsername", uAutenticado.FictionalName);
                    Preferences.Set("UsuarioEmail", uAutenticado.Email);

                    await Application.Current.MainPage
                       .DisplayAlert("Informação", mensagem, "OK");

                    await Shell.Current.GoToAsync(nameof(DiarioViewUser));
                }

                else
                {
                    await Application.Current.MainPage
                           .DisplayAlert("Informação", "Dados Incorretos", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message + "Detalhes:" + ex.InnerException, "OK");


            }
        }

            #endregion

            #region Navegacao
            public async Task CreateAccount()
        {
            await Shell.Current.GoToAsync(nameof(CreateAccountPage));
        }



        #endregion

    }
}

