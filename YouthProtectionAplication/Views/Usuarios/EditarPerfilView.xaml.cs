using YouthProtectionAplication.ViewModels.Usuarios;

namespace YouthProtectionAplication.Views.Usuarios;

public partial class EditarPerfilView : ContentPage
{
    UsuarioViewModel _usuarioViewModel;
    public EditarPerfilView()
	{
		InitializeComponent();
        _usuarioViewModel = new UsuarioViewModel();
        BindingContext = _usuarioViewModel;
    }
}