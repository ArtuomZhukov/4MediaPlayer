using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _4MediaPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string path = "";
        private void LoadVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog
            {
                //Filter = /*"Видео(*.*)|*.*"*/"Видео(*.MP4;*.AVI;)|*.MP4;*.AVI",
                CheckFileExists = true,
                //Multiselect = true
            };
            if (myDialog.ShowDialog() == true)
            {
                //GetLoadVideos(myDialog.FileNames);
                path = myDialog.FileName;
            }
        }

        private void StartStopVideoButton_Click(object sender, RoutedEventArgs e)
        {
            //Uri source = new Uri(path);
            //VideoPlayer.Source = source;
            //VideoPlayer.Play();
            string html = @"<iframe width=""560"" height=""315"" src=""http://www.youtube.com/watch?v=gToqBo7T-tI"" frameborder=""0"" allowfullscreen></iframe>";
            myWebView.NavigateToString(html);
        }
    }
}
