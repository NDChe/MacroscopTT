using MacroscopeTestTask.ViewModel;
using System.Windows;

namespace MacroscopeTestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DownloaderViewmodel vm = new DownloaderViewmodel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }


    }
}
