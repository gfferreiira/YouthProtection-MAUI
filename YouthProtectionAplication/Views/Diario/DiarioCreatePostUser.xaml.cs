using CommunityToolkit.Maui.Views;
using YouthProtectionAplication.ViewModels.Diario;

namespace YouthProtectionAplication.Views.Diario;

public partial class DiarioCreatePostUser : Popup
{
    private DiarioPostagemViewModel  cadastroViewModel;
    
    public DiarioCreatePostUser()
	{
        cadastroViewModel = new DiarioPostagemViewModel();
        BindingContext = cadastroViewModel;
        InitializeComponent();
	}

	
    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }

}