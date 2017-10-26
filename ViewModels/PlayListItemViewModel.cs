using Microsoft.Practices.Prism.ViewModel;
using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModels
{
    public class PlayListItemViewModel : NotificationObject
    {
        public PlayList PlayList { get; set; }

        public bool IsSelected { get; set; }
    }
}
