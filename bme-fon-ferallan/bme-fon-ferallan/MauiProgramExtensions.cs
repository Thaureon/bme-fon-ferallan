using bme_fon_ferallan.API;
using bme_fon_ferallan.Clicker;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestEase.HttpClientFactory;
using System.Reflection;

namespace bme_fon_ferallan
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
        {
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var assembly = Assembly.GetExecutingAssembly();

            var resourcePath = $"{assembly.GetName().Name.Replace('-', '_')}.Configs.appsettings.json";

            using var stream = assembly.GetManifestResourceStream(resourcePath);

            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }

            var gasPullerUrl = builder.Configuration["GasPullerApi:Url"];

            builder.Services.AddRestEaseClient<IGasPullerAPI>(gasPullerUrl);

            builder.Services.AddSingleton<ApiPageViewModel>();

            builder.Services.AddTransient<ClickerPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder;
        }
    }
}
