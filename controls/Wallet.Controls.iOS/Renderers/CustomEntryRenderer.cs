using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using Xamarin.Forms;
using Wallet.Controls.iOS.Renderers;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace Wallet.Controls.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public static void Preserve() {
            var now = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            Control.LeftView = new UIView(new CGRect(0, 0, 16, 16));
            Control.RightView = new UIView(new CGRect(0, 0, 16, 16));
        }
    }
}
