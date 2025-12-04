namespace bme_fon_ferallan.Clicker
{
    public partial class ClickerPage : ContentPage
    {
        private int _count;
        private int _prestigeCount;

        public ClickerPage()
        {
            InitializeComponent();
            _count = 0;
            _prestigeCount = 0;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            _count++;

            var counterText = $"Clicked {_count} time";

            if (_count != 1)
            {
                counterText += "s";
            }

            if (_count >= 20)
            {
                PrestigeBtn.IsVisible = true;
                PrestigeBtn.IsEnabled = true;
            }

            CounterBtn.Text = counterText;

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void OnPrestigeClicked(object sender, EventArgs e)
        {
            _prestigeCount++;
            _count = 0;

            var prestigeText = $"Prestiged {_prestigeCount} time";

            if (_prestigeCount != 1)
            {
                prestigeText += "s";
            }

            PrestigeBtn.Text = prestigeText;
            PrestigeBtn.IsEnabled = false;
            CounterBtn.Text = "Prestiged, click reset.";

            SemanticScreenReader.Announce(PrestigeBtn.Text);
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
