namespace bme_fon_ferallan.API
{
    public partial class ApiPage : ContentPage
    {
        private readonly ApiPageViewModel _bindingContext;

        public ApiPage(ApiPageViewModel viewModel)
        {
            InitializeComponent();
            _bindingContext = viewModel;
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
