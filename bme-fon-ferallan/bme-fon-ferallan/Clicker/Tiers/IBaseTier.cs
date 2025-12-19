namespace bme_fon_ferallan.Clicker.Tiers
{
    internal interface IBaseTier
    {
        public void SaveData();
        public void LoadData();
        public void Enable();
        public void Disable();
        public void Show();
        public void Hide();

        public int GetCount();
        public void SetCount(int newCount);
    }
}
