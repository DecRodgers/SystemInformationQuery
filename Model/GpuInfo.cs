using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace SystemInformationTest.Model
{
    public  class GpuInfo
    {
        public string gpuVendor {  get; set; }
        public string gpuModel { get; set; }
        public string gpuDriverVersion { get; set; }
        public UInt64 gpuMemory {  get; set; }

        public override string ToString()
        {
            string gpuString = gpuVendor;
            gpuString += "\n" + gpuModel;
            gpuString += "\n" + gpuDriverVersion;
            gpuString += "\n" + gpuMemory.ToString();
            return gpuString;
        }
    }
}
