using CommunityToolkit.Maui.Views;
using YouthProtectionAplication.ViewModels.Diario;

namespace YouthProtectionAplication.Views.Diario;

public partial class DiarioCreatePostUser : Popup
{
    DiarioPostagemViewModel  cadastroViewModel;
    
    public DiarioCreatePostUser()
	{
        cadastroViewModel = new DiarioPostagemViewModel();
        BindingContext = cadastroViewModel;
        InitializeComponent();
	}

	
    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (await Application.Current.MainPage
                  .DisplayAlert("Confirmação", "Deseja realmente sair? As alterações não serão feitas", "Sim", "Não"))
            {
                this.Close();
            }
        }
        catch
        {

        }

    }

    private async void btnFechar_Clicked(object sender, EventArgs e)
    {
        if (cadastroViewModel.SalvarCommand != null)
        {
            this.Close();
        }
    }
    }

