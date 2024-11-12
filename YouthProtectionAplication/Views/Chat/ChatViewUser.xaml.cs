using CommunityToolkit.Maui.Views;
using YouthProtectionAplication.Services;
using YouthProtectionAplication.Services.Chat;
using YouthProtectionAplication.ViewModels.Chat;

namespace YouthProtectionAplication.Views.Chat;

public partial class ChatViewUser : ContentPage
{
	ChatUserViewModel chatViewModel;
	public ChatViewUser(Models.Postagem postagem, long userId)
	{
        chatViewModel = new ChatUserViewModel(postagem, userId);
		BindingContext = chatViewModel;
        InitializeComponent();
    }


    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
      Application.Current.MainPage = new AppShell(Preferences.Get("UsuarioRole", 0));
    }
}