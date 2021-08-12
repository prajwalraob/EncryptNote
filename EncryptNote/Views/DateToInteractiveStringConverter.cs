using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EncryptNote.Views
{
    class DateToInteractiveStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime timeVal = (DateTime)value;
            TimeSpan timeSpan = DateTime.Now.Subtract(timeVal);
            if (timeSpan.Days < 1) return "Updated less than a day ago";
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
