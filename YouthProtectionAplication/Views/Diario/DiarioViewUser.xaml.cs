using CommunityToolkit.Maui.Views;
using YouthProtectionAplication.Models;
using YouthProtectionAplication.ViewModels.Diario;

namespace YouthProtectionAplication.Views.Diario;

public partial class DiarioViewUser : ContentPage
{

    DiarioListagemViewModel viewModel;

    public DiarioViewUser()
    {
        InitializeComponent();

        viewModel = new DiarioListagemViewModel();
        BindingContext = viewModel;

  
    }
        private void Button_Clicked(object sender, EventArgs e)
        {
            this.ShowPopup(new DiarioCreatePostUser());
        }

       


   }
