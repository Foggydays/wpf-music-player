using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class PlayControls : UserControl
    {
        public PlayControls()
        {
            InitializeComponent();
            InitImageButton();
            this.Loaded += PlayControls_Loaded;
        }

        private void InitImageButton()
        {
            lastKey.DataContext = new BitmapImage(new Uri("/Image/full_prev.png", UriKind.Relative));
            nextKey.DataContext = new BitmapImage(new Uri("/Image/full_next.png", UriKind.Relative));
            stopKey.DataContext = new BitmapImage(new Uri("/Image/full_stop.png", UriKind.Relative));
        }

        void PlayControls_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
