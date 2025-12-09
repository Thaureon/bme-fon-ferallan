namespace bme_fon_ferallan.Clicker
{
    public partial class ClickerPage : ContentPage
    {
        private int _peopleCount;
        private int _cityCount;
        private int _countryCount;
        private int _planetCount;
        private int _solarSystemCount;

        public ClickerPage()
        {
            InitializeComponent();
            SetupClickerPage();
        }

        private void SetupClickerPage()
        {
            _peopleCount = 0;
            _cityCount = 0;
            _countryCount = 0;
            _planetCount = 0;
            _solarSystemCount = 0;

            CityBtn.IsEnabled = false;

            CountryBtn.IsEnabled = false;
            CountryBtn.IsVisible = false;
            CountryLabel.IsVisible = false;

            PlanetBtn.IsEnabled = false;
            PlanetBtn.IsVisible = false;
            PlanetLabel.IsVisible = false;

            SolarSystemBtn.IsEnabled = false;
            SolarSystemBtn.IsVisible = false;
            SolarSystemLabel.IsVisible = false;
        }

        private void UpdateTierText(string tierName)
        {
            var announcementText = "";
            switch (tierName)
            {
                case "Person":
                    var personText = _peopleCount == 1 ? "person" : "people";
                    PersonLabel.Text = $"{_peopleCount} {personText} created";
                    announcementText = PersonLabel.Text;
                    break;
                case "City":
                    var cityText = _cityCount == 1 ? "city" : "cities";
                    CityLabel.Text = $"{_cityCount} {cityText} created";
                    announcementText = CityLabel.Text;
                    break;
                case "Country":
                    var countryText = _countryCount == 1 ? "country" : "countries";
                    CountryLabel.Text = $"{_countryCount} {countryText} created";
                    announcementText = CountryLabel.Text;
                    break;
                case "Planet":
                    var planetText = _planetCount == 1 ? "planet" : "planets";
                    PlanetLabel.Text = $"{_planetCount} {planetText} created";
                    announcementText = PlanetLabel.Text;
                    break;
                case "SolarSystem":
                    var solarSystemText = _solarSystemCount == 1 ? "solar system" : "solar systems";
                    SolarSystemLabel.Text = $"{_solarSystemCount} {solarSystemText} created";
                    announcementText = SolarSystemLabel.Text;
                    break;
            }
            SemanticScreenReader.Announce(announcementText);
        }

        private void ResetTier(string tierName)
        {
            switch (tierName)
            {
                case "Person":
                    _peopleCount = 0;
                    CityBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "City":
                    _cityCount = 0;
                    CountryBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "Country":
                    _countryCount = 0;
                    PlanetBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "Planet":
                    _planetCount = 0;
                    SolarSystemBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "SolarSystem":
                    _solarSystemCount = 0;
                    UpdateTierText(tierName);
                    break;
            }
        }

        private void UnlockTier(string tierName)
        {
            switch (tierName)
            {
                case "Person":
                case "City":
                    break;
                case "Country":
                    CountryBtn.IsVisible = true;
                    CountryLabel.IsVisible = true;
                    break;
                case "Planet":
                    PlanetBtn.IsVisible = true;
                    PlanetLabel.IsVisible = true;
                    break;
                case "SolarSystem":
                    SolarSystemBtn.IsVisible = true;
                    SolarSystemLabel.IsVisible = true;
                    break;
            }
        }

        private void OnPersonCounterClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var buttonName = button.AutomationId;

            _peopleCount++;

            if (_peopleCount >= 20)
            {
                CityBtn.IsEnabled = true;
            }

            UpdateTierText("Person");
        }

        private void OnCityCounterClicked(object sender, EventArgs e)
        {
            _cityCount++;

            if (_cityCount >= 3)
            {
                CountryBtn.IsEnabled = true;
            }

            ResetTier("Person");
            UpdateTierText("City");
            UnlockTier("Country");
        }

        private void OnCountryCounterClicked(object sender, EventArgs e)
        {
            _countryCount++;

            if (_countryCount >= 3)
            {
                PlanetBtn.IsEnabled = true;
            }

            ResetTier("Person");
            ResetTier("City");
            UpdateTierText("Country");
            UnlockTier("Planet");
        }

        private void OnPlanetCounterClicked(object sender, EventArgs e)
        {
            _planetCount++;

            if (_planetCount >= 3)
            {
                SolarSystemBtn.IsEnabled = true;
            }

            ResetTier("Person");
            ResetTier("City");
            ResetTier("Country");
            UpdateTierText("Planet");
        }

        private void OnSolarSystemCounterClicked(object sender, EventArgs e)
        {
            _solarSystemCount++;

            ResetTier("Person");
            ResetTier("City");
            ResetTier("Country");
            ResetTier("Planet");
            UpdateTierText("SolarSystem");
        }
    }
}
