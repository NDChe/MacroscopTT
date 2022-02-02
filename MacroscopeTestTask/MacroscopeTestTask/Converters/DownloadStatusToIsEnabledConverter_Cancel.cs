using MacroscopeTestTask.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MacroscopeTestTask.Converters
{
    /// <summary>
    /// Преобразует <see cref="DownloadStatus.Running"/> в <see cref="true"/>.
    /// </summary>
    public class DownloadStatusToIsEnabledConverter_Cancel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case DownloadStatus.Running:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
