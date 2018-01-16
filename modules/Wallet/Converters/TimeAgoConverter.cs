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

