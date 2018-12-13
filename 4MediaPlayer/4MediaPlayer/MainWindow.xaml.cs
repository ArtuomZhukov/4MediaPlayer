using Microsoft.Win32;
using System;
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
        private bool fullscreen = false;

        private void VideoPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
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
