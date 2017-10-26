using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MusicPlayer.Models;
using MusicPlayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MusicPlayer.ViewModels
{
    class MainWindowViewModel : NotificationObject
    {
        private MusicPlay play;

        //正在播放的音乐
        private MusicItemViewModel playing;

        public MusicItemViewModel Playing
        {
            get { return playing; }
            set
            {
                playing = value;
                RaisePropertyChanged("Playing");
            }
        }

        private BitmapImage playIcon;

        public BitmapImage PlayIcon
        {
            get { return playIcon; }
            set
            {
                playIcon = value;
                RaisePropertyChanged("PlayIcon");
            }
        }

        //播放顺序
        private int playOrder;

        public int PlayOrder
        {
            get { return playOrder; }
            set
            {
                playOrder = value;
                RaisePropertyChanged("PlayOrder");
            }
        }

        private BitmapImage orderIcon;

        public BitmapImage OrderIcon
        {
            get { return orderIcon; }
            set
            {
                orderIcon = value;
                RaisePropertyChanged("OrderIcon");
            }
        }

        //播放进度
        private TimeSpan position;

        public TimeSpan Position
        {
            get { return position; }
            set
            {
                position = value;
                RaisePropertyChanged("Position");
            }
        }

        //持续时间
        private double maxTime;

        public double MaxTime
        {
            get { return maxTime; }
            set
            {
                maxTime = value;
                RaisePropertyChanged("MaxTime");
            }
        }

        //音量
        private double volume;

        public double Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                RaisePropertyChanged("Volume");
            }
        }

        //播放列表
        private List<PlayListItemViewModel> playLists;

        public List<PlayListItemViewModel> PlayLists
        {
            get { return playLists; }
            set
            {
                playLists = value;
                RaisePropertyChanged("PlayLists");
            }
        }

        //音乐列表
        private List<MusicItemViewModel> musicList;

        public List<MusicItemViewModel> MusicList
        {
            get { return musicList; }
            set
            {
                musicList = value;
                RaisePropertyChanged("MusicList");
            }
        }

        //歌词
        private List<LyricItemViemModel> lyric;

        public List<LyricItemViemModel> Lyric
        {
            get { return lyric; }
            set
            {
                lyric = value;
                RaisePropertyChanged("Lyric");
            }
        }


        public DelegateCommand OpenFileCommand { get; set; }
        public DelegateCommand OpenFolderCommand { get; set; }
        public DelegateCommand DeleteMusicCommand { get; set; }

        public DelegateCommand PlayListClickCommand { get; set; }
        public DelegateCommand MusicListDoubleClickCommand { get; set; }

        public DelegateCommand LastMusicCommand { get; set; }
        public DelegateCommand PlayMusicCommand { get; set; }
        public DelegateCommand NextMusicCommand { get; set; }
        public DelegateCommand StopMusicCommand { get; set; }
        public DelegateCommand PlayOrderCommand { get; set; }

        public DelegateCommand TimeControlButtonDown { get; set; }
        public DelegateCommand<Slider> TimeControlButtonUp { get; set; }
        public DelegateCommand VolumeChangedCommand { get; set; }

        public MainWindowViewModel()
        {
            InitPlayList();
            InitMusicList(PlayLists.First());
            Lyric = new List<LyricItemViemModel>();
            play = new MusicPlay();
            PlayOrder = 0;
            MaxTime = 1;
            PlayIcon = new BitmapImage(new Uri("/Image/full_play.png", UriKind.Relative));
            OrderIcon = new BitmapImage(new Uri("/Image/full_recycle.png", UriKind.Relative));
            Volume = play.GetVolume();

            play.dt = new System.Windows.Threading.DispatcherTimer();
            play.dt.Interval = TimeSpan.FromSeconds(0.1);
            play.dt.Tick += Dt_Tick;

            OpenFileCommand = new DelegateCommand(OpenMusic);
            OpenFolderCommand = new DelegateCommand(OpenFolder);
            DeleteMusicCommand = new DelegateCommand(DeleteMusic);
            PlayListClickCommand = new DelegateCommand(PlayListClick);
            MusicListDoubleClickCommand = new DelegateCommand(MusicListDoubleClick);
            LastMusicCommand = new DelegateCommand(LastMusic);
            PlayMusicCommand = new DelegateCommand(PlayMusic);
            NextMusicCommand = new DelegateCommand(NextMusic);
            StopMusicCommand = new DelegateCommand(StopMusic);
            PlayOrderCommand = new DelegateCommand(ClickPlayOrder);
            TimeControlButtonDown = new DelegateCommand(TimeControlButtonDownExcute);
            TimeControlButtonUp = new DelegateCommand<Slider>(TimeControlButtonUpExcute);
            VolumeChangedCommand = new DelegateCommand(VolumeChanged);
        }

        /// <summary>
        /// 初始化歌单列表
        /// </summary>
        private void InitPlayList()
        {
            PlayLists = new List<PlayListItemViewModel>();
            foreach (var item in PlayListService.GetAllPlayLists())
            {
                PlayListItemViewModel viewModel = new PlayListItemViewModel();
                viewModel.PlayList = item;
                PlayLists.Add(viewModel);
            }
        }
        /// <summary>
        /// 初始化音乐列表
        /// </summary>
        /// <param name="playList"></param>
        private void InitMusicList(PlayListItemViewModel playList)
        {
            MusicList = new List<MusicItemViewModel>();
            playList.IsSelected = true;
            foreach (var item in PlayListService.GetMusicList(playList.PlayList))
            {
                MusicItemViewModel viewModel = new MusicItemViewModel();
                viewModel.Music = item;
                MusicList.Add(viewModel);
            }
        }
        /// <summary>
        /// 打开一首音乐
        /// </summary>
        private void OpenMusic()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "所有支持的文件|*.mp3;*.wav|mp3|*.mp3|wav|*.wav";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var selectedPlayList = PlayLists.Where(i => i.IsSelected == true).First();
                var playList = selectedPlayList.PlayList;
                PlayListService.CleanMusic(playList);
                PlayListService.AddMusic(playList, OpenMusicService.OpenMusic(ofd.FileName));
                InitMusicList(selectedPlayList);
            }
        }
        //打开一个文件夹
        private void OpenFolder()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择要打开的文件夹";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                var selectedPlayList = PlayLists.Where(i => i.IsSelected == true).First();
                var playList = selectedPlayList.PlayList;
                PlayListService.CleanMusic(playList);
                PlayListService.AddMusic(playList, OpenMusicService.OpenFolder(folder.SelectedPath));
                InitMusicList(selectedPlayList);
            }
        }
        //只能删除一个
        /// <summary>
        /// 删除音乐
        /// </summary>
        private void DeleteMusic()
        {
            List<MusicItemViewModel> selectedItem = MusicList.Where(i => i.IsSelected == true).ToList();
            var selectedPlayList = PlayLists.Where(i => i.IsSelected == true).First();
            if (selectedItem.Count > 0)
            {
                PlayListService.DeleteMusic(selectedPlayList.PlayList, selectedItem[0].Music);
                InitMusicList(selectedPlayList);
            }
        }
        /// <summary>
        /// 切换播放列表
        /// </summary>
        private void PlayListClick()
        {
            var selectedPlayList = PlayLists.Where(i => i.IsSelected == true).First();
            InitMusicList(selectedPlayList);
        }
        /// <summary>
        /// 双击播放音乐
        /// </summary>
        private void MusicListDoubleClick()
        {
            StopMusic();
            PlayMusic();
        }
        /// <summary>
        /// 上一曲
        /// </summary>
        private void LastMusic()
        {
            List<MusicItemViewModel> selectedItem = MusicList.Where(i => i.IsSelected == true).ToList();
            if (selectedItem.Count > 0)
            {
                Playing = selectedItem.First();
                int index = MusicList.IndexOf(Playing);
                if (index >= 1)
                {
                    Playing.IsSelected = false;
                    Playing = MusicList[--index];
                    Playing.IsSelected = true;
                    System.Threading.Thread.Sleep(100);
                    PlayMusic(Playing.Music);
                }
                else
                {
                    Playing.IsSelected = false;
                    Playing = MusicList.Last();
                    Playing.IsSelected = true;
                    System.Threading.Thread.Sleep(100);
                    PlayMusic(Playing.Music);
                }
                PlayIcon = new BitmapImage(new Uri("/Image/full_pause.png", UriKind.Relative));
            }
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        private void PlayMusic()
        {
            List<MusicItemViewModel> selectedItem = MusicList.Where(i => i.IsSelected == true).ToList();
            //正在播放就暂停
            if (play.GetPlayState() == MusicPlay.PlayState.playing)
            {
                play.dt.Stop();
                PlayIcon = new BitmapImage(new Uri("/Image/full_play.png", UriKind.Relative));
                play.Pause();
            }
            else if (play.GetPlayState() == MusicPlay.PlayState.stoped)
            {
                //没有选择歌曲，且没有在播放
                if (selectedItem.Count == 0)
                {
                    Playing = MusicList[0];
                    Playing.IsSelected = true;
                    PlayIcon = new BitmapImage(new Uri("/Image/full_pause.png", UriKind.Relative));
                    PlayMusic(Playing.Music);
                }
                //选择了歌曲，且没有在播放
                else
                {
                    PlayIcon = new BitmapImage(new Uri("/Image/full_pause.png", UriKind.Relative));
                    Playing = selectedItem.First();
                    PlayMusic(Playing.Music);
                }
            }
            //暂停则播放
            else
            {
                PlayIcon = new BitmapImage(new Uri("/Image/full_pause.png", UriKind.Relative));
                play.Play();
                play.dt.Start();
            }
        }
        /// <summary>
        /// 下一曲
        /// </summary>
        private void NextMusic()
        {
            List<MusicItemViewModel> selectedItem = MusicList.Where(i => i.IsSelected == true).ToList();
            if (selectedItem.Count > 0)
            {
                Playing = selectedItem.First();
                int index = MusicList.IndexOf(Playing);
                if (PlayOrder == 0)
                {
                    if (index < MusicList.Count - 1)
                    {
                        Playing.IsSelected = false;
                        Playing = MusicList[++index];
                        Playing.IsSelected = true;
                        //System.Threading.Thread.Sleep(100);
                        PlayMusic(Playing.Music);
                    }
                    else
                    {
                        Playing.IsSelected = false;
                        Playing = MusicList.First();
                        Playing.IsSelected = true;
                        //System.Threading.Thread.Sleep(100);
                        PlayMusic(Playing.Music);
                    }
                }
                else if (PlayOrder == 1)
                {
                    Random random = new Random();
                    int select;
                    do
                    {
                        select = random.Next(MusicList.Count);
                    } while (select == index);
                    Playing.IsSelected = false;
                    Playing = MusicList[select];
                    playing.IsSelected = true;
                    PlayMusic(Playing.Music);
                }
                
                PlayIcon = new BitmapImage(new Uri("/Image/full_pause.png", UriKind.Relative));
            }
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        private void StopMusic()
        {
            PlayIcon = new BitmapImage(new Uri("/Image/full_play.png", UriKind.Relative));
            Playing = new MusicItemViewModel();
            play.dt.Stop();
            play.Stop();
            Position = new TimeSpan();
        }
        /// <summary>
        /// 切换播放模式
        /// </summary>
        private void ClickPlayOrder()
        {
            if (PlayOrder == 0)
            {
                PlayOrder = 1;
                OrderIcon = new BitmapImage(new Uri("/Image/full_random.png", UriKind.Relative));
            }
            else
            {
                PlayOrder = 0;
                OrderIcon = new BitmapImage(new Uri("/Image/full_recycle.png", UriKind.Relative));
            }
        }
        /// <summary>
        /// 播放音乐的方法
        /// </summary>
        private void PlayMusic(Music music)
        {
            play.Stop();
            play.Load(new Uri(music.Url));
            InitLyric(music);
            play.Play();
            System.Threading.Thread.Sleep(500);
            MaxTime = play.GetMusicDuringTime().TotalSeconds;
            play.dt.Start();
        }
        /// <summary>
        /// 按下进度条时，暂停进度条
        /// </summary>
        private void TimeControlButtonDownExcute()
        {
            play.dt.Stop();
        }
        /// <summary>
        /// 释放进度条时
        /// </summary>
        private void TimeControlButtonUpExcute(Slider sli)
        {
            Position = TimeSpan.FromSeconds(sli.Value);
            play.SetPosition(Position);
            play.dt.Start();
        }
        /// <summary>
        /// 控制音量
        /// </summary>
        private void VolumeChanged()
        {
            play.SetVolume(Volume);
        }

        int i = 0;
        private void Dt_Tick(object sender, EventArgs e)
        {
            Position = play.GetPosition();
            if (Position.TotalSeconds == MaxTime)
            {
                StopMusic();
                NextMusic();
            }
            if (i < Lyric.Count && Position >= Lyric[i].Time && Lyric[i].Playing == false)
            {
                Lyric[i].Playing = true;
                Console.WriteLine(Lyric[i].Time.ToString() + Position.ToString());
                if (i > 0)
                {
                    Lyric[i - 1].Playing = false;
                }
                i++;
            }
        }

        /// <summary>
        /// 初始化歌词
        /// </summary>
        /// <param name="music"></param>
        private void InitLyric(Music music)
        {
            List<LyricItemViemModel> lyric = new List<LyricItemViemModel>();
            i = 0;
            LyricServices lyricSer = new LyricServices(music, null);
            var lyricLines = lyricSer.LyricLines;
            foreach (var item in lyricLines)
            {
                lyric.Add(new LyricItemViemModel() { Time = item.Key, Lyric = item.Value });
            }
            Lyric = lyric;
        }
    }
}
