using CommunityToolkit.Maui.Views;
using YouthProtectionAplication.ViewModels.Usuarios;
using YouthProtectionAplication.Views.Chat;

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