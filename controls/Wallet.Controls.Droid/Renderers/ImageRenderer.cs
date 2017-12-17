using System;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Xamarin.Forms;
using Wallet.Controls.Droid.Renderers;

[assembly: ExportRenderer(typeof(Image), typeof(CustomImageRenderer))]
namespace Wallet.Controls.Droid.Renderers
{
    public class CustomImageRenderer : ImageRenderer
    {
        public static void Preserve() {}

        public CustomImageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
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

            Control.SetColorFilter(color.ToAndroid());
        }
    }
}
