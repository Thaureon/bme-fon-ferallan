namespace bme_fon_ferallan.Clicker.Tiers
{
    public class TierData
    {
        public bool Unlocked;

        public TierType Type;
        public int Count;

        public TierType RequireType;
        public int RequireCount;

        public string TierText => Count == 1 ? $"{Count} {SingleText}" : $"{Count} {MultipleText}";

        public string SingleText;
        public string MultipleText;
    }
}
