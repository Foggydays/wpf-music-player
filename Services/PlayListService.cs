using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace MusicPlayer.Services
{
    class PlayListService
    {
        /// <summary>
        /// 获取所有歌单
        /// </summary>
        /// <returns></returns>
        public static List<PlayList> GetAllPlayLists()
        {
            List<PlayList> musicLists = new List<PlayList>();
            DirectoryInfo theFloder = new DirectoryInfo(@"G:\");
            FileInfo[] theFile = theFloder.GetFiles("*.xml");
            foreach (var list in theFile)
            {
                musicLists.Add(new PlayList() { Url = list.FullName, Name = list.Name.Substring(0, list.Name.LastIndexOf(".")) });
            }
            return musicLists;
        }
        /// <summary>
        /// 新建歌单
        /// </summary>
        /// <param name="playLisy"></param>
        public static void NewPlayList(PlayList playLisy)
        {
            XDocument xmlFile = new XDocument();
            XElement root = new XElement("MusicList");
            xmlFile.Add(root);
            xmlFile.Save(@"G:\" + playLisy.Name + ".xml");
        }
        /// <summary>
        /// 删除歌单
        /// </summary>
        /// <param name="playList"></param>
        public static void DeletePlayList(PlayList playList)
        {
            FileInfo theFile = new FileInfo(playList.Url);
            if (!theFile.Exists)
            {
                theFile.Delete();
            }
        }
        /// <summary>
        /// 向歌单内添加音乐
        /// </summary>
        /// <param name="playList"></param>
        /// <param name="music"></param>
        public static void AddMusic(PlayList playList, Music music)
        {
            XDocument xmlFile = XDocument.Load(playList.Url);
            xmlFile.Root.Add(new XElement("Music",
                new XElement("Name", music.Name),
                new XElement("Url", music.Url)));
            xmlFile.Save(playList.Url);
        }
        /// <summary>
        /// 向歌单内添加音乐
        /// </summary>
        /// <param name="playList"></param>
        /// <param name="musicList"></param>
        public static void AddMusic(PlayList playList, List<Music> musicList)
        {
            XDocument xmlFile = XDocument.Load(playList.Url);
            foreach (var music in musicList)
            {
                xmlFile.Root.Add(new XElement("Music",
                new XElement("Name", music.Name),
                new XElement("Url", music.Url)));
            }
            xmlFile.Save(playList.Url);
        }
        /// <summary>
        /// 删除歌单内的音乐
        /// </summary>
        /// <param name="playList"></param>
        /// <param name="music"></param>
        public static void DeleteMusic(PlayList playList, Music music)
        {
            XDocument xmlFile = XDocument.Load(playList.Url);
            xmlFile.Root.Elements("Music").Where(p => p.Element("Name").Value.Equals(music.Name)).First().Remove();
            xmlFile.Save(playList.Url);
        }
        /// <summary>
        /// 清空列表
        /// </summary>
        /// <param name="playList"></param>
        public static void CleanMusic(PlayList playList)
        {
            XDocument xmlFile = XDocument.Load(playList.Url);
            xmlFile.Root.Remove();
            xmlFile.Add(new XElement("PlayList"));
            xmlFile.Save(playList.Url);
        }
        /// 打开播放列表
        /// </summary>
        /// <param name="playList"></param>
        /// <returns></returns>
        public static List<Music> GetMusicList(PlayList playList)
        {
            List<Music> musicList = new List<Music>();

            XDocument musicListDocument = XDocument.Load(playList.Url);
            XElement root = musicListDocument.Root;
            IEnumerable<XElement> allElements = root.Elements();

            foreach (var musicElement in allElements)
            {
                musicList.Add(new Music() { Name = musicElement.Element("Name").Value, Url = musicElement.Element("Url").Value });
            }

            return musicList;
        }
    }
}
