using Android.App;
using Android.Content.PM;
using Android.Content;

namespace Wallet.Droid
{
    [Activity(NoHistory = true, Label = "@string/app_name", Icon = "@mipmap/ic_launcher", Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}
