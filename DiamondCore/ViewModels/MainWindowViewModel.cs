using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DiamondCore.Utils;
using ReactiveUI;

namespace DiamondCore.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private BitmapImage _img;
        public BitmapImage Img
        {
            get => _img;
            set => this.RaiseAndSetIfChanged(ref _img, value);
        }

        public MainWindowViewModel()
        {
            var sbmp = new Sbmp("C:\\Users\\Nekohime\\Pictures\\Алмазная Зая\\1.png");
            _img = Sbmp.BitmapToImageSource(sbmp.GetBmp());
        }
    }
}
