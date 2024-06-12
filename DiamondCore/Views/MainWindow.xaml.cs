using System;
using System.Collections.Generic;
using System.Drawing;
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
using DiamondCore.Utils;
using DiamondCore.ViewModels;
using Size = System.Drawing.Size;

namespace DiamondCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var q = new DiamondCreator(new Sbmp("C:\\Users\\Nekohime\\Pictures\\Алмазная Зая\\2.png"));
            q.Create(PaperFormat.GetByName("A4"), ClusterizationType.Sphere, 0);
            q.SaveTestImage();
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
