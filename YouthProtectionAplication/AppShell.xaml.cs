using YouthProtectionAplication.Views;
using YouthProtectionAplication.Views.Usuarios;

namespace YouthProtectionAplication
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateAccountPage), typeof(CreateAccountPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
