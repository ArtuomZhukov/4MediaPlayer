using DevExpress.Mvvm;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace _4MediaPlayer
{
    public partial class MediaPlayer : UserControl, INotifyPropertyChanged
    {
        public bool IsFullScreen { get; set; } = false;
        public bool IsPause { get; set; } = true;

        TimeSpan videoTimeSpan;
        DispatcherTimer VideoTimer;

        public MediaPlayer()
        {
            InitializeComponent();
            mediaVolume.Value = 0.5;
            mediaPlayerControls.Visibility = Visibility.Hidden;

            VideoTimer = new DispatcherTimer();
            VideoTimer.Tick += new EventHandler(VideoTimerTick);
            VideoTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void PlayDefault_Click(object sender, RoutedEventArgs e)
        {
            PlayClick();
        }

        private void FullScreen_Click(object sender, RoutedEventArgs e)
        {
            FullscreenClick();
        }

        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.IsMuted = !mediaPlayer.IsMuted;
        }

        private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            videoTimeSpan = TimeSpan.FromMilliseconds(mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds);
            VideoTimeSlider.Maximum = videoTimeSpan.TotalSeconds;
        }
        private void VideoTimeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(VideoTimeSlider.Value);
            mediaPlayer.Position = timeSpan;
        }

        public void PlayClick()
        {
            if (mediaPlayer.Source == null)
                return;
            if (IsPause)
            {
                mediaPlayer.Play();
                VideoTimer.Start();
            }
            else
            {
                mediaPlayer.Pause();
                VideoTimer.Stop();
            }
            IsPause = !IsPause;
        }

        public void FullscreenClick()
        {
            IsFullScreen = !IsFullScreen;
        }

        private void VideoTimerTick(object sender, EventArgs e)
        {
            VideoTimeSlider.Value = mediaPlayer.Position.TotalSeconds;
            if (VideoTimeSlider.Value == VideoTimeSlider.Maximum)
                PlayClick();
            else
                VideoTimeLabel.Content = String.Format("{0}:{1}/{2}:{3}",
                    mediaPlayer.Position.Minutes, mediaPlayer.Position.Seconds,
                videoTimeSpan.Minutes, videoTimeSpan.Seconds);
        }

        private void VideoPlayer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (mediaPlayer.Source != null)
                mediaPlayerControls.Visibility = Visibility.Visible;
        }

        private void VideoPlayer_MouseLeave(object sender, MouseEventArgs e)
        {
            mediaPlayerControls.Visibility = Visibility.Hidden;
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
