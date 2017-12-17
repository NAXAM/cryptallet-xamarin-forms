using System;
using Xamarin.Forms;
namespace Wallet.Controls
{
    public static class ImageProperties
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.CreateAttached(
            "Color",
            typeof(Color),
            typeof(ImageProperties),
            Color.Transparent,
            BindingMode.OneWay
        );
        public static Color GetColor(BindableObject obj)
        {
            return (Color)obj.GetValue(ColorProperty);
        }
        public static void SetColor(BindableObject obj, Color value)
        {
            obj.SetValue(ColorProperty, value);
        }
    }
}
