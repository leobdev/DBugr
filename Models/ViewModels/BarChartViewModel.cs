using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Models.ViewModels
{
    public class BarChartViewModel
    {
        public SubData[] bars { get; set; }
        public string[] categories { get; set; }
    }

    public class SubData
    {
        public string name { get; set; }
        public int[] data { get; set; }
    }
}
