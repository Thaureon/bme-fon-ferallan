namespace bme_fon_ferallan.iOS
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseSharedMauiApp();

            return builder.Build();
        }
    }
}
