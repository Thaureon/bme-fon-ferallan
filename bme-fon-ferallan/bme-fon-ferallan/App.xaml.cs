using Plugin.MauiMtAdmob;

namespace bme_fon_ferallan
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            CrossMauiMTAdmob.Current.UserPersonalizedAds = true;
            CrossMauiMTAdmob.Current.ComplyWithFamilyPolicies = true;
            CrossMauiMTAdmob.Current.UseRestrictedDataProcessing = true;
            CrossMauiMTAdmob.Current.BannerAdsId = "ca-app-pub-3940256099942544/6300978111";

            CrossMauiMTAdmob.Current.InitialiseAndShowConsentForm();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
