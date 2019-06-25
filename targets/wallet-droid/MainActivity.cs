
using Android.App;
using Android.Content.PM;
using Android.OS;
using Wallet.Forms.Bootstraps;
using Plugin.SecureStorage;
using Wallet.Controls.Droid.Renderers;
using Acr.UserDialogs;

namespace Wallet.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/ic_launcher", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            PreserveCustomRenderers();

            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new WalletApplication());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void PreserveCustomRenderers()
        {
            CustomEntryRenderer.Preserve();
            CustomImageRenderer.Preserve();

            ZXing.Net.Mobile.Forms.Android.Platform.Init();
        }
    }
}
