using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace EmployeeDirectory.Caliburn.Converters
{
    public class SalaryRangeToColorConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (decimal.TryParse(value.ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var range))
                {
                    if (range < 20000)
                        return new SolidColorBrush(Colors.Red);
                    else if (range < 40000)
                        return new SolidColorBrush(Colors.Blue);
                    else if (range > 40000)
                        return new SolidColorBrush(Colors.ForestGreen);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
