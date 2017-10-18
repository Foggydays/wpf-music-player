using MusicPlayer;
using MusicPlayer.Models;
using MusicPlayer.Services;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainWindowViewModel;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            deleteButton.DataContext = new BitmapImage(new Uri("/Image/bt_del.png", UriKind.Relative));
            openButton.DataContext = new BitmapImage(new Uri("/Image/bt_open.png", UriKind.Relative));
            menuButton.DataContext = new BitmapImage(new Uri("/Image/bt_main.png", UriKind.Relative));
            testButton.DataContext = new BitmapImage(new Uri("/Image/bt_activ.png", UriKind.Relative));

            menuButton.Command = mainWindowViewModel.OpenFileCommand;
            openButton.Command = mainWindowViewModel.OpenFolderCommand;
            deleteButton.Command = mainWindowViewModel.DeleteMusicCommand;
            testButton.Command = mainWindowViewModel.ShowDialogCommand;
        }

        private void PlayControls_Loaded(object sender, RoutedEventArgs e)
        {
            playcontrols.playKey.Command = mainWindowViewModel.PlayMusicCommand;
            playcontrols.nextKey.Command = mainWindowViewModel.NextMusicCommand;
            playcontrols.lastKey.Command = mainWindowViewModel.LastMusicCommand;
            playcontrols.stopKey.Command = mainWindowViewModel.StopMusicCommand;
        }
    }
}
