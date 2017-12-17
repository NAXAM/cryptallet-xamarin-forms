using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using Wallet.Controls.iOS.Renderers;

[assembly: ExportRenderer(typeof(Image), typeof(CustomImageRenderer))]
namespace Wallet.Controls.iOS.Renderers
{
    public class CustomImageRenderer : ImageRenderer
    {
        public static void Preserve()
        {
            var now = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
        {
            base.OnElementChanged(e);

            Colorize();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ImageProperties.ColorProperty.PropertyName)
            {
                Colorize();
            }
        }

        void Colorize()
        {
            if (Control == null || Element == null) return;

            var color = ImageProperties.GetColor(Element);

            if (Color.Transparent == color) return;

            Control.Image = Control.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Control.TintColor = color.ToUIColor();
        }
    }
}
