using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace MusicPlayer
{
    public class MusicPlay
    {
        //表示音乐播放状态
        public enum PlayState : int
        {
            stoped = 0,
            playing = 1,
            paused = 2
        }

        private MediaPlayer player = null;
        private PlayState state;

        public DispatcherTimer dt = null;
        public MusicPlay()
        {
            player = new MediaPlayer();
            state = PlayState.stoped;
        }

        public void Load(Uri file)
        {
            player = new MediaPlayer();
            player.Open(file);
        }

        //播放、暂停、停止的方法
        public void Play()
        {
            player.Play();
            state = PlayState.playing;
        }
        public void Pause()
        {
            player.Pause();
            state = PlayState.paused;
        }
        public void Stop()
        {
            player.Stop();
            state = PlayState.stoped;
        }

        //根据文件路径获取文件名字
        public string GetMusicTitle()
        {
            string title = player.Source.ToString();
            return title.Substring(title.LastIndexOf("/") + 1, title.Length - title.LastIndexOf("/") - 1);
        }

        public void StopPlayer()
        {
            player.Stop();
        }

        //获取音乐文件的自然持续时间：
        //用MediaPlayer的NaturalDuration.HasTimeSpan检查是否可以读取音乐的自然持续时间
        //还用了While循环避免了读取到空值或读取不到的情况
        public TimeSpan GetMusicDuringTime()
        {
            do
            {
                if (player.NaturalDuration.HasTimeSpan)
                {
                    return player.NaturalDuration.TimeSpan;
                }
            } while (!player.NaturalDuration.HasTimeSpan);
            return new TimeSpan();
        }

        //设置和获取当前进度的方法：
        public void SetPosition(TimeSpan tp)
        {
            player.Position = tp;
        }
        public TimeSpan GetPosition()
        {
            return player.Position;
        }

        //设置和读取音量
        public void SetVolume(double volum)
        {
            player.Volume = volum;
        }
        public double GetVolume()
        {
            return player.Volume;
        }

        //获取和设置当前播放器的状态，这里只是三种状态，但实际可能会更多：
        public PlayState GetPlayState()
        {
            return state;
        }
        /*public void SetPlayerState()
        {
            if (state == PlayState.stoped)
            {
                this.Stop();
            }
            else if (state == PlayState.playing)
            {
                this.Play();
            }
            else if (state == PlayState.paused)
            {
                this.Pause();
            }
        }*/
    }
}