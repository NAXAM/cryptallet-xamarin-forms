
using Foundation;
using UIKit;
using Wallet.Forms.Bootstraps;
using Wallet.Controls.iOS;
using Wallet.Controls.iOS.Renderers;

namespace Wallet.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            PreserveCustomRenderers();

            SetupTheme();

            global::Xamarin.Forms.Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
            Xamarin.Calabash.Start();
#endif
            LoadApplication(new WalletApplication());

            return base.FinishedLaunching(app, options);
        }

        void PreserveCustomRenderers()
        {
            CustomEntryRenderer.Preserve();
            CustomImageRenderer.Preserve();

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
        }

        void SetupTheme()
        {
            var colorPrimary = UIColor.FromRGB(0x35 / 255f, 0x74 / 255f, 0xFA / 255f);
            var colorTextPrimary = UIColor.White;

            UINavigationBar.Appearance.BackgroundColor = colorPrimary;
            UINavigationBar.Appearance.BarTintColor = colorPrimary;
            UINavigationBar.Appearance.TintColor = colorTextPrimary;

            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes()
            {
                ForegroundColor = colorTextPrimary
            };
        }
    }
}
