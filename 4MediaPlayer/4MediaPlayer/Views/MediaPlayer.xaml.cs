using DevExpress.Mvvm;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _4MediaPlayer
{
    public partial class MediaPlayer : UserControl, INotifyPropertyChanged
    {
        public bool IsFullScreen { get; set; } = false;
        public bool IsPause { get; set; } = true;

        public MediaPlayer()
        {
            InitializeComponent();
            mediaVolume.Value = 0.5;
        }

        private void PlayDefault_Click(object sender, RoutedEventArgs e)
        {
            PlayClick();
        }

        private void FullScreen_Click(object sender, RoutedEventArgs e)
        {
            FullscreenClick();
        }

        public void PlayClick()
        {
            if (mediaPlayer.Source == null)
                return;
            if (IsPause)
                mediaPlayer.Play();
            else
                mediaPlayer.Pause();
            IsPause = !IsPause;
        }

        public void FullscreenClick()
        {
            IsFullScreen = !IsFullScreen;
        }

        private async void MediaPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement ctrl = (FrameworkElement)sender;
            ClickAttach.SetClicks(ctrl, e.ClickCount);
            await Task.Delay(300);
            DoClickStuff(ctrl);
        }

        private void DoClickStuff(FrameworkElement ctrl)
        {
            int clicks = ClickAttach.GetClicks(ctrl);
            ClickAttach.SetClicks(ctrl, 0);
            if (clicks > 0)
            {
                if (clicks == 1)
                {
                    PlayClick();
                }
                else
                {
                    FullscreenClick();
                }
            }
        }

        public class ClickAttach : FrameworkElement
        {
            public static int GetClicks(FrameworkElement ctrl)
            {
                return (int)ctrl.GetValue(ClicksProperty);
            }
            public static void SetClicks(FrameworkElement ctrl, int value)
            {
                ctrl.SetValue(ClicksProperty, value);
            }

            public static readonly DependencyProperty ClicksProperty = DependencyProperty.RegisterAttached(
                              "Clicks",
                              typeof(int),
                              typeof(FrameworkElement));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
