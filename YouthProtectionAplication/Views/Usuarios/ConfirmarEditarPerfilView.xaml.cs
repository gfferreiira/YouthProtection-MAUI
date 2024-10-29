using YouthProtectionAplication.ViewModels.Usuarios;

namespace YouthProtectionAplication.Views.Usuarios;

public partial class ConfirmarEditarPerfilView : ContentPage
{
    UsuarioViewModel _usuarioViewModel;
    public ConfirmarEditarPerfilView()
	{
		InitializeComponent();
        _usuarioViewModel = new UsuarioViewModel();
        BindingContext = _usuarioViewModel;
        _ = _usuarioViewModel.RetornarUsuario();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
           if (await Application.Current.MainPage
                 .DisplayAlert("Confirmação", "Deseja realmente sair? As alterações não serão feitas", "Sim", "Não"))
            {
                Application.Current.MainPage = new AppShell();
            }
        }
        catch 
        {

        }
        
    }
}