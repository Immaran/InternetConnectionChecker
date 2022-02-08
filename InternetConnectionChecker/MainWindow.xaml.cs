using System;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace InternetConnectionChecker
{
    public partial class MainWindow : Window
    {
        private double _frequency;

        private string _ip;

        public MainWindow()
        {
            InitializeComponent();

            _frequency = Properties.Settings.Default.Frequency;

            _ip = Properties.Settings.Default.IP;

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(1 / _frequency);
            timer.Tick += Check;
            timer.Start();
        }

        private void Check(object sender, EventArgs e)
        {
            if (this.IsLoaded)
            {
                try
                {
                    if (new Ping().Send(_ip).Status.ToString().Equals("Success"))
                    {
                        root.Background = Brushes.Green;
                        textBlock.Foreground = Brushes.White;
                        textBlock.Text = "INTERNET IS ON!";
                    }
                }
                catch (PingException)
                {
                    root.Background = Brushes.Red;
                    textBlock.Foreground = Brushes.Black;
                    textBlock.Text = "INTERNET IS OFF!";
                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            textBlock.FontSize = this.Width / 10;
        }
    }
}
