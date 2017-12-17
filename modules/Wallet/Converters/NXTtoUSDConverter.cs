using System;
using System.Globalization;
using Xamarin.Forms;
namespace Wallet.Converters
{
    public class NXTtoUSDConverter : IValueConverter
    {
        static readonly decimal TOKEN_IN_DOLLAR = 0.05m;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var numericValue = System.Convert.ToDecimal(value);

                return string.Format("\u2248 {0:#,##0.00} USD", numericValue * TOKEN_IN_DOLLAR);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
