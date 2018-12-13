using System.IO;
using System.Windows.Controls;

namespace _4MediaPlayer
{
    public partial class MediaItem : UserControl
    {
        private MainWindow MainWindow;

        public string Path { get; }

        public MediaType Type { get; }

        public enum MediaType : byte
        {
            Video,
            Audio,
            Image,
            Gif,
            None
        }

        public MediaItem()
        {
            InitializeComponent();
        }

        public bool IsAvailable()
        {
            return File.Exists(Path);
        }

        public static MediaType GetMediaType(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                switch (fileInf.Extension)
                {
                    case"mkv":
                    case"mp4":
                    case"avi":
                    case"wmv":
                        return MediaType.Video;
                    case "mp3":
                    case "wav":
                    case "flac":
                        return MediaType.Audio;
                    case "png":
                    case "jpg":
                    case "jpeg":
                        return MediaType.Image;
                    case "gif":
                        return MediaType.Gif;
                }
            }
            return MediaType.None;
        }
    }
}
