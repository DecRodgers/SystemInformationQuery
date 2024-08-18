using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInformationTest.Model
{
    public class CpuInfo
    {
        public string? cpuVendor { get; set; }
        public string? cpuModel { get; set; }
        public uint cpuCores { get; set; }
        public uint cpuThreads { get; set; }
        public uint cpuL2Cache { get; set; }
        public uint cpuL3Cache { get; set; }

        public override string ToString() 
        {
            string cpuDataSring;
            cpuDataSring = cpuVendor;
            cpuDataSring += "\n" + cpuModel;
            cpuDataSring += "\n" + cpuCores.ToString();
            cpuDataSring += "\n" + cpuThreads.ToString();
            cpuDataSring += "\n" + cpuL2Cache.ToString();
            cpuDataSring += "\n" + cpuL3Cache.ToString();
            return cpuDataSring;
        }
    }
}
