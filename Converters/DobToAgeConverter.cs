using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace EmployeeDirectory.Caliburn.Converters
{
    public class DobToAgeConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value is DateTime date ? DateTime.Today.Year - date.Year : (object)0;
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
