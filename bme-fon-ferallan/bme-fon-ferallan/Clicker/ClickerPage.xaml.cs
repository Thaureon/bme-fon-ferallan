using System.Text.Json;

using bme_fon_ferallan.Clicker.SaveData;
using bme_fon_ferallan.Clicker.Tiers;

namespace bme_fon_ferallan.Clicker
{
    public partial class ClickerPage : ContentPage
    {
        private GameSave _gameSave;

        private const string FileName = "ClickerSave";
        private string _savePath = Path.Combine(FileSystem.AppDataDirectory, $"{FileName}.json");

        private readonly Dictionary<TierType, ITier> _tiers = new Dictionary<TierType, ITier>();

        public ClickerPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ConfigureTiers();
            await LoadData();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await SaveData();
        }

        private void ConfigureTiers()
        {
            AddTier(PersonLabel, PersonBtn, TierType.People, new TierData
            {
                Count = 1,
                Type = TierType.People,
                Unlocked = true,
                RequireType = TierType.None
            });

            AddTier(CityLabel, CityBtn, TierType.City, new TierData
            {
                Count = 1,
                Type = TierType.City,
                Unlocked = false,
                RequireCount = 20,
                RequireType = TierType.People
            });

            AddTier(CountryLabel, CountryBtn, TierType.Country, new TierData
            {
                Count = 1,
                Type = TierType.Country,
                Unlocked = false,
                RequireCount = 10,
                RequireType = TierType.City
            });

            AddTier(PlanetLabel, PlanetBtn, TierType.Planet, new TierData
            {
                Count = 1,
                Type = TierType.Planet,
                Unlocked = false,
                RequireCount = 5,
                RequireType = TierType.Country
            });

            AddTier(SolarSystemLabel, SolarSystemBtn, TierType.SolarSystem, new TierData
            {
                Count = 1,
                Type = TierType.SolarSystem,
                Unlocked = false,
                RequireCount = 3,
                RequireType = TierType.Planet
            });
        }

        private void AddTier(Label label, Button button, TierType tierType, TierData data)
        {
            var personTier = new Tier(label, button);
            personTier.InitializeData(data);
            _tiers.Add(tierType, personTier);
        }

        private async Task LoadData()
        {
            var gameSave = await LoadDataFromFile();

            if (gameSave != null)
            {
                foreach (var tier in gameSave.Tiers)
                {
                    _tiers[tier.Key].LoadData(gameSave);
                }
            }
        }

        private async Task<GameSave?> LoadDataFromFile()
        {
            try
            {
                var jsonSave = await File.ReadAllTextAsync(_savePath);

                var gameSave = JsonSerializer.Deserialize<GameSave>(jsonSave);
                return gameSave;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        private async Task SaveData()
        {
            await SaveDataToFile(_gameSave);
        }

        private async Task SaveDataToFile(GameSave gameSave)
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
                    var personText = _tiers[TierType.People].GetCount() == 1 ? "person" : "people";
                    PersonLabel.Text = $"{_tiers[TierType.People].GetCount()} {personText} created";
                    announcementText = PersonLabel.Text;
                    break;
                case "City":
                    var cityText = _tiers[TierType.City].GetCount() == 1 ? "city" : "cities";
                    CityLabel.Text = $"{_tiers[TierType.City].GetCount()} {cityText} created";
                    announcementText = CityLabel.Text;
                    break;
                case "Country":
                    var countryText = _tiers[TierType.Country].GetCount() == 1 ? "country" : "countries";
                    CountryLabel.Text = $"{_tiers[TierType.Country].GetCount()} {countryText} created";
                    announcementText = CountryLabel.Text;
                    break;
                case "Planet":
                    var planetText = _tiers[TierType.Planet].GetCount() == 1 ? "planet" : "planets";
                    PlanetLabel.Text = $"{_tiers[TierType.Planet].GetCount()} {planetText} created";
                    announcementText = PlanetLabel.Text;
                    break;
                case "SolarSystem":
                    var solarSystemText = _tiers[TierType.SolarSystem].GetCount() == 1 ? "solar system" : "solar systems";
                    SolarSystemLabel.Text = $"{_tiers[TierType.SolarSystem].GetCount()} {solarSystemText} created";
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
                    _tiers[TierType.People].SetCount(0);
                    CityBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "City":
                    _tiers[TierType.City].SetCount(0);
                    CountryBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "Country":
                    _tiers[TierType.Country].SetCount(0);
                    PlanetBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "Planet":
                    _tiers[TierType.Planet].SetCount(0);
                    SolarSystemBtn.IsEnabled = false;
                    UpdateTierText(tierName);
                    break;
                case "SolarSystem":
                    _tiers[TierType.SolarSystem].SetCount(0);
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

            var tier = TierType.People;
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
            var tier = TierType.City;
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
            var tier = TierType.Country;
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
            var tier = TierType.Planet;
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
            var tier = TierType.SolarSystem;
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
