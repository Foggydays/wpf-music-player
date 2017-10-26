using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModels
{
    public class MusicItemViewModel : NotificationObject
    {
        public Music Music { get; set; }

        public bool IsSelected { get; set; }
    }
}
