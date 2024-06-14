using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using ReactiveUI;
using Size = System.Drawing.Size;

namespace DiamondCore.Utils
{
    public enum ClusterizationType
    {
        Cube,
        Sphere
    }
    public class DiamondCreator : ReactiveObject
    {
        private Sbmp _sourceImage;
        public Sbmp SourceImage
        {
            get => _sourceImage;
            set => this.RaiseAndSetIfChanged(ref _sourceImage, value);
        }

        private Sbmp _resultImage;
        public Sbmp ResultImage
        {
            get => _resultImage;
            set => this.RaiseAndSetIfChanged(ref _resultImage, value);
        }

        private PaperFormat _format;
        public PaperFormat Format
        {
            get => _format;
            set => this.RaiseAndSetIfChanged(ref _format, value);
        }

        private DiamondColor[,] _diamondMap;
        public DiamondColor[,] DiamondMap
        {
            get => _diamondMap;
            set => this.RaiseAndSetIfChanged(ref _diamondMap, value);
        }

        private int _diamondCountWidth;
        public int DiamondCountWidth
        {
            get => _diamondCountWidth;
            set => this.RaiseAndSetIfChanged(ref _diamondCountWidth, value);
        }

        private int _diamondCountHeight;
        public int DiamondCountHeight
        {
            get => _diamondCountHeight;
            set => this.RaiseAndSetIfChanged(ref _diamondCountHeight, value);
        }

        private int _grabPixelCount;
        public int GrabPixelCount
        {
            get => _grabPixelCount;
            set => this.RaiseAndSetIfChanged(ref _grabPixelCount, value);
        }

        private int _skipPixelCountW;
        public int SkipPixelCountW
        {
            get => _skipPixelCountW;
            set => this.RaiseAndSetIfChanged(ref _skipPixelCountW, value);
        }

        private int _skipPixelCountH;
        public int SkipPixelCountH
        {
            get => _skipPixelCountH;
            set => this.RaiseAndSetIfChanged(ref _skipPixelCountH, value);
        }

        private Size _diamondImageSizePx;
        public Size DiamondImageSizePx
        {
            get => _diamondImageSizePx;
            set => this.RaiseAndSetIfChanged(ref _diamondImageSizePx, value);
        }

        private List<DiamondColor> _colorList = DiamondColor.GetAll();

        public DiamondCreator(Sbmp image)
        {
            _sourceImage = image;
        }

        public void Create(PaperFormat format, Thickness indent, ClusterizationType cType, int extendPixelCount)
        {
            DiamondImageSizePx = new Size(format.Width - (int) indent.Left - (int) indent.Right, format.Height - (int) indent.Top - (int) indent.Bottom);
            GrabPixelCount = DiamondImageSizePx.Width / DiamondImageSizePx.Height < SourceImage.Width / SourceImage.Height ? SourceImage.Height / (int) ((DiamondImageSizePx.Height) / (g.DiamondSize * g.DPI)) : SourceImage.Width / (int) ((DiamondImageSizePx.Width) / (g.DiamondSize * g.DPI));
            DiamondCountWidth  = SourceImage.Width  / GrabPixelCount;
            DiamondCountHeight = SourceImage.Height / GrabPixelCount;
            DiamondMap      = new DiamondColor[DiamondCountWidth, DiamondCountHeight];
            Pixelize();
        }

        public void Pixelize()
        {
            for(var i = 0; i < DiamondCountWidth; i++)
            for (var j = 0; j < DiamondCountHeight; j++)
                DiamondMap[i, j] = FindClosestColor(GetAbsColor(i * GrabPixelCount + SkipPixelCountW, j * GrabPixelCount + SkipPixelCountH, GrabPixelCount, 0));
        }

        public void CreateResultImage()
        {
            var image = new Sbmp(new byte[g.DiamondSizePx * DiamondCountWidth * g.DiamondSizePx * DiamondCountHeight * 4], DiamondCountWidth * g.DiamondSizePx, DiamondCountHeight * g.DiamondSizePx);
            for (var i = 0; i < DiamondCountWidth; i++)
            for (var j = 0; j < DiamondCountHeight; j++)
            for (var q = 0; q < g.DiamondSizePx; q++)
            for (var w = 0; w < g.DiamondSizePx; w++)
            {
                if (q == 0 || w == 0 || q == g.DiamondSizePx - 1 || w == g.DiamondSizePx - 1)
                    image.SetPixel(i * g.DiamondSizePx + q, j * g.DiamondSizePx + w, Color.Black);
                else
                    image.SetPixel(i * g.DiamondSizePx + q, j * g.DiamondSizePx + w, DiamondMap[i, j].Color);

            }

            image.Save("c:\\diatest\\test2.bmp");
        }

        public Color GetLighterColor(Color c)
        {
            return Color.FromArgb(255, (int) (c.R * 1.5f), (int) (c.G * 1.5f), (int) (c.B * 1.5f));
        }
        public void SaveTestImage()
        {
            var testImage = new Sbmp(new byte[DiamondCountWidth * DiamondCountHeight * 4], DiamondCountWidth, DiamondCountHeight);
            for(var i = 0; i < DiamondCountWidth;i++)
            for (var j = 0; j < DiamondCountHeight; j++)
            {
                testImage.SetPixel(i, j, DiamondMap[i, j].Color);
            }

            testImage.Save("c:\\diatest\\test.bmp");
        }

        public DiamondColor FindClosestColor(Color c)
        {
            var dist   = double.MaxValue;
            DiamondColor color = null;
            for (var i = 0; i < _colorList.Count; i++)
            {
               var d = Math.Sqrt(Math.Pow(_colorList[i].Color.R - c.R, 2) + Math.Pow(_colorList[i].Color.G - c.G, 2) + Math.Pow(_colorList[i].Color.B - c.B, 2));
               if (d < dist)
               {
                   dist  = d;
                   color = _colorList[i];
               }
            }

            return color;
        }

        public Color GetAbsColor(int x, int y, int count, int offset)
        {
            var r = 0;
            var g = 0;
            var b = 0;
            for (var i = x; i < SourceImage.Width && i < x + count + offset; i++)
            for (var j = y; j < SourceImage.Height && j < y + count + offset; j++)
            {
                if (i < 0)
                    i = 0;
                if (j < 0)
                    j = 0;

                var color = SourceImage.GetPixel(i, j);
                r += color.R;
                g += color.G;
                b += color.B;
            }

            return Color.FromArgb(255, r / (count * count), g / (count * count), b / (count * count));
        }
    }

    public static class Test
    {

        public static Color Lerp(this Color colour, Color to, float amount)
        {
            // start colours as lerp-able floats
            float sr = colour.R, sg = colour.G, sb = colour.B;

            // end colours as lerp-able floats
            float er = to.R, eg = to.G, eb = to.B;

            // lerp the colours to get the difference
            byte r = (byte)sr.Lerp(er, amount),
                 g = (byte)sg.Lerp(eg, amount),
                 b = (byte)sb.Lerp(eb, amount);

            // return the new colour
            return Color.FromArgb(r, g, b);
        }

        public static float Lerp(this float start, float end, float amount)
        {
            float difference = end - start;
            float adjusted   = difference * amount;
            return start + adjusted;
        }
    }
}
