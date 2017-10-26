using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPlayer
{
    /// <summary>
    /// LyricPanel.xaml 的交互逻辑
    /// </summary>
    public partial class LyricPanel : UserControl
    {
        public LyricPanel()
        {
            InitializeComponent();
            LyricItemViemModel.PlayLyric += PlayLyricEventHandler;
        }

        public static readonly DependencyProperty LyricProperty = 
            DependencyProperty.Register("Lyric", typeof(List<LyricItemViemModel>), typeof(LyricPanel));
        public List<LyricItemViemModel> Lyric
        {
            get { return (List<LyricItemViemModel>)GetValue(LyricProperty); }
            set { SetValue(LyricProperty, value); }
        }

        private void PlayLyricEventHandler(LyricItemViemModel lrc)
        {
            int i = Lyric.BinarySearch(lrc);
            Panel.Margin = new Thickness(0, (i * -56 + 75), 0, 0);
        }
    }
}
