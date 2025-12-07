using bme_fon_ferallan.Clicker;

namespace bme_fon_ferallan
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClickerPage());
        }
    }
}
