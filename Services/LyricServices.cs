using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Services
{
    class LyricServices
    {
        //歌词时间偏移量
        private double offset;

        public double Offset { get; set; }
        //存放歌词的集合
        public SortedDictionary<TimeSpan, string> LyricLines { get; set; }

        public LyricServices(Music music, Encoding encoding)
        {
            LyricLines = new SortedDictionary<TimeSpan, string>();
            //打开歌词文件，并将歌词文件读取进来。
            string lyricPath = music.Url.Substring(0, music.Url.Length - 3) + "lrc";
            string fileString = OpenLyricFile(lyricPath, encoding);
            StartAnalyzeLyric(fileString);
        }

        #region
        /// <summary>
        /// 打开歌词文件流，并将歌词文件字符串读取出来并返回解析。
        /// </summary>
        /// <param name="lyricPath">歌词文件路径</param>
        /// <returns>歌词文件中的字符串</returns>
        private string OpenLyricFile(string lyricPath, Encoding encoding)
        {
            string tempStr = "";
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(lyricPath, encoding);
                //读取文件所有字节。
                tempStr = sr.ReadToEnd();
            }
            catch
            {
                tempStr = "";
            }
            finally
            {
                if (sr != null)
                {
                    sr.Dispose();
                }
            }
            return tempStr;
        }

        /// <summary>
        /// 对歌词文件的内容分类解析
        /// </summary>
        /// <param name="fileString">待解析的歌词字符串</param>
        private void StartAnalyzeLyric(string fileString)
        {
            //以行为单位进行第一步分割
            string[] stringLine;
            stringLine = fileString.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < stringLine.Length; i++)
            {
                if (stringLine[i].Contains('['))     //如果该行拥有[表明改行是需要解析行。
                {
                    string tempStr = stringLine[i].Trim();
                    #region 忽略无用的部分
                    if (tempStr.Contains("[ar:"))
                    {

                    }
                    else if (tempStr.Contains("[ti:"))
                    {

                    }
                    else if (tempStr.Contains("[al:"))
                    {

                    }
                    else if (tempStr.Contains("[by:"))
                    {

                    }
                    #endregion
                    //歌词的正文部分，即歌词和对应的时间。
                    else
                    {
                        if (tempStr.Contains("[offset]"))
                        {
                            double.TryParse(GetOffsetStr(tempStr), out this.offset);
                        }
                        else
                        {
                            GetLyricContentAndTime(tempStr);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取offset字符串
        /// </summary>
        /// <param name="tempStr"></param>
        /// <returns></returns>
        private string GetOffsetStr(string tempStr)
        {
            int index = tempStr.IndexOf(":") + 1;
            string str = tempStr.Substring(index, tempStr.Length - index - 1);
            return str;
        }

        /// <summary>
        /// 将歌词加入集合
        /// </summary>
        /// <param name="tempStr"></param>
        private void GetLyricContentAndTime(string tempStr)
        {
            //以中括号分割，获取时间标签
            string[] lineStr = tempStr.Split(']');
            string lyric = lineStr.Last().Trim().Replace("/", "\n");
            int indexM = lineStr.Length;
            TimeSpan timeSpan = new TimeSpan();
            for (int i = 0; i < indexM - 1; i++)
            {
                string time = "00:" + lineStr[i].Substring(1);
                if (TimeSpan.TryParse(time, out timeSpan))
                {
                    if (lyric == "")
                    {
                        LyricLines.Add(timeSpan, " ");
                    }
                    else
                    {
                        LyricLines.Add(timeSpan, lyric);
                    }
                }
            }
        }
        #endregion
    }
}