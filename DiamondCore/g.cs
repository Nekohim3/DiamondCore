using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondCore
{
    //todo бахнуть сохранение настроек
    public static class g
    {
        public static int       DPIinch       { get; set; } = 300; //пикселей на дюйм (118.11 пикселей на см)
        public static float     DPI           => DPIinch / 25.4f;  //пикселей на мм
        public static float     DiamondSize   { get; set; } = 2.5f;  // в мм
        public static int       DiamondSizePx { get; set; } = 45;
        public static Thickness Margin        { get; set; } = new(2, 2, 2, 2); //отступы от краев бумаги
        public static int       GridWidth     { get; set; } = 1;

        /* размер бумаги а4 297*210мм
         * допустим поля по 20мм с каждой стороны
         * рабочая область остается 277*190мм
         * пикселей на мм 11,811
         * размер страза 2,5мм
         * 
         * 
         * 
         */
    }
}
