using bme_fon_ferallan.Clicker.Tiers;

namespace bme_fon_ferallan.Clicker
{
    public partial class ClickerPage : ContentPage
    {
        private int _peopleCount;
        private int _cityCount;
        private int _countryCount;
        private int _planetCount;
        private int _solarSystemCount;

        private Dictionary<Tier, ITier> _tiers = new Dictionary<Tier, ITier>();

        public ClickerPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SetupClickerPage();
            LoadData();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            SaveData();
        }

        private void SetupClickerPage()
        {
            _tiers.Add(Tier.People, new BaseTier(PersonLabel, PersonBtn, TierNameConstants.PeopleTier, Tier.People));
            _tiers.Add(Tier.City, new BaseTier(CityLabel, CityBtn, TierNameConstants.CityTier, Tier.City));
            _tiers.Add(Tier.Country, new BaseTier(CountryLabel, CountryBtn, TierNameConstants.CountryTier, Tier.Country));
            _tiers.Add(Tier.Planet, new BaseTier(PlanetLabel, PlanetBtn, TierNameConstants.PlanetTier, Tier.Planet));
            _tiers.Add(Tier.SolarSystem, new BaseTier(SolarSystemLabel, SolarSystemBtn, TierNameConstants.SolarSystemTier, Tier.SolarSystem));
        }

        private void LoadData()
        {
            foreach (var tier in _tiers)
            {
                tier.Value.LoadData();
            }
        }

        private void SaveData()
        {
            foreach (var tier in _tiers)
            {
                tier.Value.SaveData();
            }
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
