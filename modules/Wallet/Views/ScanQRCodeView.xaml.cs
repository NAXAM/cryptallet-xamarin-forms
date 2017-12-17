using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using Wallet.ViewModels;

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
            scannerView.OnScanResult += ScannerView_OnScanResult;
        }

        protected override void OnDisappearing()
        {
            scannerView.OnScanResult -= ScannerView_OnScanResult;
            scannerView.IsScanning = false;
            base.OnDisappearing();
        }

        void ScannerView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(delegate
            {
                scannerView.IsAnalyzing = false;
                scannerView.IsScanning = false;

                if (BindingContext is ScanQRCodeViewModel vm)
                {
                    vm.ScanCommand?.Execute(result);
                }
            });
        }
    }
}
