namespace bme_fon_ferallan.API
{
    public partial class ApiPage : ContentPage
    {
        private readonly IGasPullerAPI _gasPullerApi;

        public ApiPage(IGasPullerAPI gasPullerApi)
        {
            InitializeComponent();
            _gasPullerApi = gasPullerApi;
        }

        private void OnStateUsaPriceCheck(object sender, EventArgs e)
        {
            if (_gasPullerApi != null)
            {
                var input = StateCheckInput.Text;
                var statePriceCheck = _gasPullerApi.GetStateUsaPrice(input);
            }
        }

        private void OnCityPriceCheck(object sender, EventArgs e)
        {
            if (_gasPullerApi != null)
            {
                var statePriceCheck = _gasPullerApi.GetStateUsaPrice("TX");
            }
        }
    }
}
