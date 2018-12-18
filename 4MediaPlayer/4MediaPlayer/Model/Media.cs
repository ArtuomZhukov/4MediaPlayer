using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4MediaPlayer.Model;

namespace _4MediaPlayer.Model
{
    class Media : BaseVM
    {
        public string Name { get; set; }
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




    }
}
