using System;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Xamarin.Forms;
using Wallet.Controls.Droid.Renderers;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace Wallet.Controls.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public static void Preserve() { }

        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            UpdateBackground();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Entry.BackgroundColorProperty.PropertyName)
            {
                UpdateBackground();
            }
        }

        void UpdateBackground()
        {
            if (Control == null || Element == null) return;

            Control.SetBackgroundResource(Resource.Drawable.bg_edittext);
            int padding = (int)Context.ToPixels(16);
            Control.SetPadding(padding, padding, padding, padding);
        }
    }
}
