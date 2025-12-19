namespace bme_fon_ferallan.Clicker.Tiers
{
    public class BaseTier : IBaseTier
    {
        private Label _label;
        private Button _button;
        private string _tierName;
        private Tier _currentTier;

        private int _count;

        public BaseTier(Label label, Button button, string tierName, Tier currentTier)
        {
            _label = label;
            _button = button;
            _tierName = tierName;
            _currentTier = currentTier;
        }

        public void SaveData()
        {
            Preferences.Set(_tierName, _count);
        }

        public void LoadData()
        {
            _count = Preferences.Get(_tierName, 0);
            var tier = (Tier)Preferences.Get(TierNameConstants.HighestTier, (int)Tier.None);
            if (tier >= _currentTier - 1)
            {
                Enable();
                Show();
            }
            else
            {
                Disable();
                Hide();
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
            return _count;
        }

        public void SetCount(int newCount)
        {
            _count = newCount;
        }
    }
}
