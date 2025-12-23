using bme_fon_ferallan.Clicker.SaveData;

namespace bme_fon_ferallan.Clicker.Tiers
{
    internal interface ITier
    {
        public void InitializeData(TierData tierData);
        public void LoadData(GameSave loadedData);
        public void SaveData(GameSave saveData);
        public void Enable();
        public void Disable();
        public void Show();
        public void Hide();

        public int GetCount();
        public void SetCount(int newCount);
        public void IncrementCount(int increment);
        public void CheckEnable(int requirementCount);
        public void CheckUnlock();
        public void SetText();
    }
}
