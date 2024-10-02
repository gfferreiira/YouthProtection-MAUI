using YouthProtectionAplication.Views.Usuarios;

namespace YouthProtectionAplication
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginView());

           
        }
    }
}
