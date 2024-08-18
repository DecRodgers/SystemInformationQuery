using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SystemInformationTest.Model
{
    public class FullSystemInfo
    {
        public string systemName { get; set; }
        public CpuInfo cpuInfo { get; set; }
        public MotherboardInfo motherboardInfo { get; set; }
        public BiosInfo biosInfo { get; set; }
        public List<RamInfo> ramInfo { get; set; }
        public UInt64 totalRamCapacity { get => CalculateTotalRamCapacity(); }
        public GpuInfo gpuInfo { get; set; }
        public List<DiskInfo> diskInfo { get; set; }


        UInt64 CalculateTotalRamCapacity()
        {
            UInt64 total = 0;
            foreach (var ramModule in ramInfo) {
                total += ramModule.ramCapacity;           
            }        
            return total;
        }

        public override string ToString()
        {
            string systemString = systemName;
            systemString += "\n" + cpuInfo;
            systemString += "\n" + motherboardInfo;
            systemString += "\n" + biosInfo;
            foreach (var ramModule in ramInfo)
            {
                systemString += "\n" + ramModule;
            }            
            systemString += "\n" + totalRamCapacity;
            systemString += "\n" + gpuInfo;
            foreach (var disk in diskInfo) 
            {
                systemString += "\n" + disk;
            }            
            return systemString;
        }
    }
}
