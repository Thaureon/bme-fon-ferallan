using Plugin.MauiMtAdmob.Controls;

namespace bme_fon_ferallan.Ads
{
    public partial class AdsPage : ContentPage
    {

        public AdsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MTAdView ads = new MTAdView();
        }
    }
}
