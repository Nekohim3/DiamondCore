using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ReactiveUI;

namespace DiamondCore.Utils
{
    public class PaperFormat : ReactiveObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private int _width;
        public int Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }

        private int _height;
        public int Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        public PaperFormat(string name, int width, int height)
        {
            _name   = name;
            _width  = width;
            _height = height;
        }

        public string DisplayName => $"{Name}: {Width}*{Height}";

        public static List<PaperFormat> GetFullList() => new()
                                                         {
                                                             new PaperFormat("A1", (int) Math.Round((g.DPI / 25.4) * 840, 0), (int) Math.Round((g.DPI / 25.4) * 594, 0)),
                                                             new PaperFormat("A2", (int) Math.Round((g.DPI / 25.4) * 594, 0), (int) Math.Round((g.DPI / 25.4) * 420, 0)),
                                                             new PaperFormat("A3", (int) Math.Round((g.DPI / 25.4) * 420, 0), (int) Math.Round((g.DPI / 25.4) * 297, 0)),
                                                             new PaperFormat("A4", (int) Math.Round((g.DPI / 25.4) * 297, 0), (int) Math.Round((g.DPI / 25.4) * 210, 0))
                                                         };
        //lst.Add(new PaperFormat("A1", (int)Math.Round((g.DPI / 25.4) * 840, 0), (int)Math.Round((g.DPI / 25.4) * 594, 0)));
    }
}
