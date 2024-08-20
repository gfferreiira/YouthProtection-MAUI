using YouthProtectionAplication.ViewModels;

namespace YouthProtectionAplication.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}