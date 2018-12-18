namespace _4MediaPlayer.Model
{
    public class Media : BaseVM
    {
        public string Name { get; set; }
        public string Path { get; set; }
        //media Preview
        public bool IsVisiblePreview { get; set; } = false;
        //public MediaType Type { get; }
        //public enum MediaType : byte
        //{
        //    Video,
        //    Audio,
        //    Image,
        //    Gif,
        //    None
        //}
    }
}
