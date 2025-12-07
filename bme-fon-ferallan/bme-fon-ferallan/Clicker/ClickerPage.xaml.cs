namespace bme_fon_ferallan.Clicker
{
    public partial class ClickerPage : ContentPage
    {
        private int _peopleCount;
        private int _cityCount;
        private int _countryCount;

        public ClickerPage()
        {
            InitializeComponent();
            _peopleCount = 0;
            _cityCount = 0;
            _countryCount = 0;
        }

        private void OnPersonCounterClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var buttonName = button.AutomationId;

            _peopleCount++;

            var personText = "people";

            if (_peopleCount == 1)
            {
                personText = "person";
            }

            if (_peopleCount >= 20)
            {
                CityBtn.IsEnabled = true;
            }
            var counterText = $"{_peopleCount} {personText} created";

            PersonBtn.Text = counterText;

            SemanticScreenReader.Announce(PersonBtn.Text);
        }

        private void OnCityCounterClicked(object sender, EventArgs e)
        {
            _cityCount++;

            _peopleCount = 0;

            var cityText = "City";

            if (_cityCount != 1)
            {
                cityText = "Cities";
            }

            var prestigeText = $"{_cityCount} {cityText} created";
            CityBtn.Text = prestigeText;
            CityBtn.IsEnabled = false;

            if (_cityCount >= 3)
            {
                CountryBtn.IsEnabled = true;
            }

            PersonBtn.Text = "Click to add some people.";

            SemanticScreenReader.Announce(PersonBtn.Text);
            SemanticScreenReader.Announce(CityBtn.Text);
            CountryBtn.IsVisible = true;
        }

        private void OnCountryCounterClicked(object sender, EventArgs e)
        {
            _countryCount++;

            _peopleCount = 0;
            _cityCount = 0;

            var countryEnding = "Country";

            if (_countryCount != 1)
            {
                countryEnding = "Countries";
            }

            var countryText = $"{_countryCount} {countryEnding} created";

            CityBtn.Text = countryText;
            CityBtn.IsEnabled = false;

            CountryBtn.Text = countryText;
            CountryBtn.IsEnabled = false;

            PersonBtn.Text = "Click to add some people.";
            CityBtn.Text = "Click to add a city.";

            SemanticScreenReader.Announce(PersonBtn.Text);
            SemanticScreenReader.Announce(CityBtn.Text);
            SemanticScreenReader.Announce(CountryBtn.Text);
        }
    }

}
