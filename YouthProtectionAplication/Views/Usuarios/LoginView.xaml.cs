using YouthProtectionAplication.ViewModels.Usuarios;

namespace YouthProtectionAplication.Views.Usuarios;

public partial class LoginView : ContentPage
{
    UsuarioViewModel _usuarioViewModel;
    public LoginView()
	{
        InitializeComponent();

        _usuarioViewModel = new UsuarioViewModel();
        BindingContext = _usuarioViewModel;
       // Application.Current.UserAppTheme = AppTheme.Dark;
    }
}