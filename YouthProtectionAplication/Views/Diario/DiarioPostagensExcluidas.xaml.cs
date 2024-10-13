using YouthProtectionAplication.ViewModels.Diario;

namespace YouthProtectionAplication.Views.Diario;

public partial class DiarioPostagensExcluidas : ContentPage
{
    DiarioListagemViewModel viewModel;

    public DiarioPostagensExcluidas()
	{
		InitializeComponent();
        viewModel = new DiarioListagemViewModel();
        BindingContext = viewModel;
    }
}