namespace _4MediaPlayer.Model
{
    public class Media : BaseVM
    {
        public string Name { get; set; }
        public string Path { get; set; }
        //media Preview
        //public bool IsVisiblePreview { get; set; } = false;
        public MediaType Type { get; set; }
        public enum MediaType : byte
        {
            Video,
            Audio,
            Image,
            Gif,
            Unknown
        }

        public static MediaType GetType(string path)
        {
            switch (System.IO.Path.GetExtension(path).ToLower())
            {
                case ".mkv":
                case ".mp4":
                case ".avi":
                case ".wmv":
                    return MediaType.Video;
                case ".mp3":
                case ".wav":
                case ".flac":
                    return MediaType.Audio;
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                    return MediaType.Image;
                    //return MediaType.Gif;
                default:
                    return MediaType.Unknown;
            }
        }
    }
}
