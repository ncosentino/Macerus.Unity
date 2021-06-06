#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Data;
#endif

using System;
using System.Globalization;

namespace Assets.Scripts.Plugins.Features.NewHud.Noesis
{
    public sealed class GreaterThanZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = System.Convert.ToDouble(value, culture);
            return converted > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
