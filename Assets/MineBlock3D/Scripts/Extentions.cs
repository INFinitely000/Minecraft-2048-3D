using System.Collections.Generic;
using UnityEngine;

namespace MineBlock3D.Scripts
{
    public static class Extentions
    {
        private static Dictionary<int, string> _shortFormats = new Dictionary<int, string>()
        {
            [0] = "1",
            [1] = "2",
            [2] = "4",
            [3] = "8",
            [4] = "16",
            [5] = "32",
            [6] = "64",
            [7] = "128",
            [8] = "256",
            [9] = "512",
            [10] = "1K",
            [11] = "2K",
            [12] = "4K",
            [13] = "8K",
            [14] = "16K",
            [15] = "32K",
            [16] = "64K",
            [17] = "128K",
            [18] = "256K",
            [19] = "512K",
            [20] = "1M",
            [21] = "2M",
            [22] = "4M",
            [23] = "8M",
            [24] = "16M",
            [25] = "32M",
            [26] = "64M",
            [27] = "128M",
            [28] = "256M",
            [29] = "512M",
            [30] = "1B"
        };
        
        
        public static string FormatToShort(this int value)
        {
            return _shortFormats[value];
        }
    }
}