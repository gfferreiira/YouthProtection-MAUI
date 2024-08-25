using YouthProtectionAplication.ViewModels;

namespace YouthProtectionAplication.Views.Usuarios;

public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage(CreateAccountViewModel createAccountViewModel)
	{
		InitializeComponent();
		BindingContext = createAccountViewModel;
	}
}