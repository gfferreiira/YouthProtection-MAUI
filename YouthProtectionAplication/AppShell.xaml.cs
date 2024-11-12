using YouthProtectionAplication.Views;
using YouthProtectionAplication.Views.Diario;
using YouthProtectionAplication.Views.Usuarios;

namespace YouthProtectionAplication
{
    public partial class AppShell : Shell
    {
        public AppShell(int userRole)
        {
            InitializeComponent();

            ConfigureShell(userRole);
        }

        private void ConfigureShell(int userRole)
        {
            // Limpa itens existentes
            Items.Clear();

            var tabBar = new TabBar();


            if (userRole == 1)
            {
                // Rotas e páginas específicas para o Utilizador
                tabBar.Items.Add(new ShellContent
                {
                    Title = "Home",
                    Icon = "home.png",
                   ContentTemplate = new DataTemplate(typeof(DiarioViewUser)) 
                });

                tabBar.Items.Add(new ShellContent
                {
                    Title = "Perfil",
                    Icon = "gg_profile.png",
                    ContentTemplate = new DataTemplate(typeof(EditarPerfilView)) 
                });
            }
            else if (userRole == 2)
            {
                tabBar.Items.Add(new ShellContent
                {
                    Title = "Perfil",
                    Icon = "gg_profile.png",
                    ContentTemplate = new DataTemplate(typeof(EditarPerfilView))
                });
            }

            Items.Add(tabBar);
        }
    }
}

