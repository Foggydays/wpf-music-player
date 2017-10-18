using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    class Music
    {
        public String Name { get; set; }
        public String Url { get; set; }

        public Music()
        {

        }
        public Music(FileInfo file)
        {
            Url = file.FullName;
            Name = file.Name.Substring(0, file.Name.LastIndexOf("."));
        }
    }
}
