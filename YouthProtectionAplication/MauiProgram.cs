using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using YouthProtectionAplication.Services.Usuarios;
using YouthProtectionAplication.ViewModels;
using YouthProtectionAplication.ViewModels.Usuarios;
using YouthProtectionAplication.Views.Usuarios;

namespace YouthProtectionAplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
           
            builder.Services.AddSingleton<UsuarioViewModel>();
            builder.Services.AddSingleton<CreateAccountPage>();

      
            
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
