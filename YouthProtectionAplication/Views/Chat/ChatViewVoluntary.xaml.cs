using YouthProtectionAplication.ViewModels.Voluntario;

namespace YouthProtectionAplication.Views.Chat;

public partial class ChatViewVoluntary : ContentPage
{
	ChatVoluntaryViewModel chatViewModel;
	public ChatViewVoluntary(Models.Postagem postagem, long userId)
	{
        chatViewModel = new ChatVoluntaryViewModel(postagem, userId);
        BindingContext = chatViewModel;
        InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Application.Current.MainPage = new AppShell(Preferences.Get("UsuarioRole", 0));
    }
}