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
    /// NewImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButton : UserControl, ICommandSource
    {
        public ImageButton()
        {
            InitializeComponent();
        }

        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.VerticalAlignment = VerticalAlignment.Top;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Command.Execute(null);
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            image.HorizontalAlignment = HorizontalAlignment.Right;
            image.VerticalAlignment = VerticalAlignment.Bottom;
        }
    }
}
