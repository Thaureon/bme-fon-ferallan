using Microsoft.Extensions.Configuration;

namespace bme_fon_ferallan.API
{
    public partial class ApiPage : ContentPage
    {
        private readonly ApiPageViewModel _bindingContext;

        private readonly IConfiguration _config;

        public ApiPage(ApiPageViewModel viewModel, IConfiguration config)
        {
            InitializeComponent();
            _bindingContext = viewModel;
            _config = config;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var token = _config["GasPullerApi:ApiKey"];

            if (token != null)
            {
                await SecureStorage.Default.SetAsync("auth_token", token);
            }
            // else should have some error handling normally.
        }

        private async void OnStateUsaPriceCheck(object sender, EventArgs e)
        {
            var input = StateCheckInput.Text;
            var statePriceCheck = await _bindingContext.GetStateUsaPrice(input);
        }

        private async void OnCityPriceCheck(object sender, EventArgs e)
        {
            var statePriceCheck = await _bindingContext.GetFromCity("dallas", "gasoline");
        }
    }
}
