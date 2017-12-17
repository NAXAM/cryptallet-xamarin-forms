using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace Wallet.Views
{
    public partial class ScanQRCodeView : ContentPage
    {
        public ScanQRCodeView()
        {
            InitializeComponent();

            scannerView.Options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false, //update later to come from settings
                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                TryHarder = true,
                AutoRotate = false,
                TryInverted = true,
                DelayBetweenContinuousScans = 2000
            };
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            scannerView.IsScanning = true;
            scannerView.AutoFocus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            scannerView.IsScanning = false;
        }

    }
}
