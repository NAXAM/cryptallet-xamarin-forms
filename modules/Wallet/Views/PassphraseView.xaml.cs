using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Wallet.Views
{
    public partial class PassphraseView : ContentPage
    {
        public PassphraseView()
        {
            InitializeComponent();

            lblWarning.Text = @"There are 12 words below. Please wright it down.
This key is only way to restore your wallet 
if your phone is lost, stolen, brocken, upgraded

Plesae, be sure nobody can see it and keep it safe!";
        }
    }
}
