using YouthProtectionAplication.ViewModels;

namespace YouthProtectionAplication.Views.Usuarios;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}