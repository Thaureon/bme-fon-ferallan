using bme_fon_ferallan.Clicker.CustomControls;
using bme_fon_ferallan.Clicker.SaveData;
using bme_fon_ferallan.Clicker.Tiers;

using Newtonsoft.Json;

namespace bme_fon_ferallan.Clicker
{
    public partial class ClickerPage : ContentPage
    {
        private GameSave _gameSave = new();

        private bool OverwriteFile = false;
        private const string FileName = "ClickerSave";
        private string _savePath = Path.Combine(FileSystem.AppDataDirectory, $"{FileName}.json");

        private readonly Dictionary<TierType, ITier> _tiers = new();

        public ClickerPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if(!_gameSave.Tiers.Any())
            {
                ConfigureTiers();
            }

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
                Count = 0,
                IncrementAmount = 1,
                Type = TierType.People,
                Unlocked = true,
                RequireType = TierType.None,
                SingleText = "person exists",
                MultipleText = "people exist"
            });

            AddTier(CityLabel, CityBtn, TierType.City, new TierData
            {
                Count = 0,
                IncrementAmount = 1,
                Type = TierType.City,
                Unlocked = true,
                RequireCount = 20,
                RequireType = TierType.People,
                SingleText = "city exists",
                MultipleText = "cities exist"
            });

            AddTier(CountryLabel, CountryBtn, TierType.Country, new TierData
            {
                Count = 0,
                IncrementAmount = 1,
                Type = TierType.Country,
                Unlocked = false,
                RequireCount = 10,
                RequireType = TierType.City,
                SingleText = "country exists",
                MultipleText = "countries exist"
            });

            AddTier(PlanetLabel, PlanetBtn, TierType.Planet, new TierData
            {
                Count = 0,
                IncrementAmount = 1,
                Type = TierType.Planet,
                Unlocked = false,
                RequireCount = 5,
                RequireType = TierType.Country,
                SingleText = "planet exists",
                MultipleText = "planets exist"
            });

            AddTier(SolarSystemLabel, SolarSystemBtn, TierType.SolarSystem, new TierData
            {
                Count = 0,
                IncrementAmount = 1,
                Type = TierType.SolarSystem,
                Unlocked = false,
                RequireCount = 3,
                RequireType = TierType.Planet,
                SingleText = "solar system exists",
                MultipleText = "solar systems exist"
            });
        }

        private void AddTier(CustomLabel label, CustomButton button, TierType tierType, TierData data)
        {
            var personTier = new Tier(label, button);
            personTier.InitializeData(data);
            _tiers.TryAdd(tierType, personTier);
            _gameSave.Tiers.Add(tierType, data);
        }

        private async Task LoadData()
        {
            var gameSave = await LoadDataFromFile();

            if (!OverwriteFile && gameSave != null && gameSave.Tiers.Any())
            {
                foreach (var tier in gameSave.Tiers)
                {
                    _tiers[tier.Key].LoadData(gameSave);
                }

                _gameSave = gameSave;
            }
            else
            {
                await SaveData();
                foreach (var tier in _gameSave.Tiers)
                {
                    _tiers[tier.Key].LoadData(_gameSave);
                }
            }
        }

        private async Task<GameSave?> LoadDataFromFile()
        {
            try
            {
                var jsonSave = await File.ReadAllTextAsync(_savePath);

                var gameSave = JsonConvert.DeserializeObject<GameSave>(jsonSave);
                return gameSave;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        private async Task SaveData()
        {
            foreach (var tier in _tiers)
            {
                tier.Value.SaveData(_gameSave);
            }
            await SaveDataToFile(_gameSave);
        }

        private async Task SaveDataToFile(GameSave gameSave)
        {
            var jsonSave = JsonConvert.SerializeObject(gameSave);

            await File.WriteAllTextAsync(_savePath, jsonSave);
        }

        private void OnClicked(object sender, EventArgs e)
        {
            var button = (CustomButton)sender;

            var tier = button.TierType;

            _tiers[tier].IncrementCount();

            var requiredTier = _gameSave.Tiers[tier].RequireType;
            var requiredAmount = _gameSave.Tiers[tier].RequireCount;

            if (requiredTier != TierType.None)
            {
                _tiers[requiredTier].DecrementCount(requiredAmount);
                _tiers[requiredTier].SetIncrementAmount(_tiers[tier].GetCount() + 1);
                _tiers[tier].CheckEnable(_tiers[requiredTier].GetCount());
            }

            var nextTiers = _gameSave.Tiers.Where(x => x.Value.RequireType == tier);

            foreach (var nextTier in nextTiers)
            {
                _tiers[nextTier.Value.Type].CheckEnable(_tiers[tier].GetCount());//.UpdateTier();
            }
        }
    }
}
