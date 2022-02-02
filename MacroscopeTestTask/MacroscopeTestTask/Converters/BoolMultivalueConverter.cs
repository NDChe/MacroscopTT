using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MacroscopeTestTask.Converters
{
    /// <summary>
    /// Возвращает <see cref="true"/> если все входящие элементы типа <see cref="bool"/> имеют значение <see cref="true"/>, иначе <see cref="false"/>.
    /// </summary>
    public class BoolMultivalueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.All(b => (bool)b);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
