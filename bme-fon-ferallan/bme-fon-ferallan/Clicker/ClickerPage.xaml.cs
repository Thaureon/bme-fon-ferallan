using System.Text.Json;

using bme_fon_ferallan.Clicker.SaveData;
using bme_fon_ferallan.Clicker.Tiers;

namespace bme_fon_ferallan.Clicker
{
    public partial class ClickerPage : ContentPage
    {
        private GameSaveInfo _gameSaveInfo;

        private const string FileName = "ClickerSave";
        private string _savePath = Path.Combine(FileSystem.AppDataDirectory, $"{FileName}.json");

        private readonly Dictionary<Tier, IBaseTier> _tiers = new Dictionary<Tier, IBaseTier>();

        public ClickerPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SetupClickerPage();
            await LoadData();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await SaveData();
        }

        private void SetupClickerPage()
        {
            _tiers.Add(Tier.People, new BaseTier(PersonLabel, PersonBtn, TierNameConstants.PeopleTier, Tier.People));
            _tiers.Add(Tier.City, new BaseTier(CityLabel, CityBtn, TierNameConstants.CityTier, Tier.City));
            _tiers.Add(Tier.Country, new BaseTier(CountryLabel, CountryBtn, TierNameConstants.CountryTier, Tier.Country));
            _tiers.Add(Tier.Planet, new BaseTier(PlanetLabel, PlanetBtn, TierNameConstants.PlanetTier, Tier.Planet));
            _tiers.Add(Tier.SolarSystem, new BaseTier(SolarSystemLabel, SolarSystemBtn, TierNameConstants.SolarSystemTier, Tier.SolarSystem));
        }

        private async Task LoadData()
        {
            _gameSaveInfo = await LoadDataFromFile();
        }

        private async Task<GameSaveInfo> LoadDataFromFile()
        {
            var jsonSave = await File.ReadAllTextAsync(_savePath);

            var gameSave = JsonSerializer.Deserialize<GameSaveInfo>(jsonSave);

            return gameSave ?? new GameSaveInfo();
        }

        private async Task SaveData()
        {
            foreach (var tier in _tiers)
            {
                tier.Value.SaveData();
            }

            await SaveDataToFile(_gameSaveInfo);
        }

        private async Task SaveDataToFile(GameSaveInfo gameSave)
        {
            var jsonSave = JsonSerializer.Serialize(gameSave);

            await File.WriteAllTextAsync(_savePath, jsonSave);
        }

        private void UpdateTierText(string tierName)
        {
            var announcementText = "";
            switch (tierName)
            {
                case "Person":
                    var personText = _tiers[Tier.People].GetCount() == 1 ? "person" : "people";
                    PersonLabel.Text = $"{_tiers[Tier.People].GetCount()} {personText} created";
                    announcementText = PersonLabel.Text;
                    break;
                case "City":
                    var cityText = _tiers[Tier.City].GetCount() == 1 ? "city" : "cities";
                    CityLabel.Text = $"{_tiers[Tier.City].GetCount()} {cityText} created";
                    announcementText = CityLabel.Text;
                    break;
                case "Country":
                    var countryText = _tiers[Tier.Country].GetCount() == 1 ? "country" : "countries";
                    CountryLabel.Text = $"{_tiers[Tier.Country].GetCount()} {countryText} created";
                    announcementText = CountryLabel.Text;
                    break;
                case "Planet":
                    var planetText = _tiers[Tier.Planet].GetCount() == 1 ? "planet" : "planets";
                    PlanetLabel.Text = $"{_tiers[Tier.Planet].GetCount()} {planetText} created";
                    announcementText = PlanetLabel.Text;
                    break;
                case "SolarSystem":
                    var solarSystemText = _tiers[Tier.SolarSystem].GetCount() == 1 ? "solar system" : "solar systems";
                    SolarSystemLabel.Text = $"{_tiers[Tier.SolarSystem].GetCount()} {solarSystemText} created";
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
                    _tiers[Tier.People].SetCount(0);
                    CityBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "City":
                    _tiers[Tier.City].SetCount(0);
                    CountryBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "Country":
                    _tiers[Tier.Country].SetCount(0);
                    PlanetBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "Planet":
                    _tiers[Tier.Planet].SetCount(0);
                    SolarSystemBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "SolarSystem":
                    _tiers[Tier.SolarSystem].SetCount(0);
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

            var tier = Tier.People;
            var newCount = _tiers[tier].GetCount() + 1;

            _tiers[tier].SetCount(newCount);

            if (newCount >= 20)
            {
                CityBtn.IsEnabled = true;
            }

            UpdateTierText("Person");
        }

        private void OnCityCounterClicked(object sender, EventArgs e)
        {
            var tier = Tier.City;
            var newCount = _tiers[tier].GetCount() + 1;

            _tiers[tier].SetCount(newCount);

            if (newCount >= 3)
            {
                CountryBtn.IsEnabled = true;
            }

            ResetTier("Person");
            UpdateTierText("City");
            UnlockTier("Country");
        }

        private void OnCountryCounterClicked(object sender, EventArgs e)
        {
            var tier = Tier.Country;
            var newCount = _tiers[tier].GetCount() + 1;

            _tiers[tier].SetCount(newCount);

            if (newCount >= 3)
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
            var tier = Tier.Planet;
            var newCount = _tiers[tier].GetCount() + 1;

            _tiers[tier].SetCount(newCount);

            if (newCount >= 3)
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
            var tier = Tier.SolarSystem;
            var newCount = _tiers[tier].GetCount() + 1;

            _tiers[tier].SetCount(newCount);

            ResetTier("Person");
            ResetTier("City");
            ResetTier("Country");
            ResetTier("Planet");
            UpdateTierText("SolarSystem");
        }
    }
}
