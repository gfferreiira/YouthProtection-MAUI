using YouthProtectionAplication.ViewModels.Diario;

namespace YouthProtectionAplication.Views.Diario;

public partial class DiarioEditPostUser : ContentPage
{

	DiarioEdicaoViewModel diarioEdicaoViewModel;
	public DiarioEditPostUser(Models.Postagem postagem)
	{
        diarioEdicaoViewModel = new DiarioEdicaoViewModel(postagem);
        InitializeComponent();
		BindingContext = diarioEdicaoViewModel;

    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (await Application.Current.MainPage
                  .DisplayAlert("Confirmação", "Deseja realmente sair? As alterações não serão feitas", "Sim", "Não"))
            {
                Application.Current.MainPage = new AppShell(Preferences.Get("UsuarioRole", 0));
            }
        }
        catch
        {

        }

    }
}
