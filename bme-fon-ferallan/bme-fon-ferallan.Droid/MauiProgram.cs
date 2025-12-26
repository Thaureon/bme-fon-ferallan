using Plugin.MauiMtAdmob;

namespace bme_fon_ferallan.Droid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseSharedMauiApp()
                .UseMauiMTAdmob();

            return builder.Build();
        }
    }
}
