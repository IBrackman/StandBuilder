using System;
using System.Collections.Generic;

namespace StandBuilder
{
    public class Figure : ICloneable
    {
        public string Style { get; set; }
        public string Name { get; set; }
        public SysSetting Sys { get; set; }
        public int Cpu { get; set; } = 1;
        public int Memory { get; set; } = 1024;
        public string Repository { get; set; }
        public Dictionary<string, Dictionary<string, string>> Services { get; set; }
        public List<NetSetting> NetSettings { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}