using Microsoft.Extensions.Logging;
using YouthProtectionAplication.Services.Usuarios;
using YouthProtectionAplication.ViewModels;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddScoped<ILoginService, LoginService>();

            builder.Services.AddSingleton<CreateAccountViewModel>();
            builder.Services.AddSingleton<CreateAccountPage>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
