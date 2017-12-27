using System;
using System.Globalization;
using Xamarin.Forms;

namespace Wallet.Converters
{
    public class TimeAgoConverter : IValueConverter
    {
        static DateTime baseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset dtOffset;

            if (value is DateTime dateTime)
            {
                dtOffset = dateTime;
            }
            else if (value is long unixTimestamp)
            {
                dtOffset = baseTime.AddSeconds(unixTimestamp);
            }
            else if (value is DateTimeOffset dateTimeOffset)
            {
                dtOffset = dateTimeOffset;
            }
            else
            {
                return "N/A";
            }

            DateTimeOffset now = DateTime.Now;
            var timespan = now.Subtract(dtOffset);
            if (timespan < TimeSpan.FromSeconds(60))
            {
                return "Just now";
            }
            if (timespan < TimeSpan.FromMinutes(5))
            {
                return "Few minutes ago";
            }

            if (timespan < TimeSpan.FromMinutes(60))
            {
                return $"{timespan.Minutes} minutes ago";
            }

            if (timespan < TimeSpan.FromHours(24))
            {
                return $"{timespan.Hours} hours ago";
            }

            if (timespan < TimeSpan.FromDays(2))
            {
                return $"Yesterday";
            }

            if (timespan < TimeSpan.FromDays(7))
            {
                return $"{timespan.Days} days ago";
            }

            return dtOffset.ToString("MMM dd, yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

