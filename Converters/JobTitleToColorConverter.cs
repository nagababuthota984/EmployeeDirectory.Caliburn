using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace EmployeeDirectory.Caliburn.Converters
{
    public class JobTitleToColorConverter : IValueConverter
    {
        public static Random random = new();
        public static Dictionary<string, SolidColorBrush> colors = new();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            KeyValuePair<string, SolidColorBrush> job = colors.FirstOrDefault(clrs => clrs.Key.Equals(value.ToString(), StringComparison.CurrentCultureIgnoreCase));
            if (job.Key == null)
            {
                colors.Add(value.ToString(), new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 200), (byte)random.Next(1, 200), (byte)random.Next(1, 200))));
            }
            return colors[value.ToString()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
