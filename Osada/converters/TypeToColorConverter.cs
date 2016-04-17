using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;


namespace converters
{
    class TypeToColorConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            var a = Color.FromArgb(0, 0, 0, 0);
            var b = new SolidColorBrush(a);
            return b;
            new SolidColorBrush(Color.FromArgb(255, 10, 210, 10));
        }

        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
