using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static MacroscopeTestTask.ViewModel.RelayCommands;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace MacroscopeTestTask.ViewModel
{
    public class DownloadTask : NotifyPropertyChangedBase
    {
        private string url = "";
        private BitmapImage image;
        private double progress = 0;
        private DownloadStatus downloadStatus = DownloadStatus.None;
        private WebClient webClient;
        private bool isUrlValid;
        private bool isBigImageRequested = false;

        public DownloadTask()
        {

        }

        public ICommand ShowBigImage
        {
            get
            {
                return new RelayCommand(param => SetBigImage());
            }
        }

        private void SetBigImage()
        {
            this.IsBigImageRequested = !IsBigImageRequested;
            Debug.WriteLine("Doubleclicked");
        }

        public DownloadStatus DownloadStatus
        {
            get => this.downloadStatus;
            set => this.SetField(ref this.downloadStatus, value);
        }
        public bool IsBigImageRequested
        {
            get => this.isBigImageRequested;
            set => this.SetField(ref this.isBigImageRequested, value); 
        }

        public string Url
        {
            get => this.url;
            set
            {
                try
                {
                    new Uri(value);
                    this.IsUrlValid = true;
                }
                catch (Exception)
                {
                    this.IsUrlValid = false;
                };
                this.SetField(ref this.url, value);
            }
        }


        public bool IsUrlValid
        {
            get => this.isUrlValid;
            set => this.SetField(ref this.isUrlValid, value);
        }

        public BitmapImage Image
        {
            get => this.image;
            set => this.SetField(ref this.image, value);
        }

        public ICommand Start
        {
            get
            {
                return new RelayCommand(param => new Task(() => this.StartDownloadAsync()).Start());
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(param => this.CancelDownload());
            }
        }



        public double Progress 
        {
            get => this.progress;
            set => this.SetField(ref this.progress, value);
        }
        /// <summary>
        /// Отменяет загрузку <see cref="WebClient"/> и выставляет статус объекта <see cref="DownloadStatus.Aborted"/>.
        /// </summary>
        internal void CancelDownload()
        {

            if (webClient is not null)
            {
                webClient.CancelAsync();
            }
            this.Progress = 0.0;
            this.Image = null;
            this.DownloadStatus = DownloadStatus.Aborted; 
        }
        /// <summary>
        /// Асинхронно начинает загрузку изображения с помощью <see cref="WebClient"/>, выставляет статус работы объекта на <see cref="DownloadStatus.Running"/>. 
        /// если он ещё не запущен и его поле url не пусто.
        /// </summary>
        internal void StartDownloadAsync()
        {

            this.Image = null;

            if (this.DownloadStatus == DownloadStatus.Running || string.IsNullOrWhiteSpace(this.url))
            {
                return;
            }

            this.DownloadStatus = DownloadStatus.Running; 

            using (this.webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadDataCompleted += Client_DownloadDataCompleted;
                    webClient.DownloadProgressChanged += Client_DownloadProgressChanged;
                    webClient.DownloadDataAsync(new Uri(this.Url));
                }
                catch (OperationCanceledException) { }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }
        /// <summary>
        /// Присваивает <see cref="Progress"/> значение <see cref="System.ComponentModel.ProgressChangedEventArgs.ProgressPercentage"/> 
        /// при срабатывании <see cref="WebClient.DownloadProgressChanged"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) 
        {
            if (this.DownloadStatus != DownloadStatus.Aborted) 
            {
                this.Progress = e.ProgressPercentage;
            }
        }
        /// <summary>
        /// Присваивает <see cref="Image"/> значение типа <see cref="BitmapImage"/> в результате срабатывания <see cref="WebClient.DownloadDataCompleted"/>
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event arguments</param>
        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                this.Image = this.ConvertByteArrayToBitMapImage(e.Result);
                this.DownloadStatus = DownloadStatus.Successfull; 
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                this.DownloadStatus = DownloadStatus.Aborted; 
            }
        }
        /// <summary>
        /// Преобразует массив типа <see cref="byte"/> в <see cref="BitmapImage"/>.
        /// </summary>
        /// <param name="imageByteArray">Массив байт с картинкой</param>
        /// <returns></returns>
        private BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray) 
        {
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
                return img;
            }
        }
    }
}