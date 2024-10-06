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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = viewModel.ObterPostagens();
    }
}