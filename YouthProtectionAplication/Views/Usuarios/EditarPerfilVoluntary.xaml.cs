using YouthProtectionAplication.ViewModels.Usuarios;

namespace YouthProtectionAplication.Views.Usuarios;

public partial class EditarPerfilVoluntary : ContentPage
{
    UsuarioViewModel _usuarioViewModel;
    public EditarPerfilVoluntary()
    {
        InitializeComponent();

        _usuarioViewModel = new UsuarioViewModel();
        BindingContext = _usuarioViewModel;
        _ = _usuarioViewModel.RetornarUsuario();

    }

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new ConfirmarEditarPerfilView();


    }

}