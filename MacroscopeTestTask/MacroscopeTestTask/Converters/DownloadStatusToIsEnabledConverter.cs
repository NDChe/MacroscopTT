using MacroscopeTestTask.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MacroscopeTestTask.Converters
{
    /// <summary>
    /// Преобразует <see cref="DownloadStatus.Running"/> в <see cref="false"/>.
    /// </summary>
    public class DownloadStatusToIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case DownloadStatus.Running:
                    {
                        return false;
                    }
                default:
                    {
                        return true;
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
