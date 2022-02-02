using System.Windows.Input;
using static MacroscopeTestTask.ViewModel.RelayCommands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace MacroscopeTestTask.ViewModel
{
    public class DownloaderViewmodel : NotifyPropertyChangedBase
    {
        private byte zIndex = 0;
        private double progress = 0;
        private BitmapImage bigImage = null;
        public ObservableCollection<DownloadTask> DownloadTasks { get; set; } = new ObservableCollection<DownloadTask>(); //коллекция со всем необходимым для каждого "набора"
        public byte ZIndex
        {
            get => this.zIndex;
            set => this.SetField(ref this.zIndex, value);
        }
        public double Progress
        {
            get => this.progress;
            set => this.SetField(ref this.progress, value);
        }
        public BitmapImage BigImage
        {
            get => this.bigImage;
            set => this.SetField(ref this.bigImage, value);
        }
        public void removeBigImage()
        {
            this.ZIndex = 0;
            this.BigImage = null;
        }

        public ICommand RemoveBigImage
        {
            get
            {
                return new RelayCommand(param => this.removeBigImage());
            }
        }
        public ICommand StartAll
        {
            get
            {
                return new RelayCommand(param => this.StartAllDownloads());
            }
        }

        public ICommand CancelAll
        {
            get
            {
                return new RelayCommand(param => this.CancelAllDownloads());
            }
        }

        public ICommand AddElement
        {
            get
            {
                return new RelayCommand(param => this.AddDownloadTask());
            }
        }

        public ICommand RemoveElement
        {
            get
            {
                return new RelayCommand(param => this.RemoveDownloadTask());
            }
        }
        /// <summary>
        /// Добавляет в конец объект класса <see cref="DownloadTask"/>
        /// </summary>
        private void AddDownloadTask()
        {
            DownloadTask downloadTask = new DownloadTask();
            downloadTask.PropertyChanged += DownloadTask_PropertyChanged;
            this.DownloadTasks.Add(downloadTask);         
        }
        /// <summary>
        /// Удаляет последний добавленный объект класса <see cref="DownloadTask"/>
        /// </summary>
        private void RemoveDownloadTask()
        {
            if (this.DownloadTasks.Count > 0)
            {
                DownloadTask downloadTask = this.DownloadTasks.Last();
                downloadTask.PropertyChanged -= DownloadTask_PropertyChanged;
                this.DownloadTasks.Remove(downloadTask);
            }       
        }
        /// <summary>
        /// Вызывает <see cref="DownloadTask.Start"/> для всех объектов <see cref="DownloadTask"/>
        /// </summary>
        private void StartAllDownloads()
        {
            foreach (DownloadTask downloadTask in this.DownloadTasks)
            {
                downloadTask.StartDownloadAsync();
            }
        }
        /// <summary>
        /// Вызывает <see cref="DownloadTask.Cancel"/> для всех объектов <see cref="DownloadTask"/>
        /// </summary>
        private void CancelAllDownloads()
        {
            foreach (DownloadTask downloadTask in this.DownloadTasks)
            {
                downloadTask.CancelDownload();
            }
        }

        /// <summary>
        /// gets or sets <see cref="ObservableCollection{T}"/> of <see cref="DownloadTask"/>
        /// </summary>

        public DownloaderViewmodel()
        {
            for (int i = 0; i < 3; i++)
            {
                DownloadTask downloadTask = new DownloadTask();
                downloadTask.PropertyChanged += DownloadTask_PropertyChanged;
                DownloadTasks.Add(downloadTask);
            }
        }
        /// <summary>
        /// Вызывается когда <see cref="DownloadTask"/> меняет своё значение.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void DownloadTask_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DownloadTask.Progress))
            {
                double sum = 0;
                int amount = 0;

                foreach (DownloadTask downloadTask in this.DownloadTasks)
                {
                    sum += downloadTask.Progress;
                    if (downloadTask.DownloadStatus == DownloadStatus.Running || downloadTask.DownloadStatus == DownloadStatus.Successfull)
                    {
                        amount++;
                    }
                }
                this.Progress = sum / amount;
            }

            if(e.PropertyName == nameof(DownloadTask.IsBigImageRequested))
            {
                this.BigImage = (BitmapImage)sender.GetType().GetProperty("Image").GetValue(sender, null);
                this.ZIndex = 2;
            }
        }
    }
}
