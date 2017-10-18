using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Services
{
    class OpenMusicService
    {
        /// <summary>
        /// 打开一首音乐
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Music OpenMusic(string uri)
        {
            FileInfo theFile = new FileInfo(uri);
            Music music = new Music(theFile);
            return music;
        }
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static List<Music> OpenFolder(string uri)
        {
            List<Music> musicList = new List<Music>();

            DirectoryInfo theFloder = new DirectoryInfo(uri);
            FileInfo[] theFile1 = theFloder.GetFiles("*.mp3");
            FileInfo[] theFile2 = theFloder.GetFiles("*.wav");

            foreach (var music in theFile1)
            {
                musicList.Add(new Music(music));
            }
            foreach (var music in theFile2)
            {
                musicList.Add(new Music(music));
            }

            musicList.Sort((x, y) => x.Name.CompareTo(y.Name));
            return musicList;
        }
    }
}
