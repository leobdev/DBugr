using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Models.ViewModels
{
    public class DonutViewModel
    {
        public string[] labels { get; set; }
        public int[] series { get; set; }
        public DonutSubData[] datasets { get; set; }

    }

    public class DonutSubData
    {
        public int[] data { get; set; }
        public string[] backgroundColor { get; set; }
    }
}
