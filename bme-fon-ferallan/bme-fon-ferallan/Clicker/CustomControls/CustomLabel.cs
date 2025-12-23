using bme_fon_ferallan.Clicker.Tiers;

namespace bme_fon_ferallan.Clicker.CustomControls
{
    public class CustomLabel : Label
    {
        public static readonly BindableProperty CustomDataProperty =
            BindableProperty.Create(nameof(TierType), typeof(TierType), typeof(CustomLabel), defaultValue: TierType.None);

        public TierType TierType
        {
            get => (TierType)GetValue(CustomDataProperty);
            set => SetValue(CustomDataProperty, value);
        }
    }
}
