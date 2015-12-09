// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (System.Convert.ToBoolean(value))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
