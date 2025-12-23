using bme_fon_ferallan.Clicker.Tiers;

namespace bme_fon_ferallan.Clicker.CustomControls
{
    public class CustomButton : Button
    {
        public static readonly BindableProperty CustomDataProperty =
            BindableProperty.Create(nameof(TierType), typeof(TierType), typeof(CustomButton), defaultValue: TierType.None);

        public TierType TierType
        {
            get => (TierType)GetValue(CustomDataProperty);
            set => SetValue(CustomDataProperty, value);
        }
    }
}
