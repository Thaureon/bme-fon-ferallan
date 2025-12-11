using bme_fon_ferallan.API;
using Microsoft.Extensions.Logging;
using RestEase.HttpClientFactory;

namespace bme_fon_ferallan
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
        {
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddRestEaseClient<IGasPullerAPI>("https://api.collectapi.com/gasPrice/");

            builder.Services.AddSingleton<ApiPageViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder;
        }
    }
}
