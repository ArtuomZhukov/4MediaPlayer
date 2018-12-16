using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _4MediaPlayer
{
    public partial class MainWindow : Window
    {
        string MediaPath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog
            {
                CheckFileExists = true,
            };
            if (myDialog.ShowDialog() == true)
            {
                MediaPath = myDialog.FileName;
            }
        }

        private void StartStopVideoButton_Click(object sender, RoutedEventArgs e)
        {
            Uri source = new Uri(MediaPath);
            VideoPlayer.Source = source;
            VideoPlayer.Play();
        }

        private async void VideoPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement ctrl = (FrameworkElement)sender;
            ClickAttach.SetClicks(ctrl, e.ClickCount);
            await Task.Delay(300);
            DoClickStuff(ctrl);
        }

        private bool fullscreen = false;
        private bool isPause = false;
        private void DoClickStuff(FrameworkElement ctrl)
        {
            int clicks = ClickAttach.GetClicks(ctrl);
            ClickAttach.SetClicks(ctrl, 0);
            if (clicks > 0)
            {
                if (clicks == 1)
                {
                    if (isPause)
                    {
                        VideoPlayer.Play();
                    }
                    else
                    {
                        VideoPlayer.Pause();
                    }
                    isPause = !isPause;
                }
                else
                {
                    if (!fullscreen)
                    {
                        WindowStyle = WindowStyle.None;
                        WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        WindowStyle = WindowStyle.SingleBorderWindow;
                        WindowState = WindowState.Normal;
                    }
                    fullscreen = !fullscreen;
                }
            }
        }
    }
}
