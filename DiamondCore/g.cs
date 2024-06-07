using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace DiamondCore
{
    public static class g
    {
        public static int   DPIinch     { get; set; } = 197;
        public static float DPI         => DPIinch / 25.4f;
        public static float   DiamondSize { get; set; } = 25;
        public static int   GridWidth   { get; set; } = 1;
    }
}
