using MacroscopeTestTask.ViewModel;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MacroscopeTestTask.Converters
{
    /// <summary>
    /// Преобразует <see cref="DownloadStatus"/> в <see cref="Brushes"/>
    /// </summary>
    public class DownloadStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case DownloadStatus.None:
                    {
                        return Brushes.Gray;
                    }
                case DownloadStatus.Aborted:
                    {
                        return Brushes.Red;
                    }
                case DownloadStatus.Running:
                    {
                        return Brushes.YellowGreen;
                    }
                case DownloadStatus.Successfull:
                    {
                        return Brushes.Green;
                    }
                default:
                    {
                        return Brushes.White;
                    }
            }         
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
