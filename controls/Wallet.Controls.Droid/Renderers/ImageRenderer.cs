/*
 * Copyright 2018 NAXAM CO.,LTD.
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */ 
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
