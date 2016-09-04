// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy
{
    using System;
    using Windows.UI;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    internal class DiscoveredBooleanToBrushConverter : IValueConverter
    {
        private Brush trueBrush = new SolidColorBrush(Colors.Black);
        private Brush falseBrush = new SolidColorBrush(Colors.Gray);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is bool && (bool)value == true
                ? this.trueBrush
                : this.falseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
