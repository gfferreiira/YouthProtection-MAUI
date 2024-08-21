using YouthProtectionAplication.Views;

namespace YouthProtectionAplication
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateAccountPage), typeof(CreateAccountPage));
        }
    }
}
