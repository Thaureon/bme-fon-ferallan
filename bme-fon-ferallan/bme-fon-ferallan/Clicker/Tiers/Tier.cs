using bme_fon_ferallan.Clicker.SaveData;

namespace bme_fon_ferallan.Clicker.Tiers
{
    public class Tier : ITier
    {
        private TierData _data;

        private Label _label;
        private Button _button;

        public Tier(Label label, Button button)
        {
            _label = label;
            _button = button;
        }

        public void InitializeData(TierData tierData)
        {
            _data = tierData;
        }

        public void LoadData(GameSave loadedData)
        {
            var currentTier = _data.Type;

            _data = loadedData.Tiers[currentTier];

            Hide();
            Disable();

            if (_data.Unlocked)
            {
                Show();

                if (_data.RequireType == TierType.None || loadedData.Tiers[currentTier - 1].Count >= _data.RequireCount)
                {
                    Enable();
                }
            }
        }

        public void Enable()
        {
            _button.IsEnabled = true;
        }

        public void Disable()
        {
            _button.IsEnabled = false;
        }

        public void Show()
        {
            _label.IsVisible = true;
            _button.IsVisible = true;
        }

        public void Hide()
        {
            _label.IsVisible = false;
            _button.IsVisible = false;
        }

        public int GetCount()
        {
            return _data.Count;
        }

        public void SetCount(int newCount)
        {
            _data.Count = newCount;
        }

        public void IncrementCount(int increment)
        {
            _data.Count += increment;
        }
    }
}
