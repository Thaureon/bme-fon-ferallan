using Android.App;
using Android.Gms.Ads;
using Android.Runtime;

namespace bme_fon_ferallan.Droid
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override void OnCreate()
        {
            base.OnCreate();
            //MobileAds.
            MobileAds.Initialize(this);
        }
    }
}
