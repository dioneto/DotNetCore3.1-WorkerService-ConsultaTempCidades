using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorTempo
{
    public class DadosMetereologicos
    {
        public Results results { get; set; }

    }
    public class Results
    {
        public string date { get; set; }
        public string time { get; set; }
        public int temp { get; set; }
        public string description { get; set; }
        public int humidity { get; set; }
        public string wind_speedy { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
    }
}
