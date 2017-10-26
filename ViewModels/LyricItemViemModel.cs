using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayer.ViewModels
{
    public class LyricItemViemModel : NotificationObject, IComparable<LyricItemViemModel>
    {
        public string Lyric { get; set; }

        private bool playing;

        public bool Playing
        {
            get { return playing; }
            set
            {
                playing = value;
                RaisePropertyChanged("Playing");
                if (value == true)
                {
                    PlayLyric(this);
                }
            }
        }

        public TimeSpan Time { get; set; }

        public LyricItemViemModel()
        {
            playing = false;
        }

        public delegate void PlayLyricEventHandler(LyricItemViemModel lrc);

        public static event PlayLyricEventHandler PlayLyric;

        public int CompareTo(LyricItemViemModel other)
        {
            LyricItemViemModel otherLyric = other as LyricItemViemModel;
            if (otherLyric != null)
                return Time.CompareTo(otherLyric.Time);

            return 0;
        }
    }
}
