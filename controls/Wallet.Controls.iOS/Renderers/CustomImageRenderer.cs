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
