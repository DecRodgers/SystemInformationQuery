using Cudafy.Host;
using Cudafy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SystemInformationTest.Model;

namespace SystemInformationTest.SystemInfoReturn
{
    [SupportedOSPlatform("windows")]
    public class SystemInfoReturn
    {
        public FullSystemInfo ReturnSystemInfoObject
        {
            get
            {
                FullSystemInfo system = new FullSystemInfo();
                system.systemName = GetSystemName();
                system.cpuInfo = GetCpuInfoObject();
                system.motherboardInfo = GetMotherboardInfoObject();
                system.biosInfo = GetBiosInfoObject();
                system.ramInfo = GetRamInfoList();
                system.gpuInfo = GetGpuInfoObject();
                system.diskInfo = GetDiskInfoList();
                return system;
            }
        }


        private string GetSystemName()
        {
            string systemName;
            var c = new ManagementObjectSearcher("select SystemName from Win32_Processor");
            var enu = c.Get().GetEnumerator();
            if (!enu.MoveNext()) throw new Exception("Unexpected WMI query failure");
            systemName = enu.Current["SystemName"].ToString();
            return systemName;
        }

        private CpuInfo GetCpuInfoObject() 
        {
            CpuInfo cpu = new CpuInfo();
            using (var searcher = new ManagementObjectSearcher("select Name,NumberOfCores,NumberOfLogicalProcessors,L2CacheSize,L3CacheSize from Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {

                    string FullCpuString = obj["Name"].ToString();
                    string cpuVendor;
                    if (FullCpuString.Contains("Intel"))
                    {
                        cpuVendor = "Intel";
                    }
                    else
                    {
                        cpuVendor = "AMD";
                    }
                    Match matchs = Regex.Match(FullCpuString, "((?<=" + cpuVendor + @"...\s)).*$");
                    string cpuModel = matchs.Value;
                    cpu.cpuVendor = cpuVendor;
                    cpu.cpuModel = cpuModel;
                    cpu.cpuCores = (uint)obj["NumberOfCores"];
                    cpu.cpuThreads = (uint)obj["NumberOfLogicalProcessors"];
                    cpu.cpuL2Cache = (uint)obj["L2CacheSize"];
                    cpu.cpuL3Cache = (uint)obj["L3CacheSize"];
                }
            }
            return cpu;
        }

        private MotherboardInfo GetMotherboardInfoObject()
        {
            MotherboardInfo motherboard = new MotherboardInfo();
            var c = new ManagementObjectSearcher("select Manufacturer,Product from Win32_BaseBoard");
            var enu = c.Get().GetEnumerator();
            if (!enu.MoveNext()) throw new Exception("Unexpected WMI query failure");
            motherboard.motherboardVendor = enu.Current["Manufacturer"].ToString();
            motherboard.motherboardModel = enu.Current["Product"].ToString();
            return motherboard;
        }

        private BiosInfo GetBiosInfoObject() 
        {
            BiosInfo bios = new BiosInfo();
            var c = new ManagementObjectSearcher("select Manufacturer,SMBIOSBIOSVersion from Win32_BIOS");
            var enu = c.Get().GetEnumerator();
            if (!enu.MoveNext()) throw new Exception("Unexpected WMI query failure");
            bios.biosManufacturer = enu.Current["Manufacturer"].ToString();
            bios.biosVersion = enu.Current["SMBIOSBIOSVersion"].ToString();
            return bios;
        }

        private List<RamInfo> GetRamInfoList()
        {
            List<RamInfo> ram = new List<RamInfo>();            
            using (var searcher = new ManagementObjectSearcher("select Manufacturer,Capacity,ConfiguredClockSpeed,BankLabel from Win32_PhysicalMemory "))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    RamInfo ramModule = new RamInfo();
                    ramModule.ramManufacturer = obj["Manufacturer"].ToString();
                    ramModule.ramBankNumber = obj["BankLabel"].ToString();
                    ramModule.ramCapacity = Convert.ToUInt64(obj["Capacity"]);
                    ramModule.ramSpeed = Convert.ToInt32(obj["ConfiguredClockSpeed"]);
                    ram.Add(ramModule);
                }
            }
            return ram;
        }

        private GpuInfo GetGpuInfoObject()
        { 
            GpuInfo gpu = new GpuInfo();            
            using (var searcher = new ManagementObjectSearcher("select Name,DriverVersion from Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    string Pattern = @"^(?<vendor>[^\s\d]+)+(?<model>\s(.*))";
                    string? FullGpuString = obj["Name"].ToString();
                    Match matchs = Regex.Match(FullGpuString, Pattern);
                    string gpuVendor = matchs.Groups["vendor"].Value;
                    string gpuModel = matchs.Groups["model"].Value;
                    gpu.gpuVendor = gpuVendor;
                    gpu.gpuModel = gpuModel.TrimStart();
                    gpu.gpuDriverVersion = obj["DriverVersion"].ToString();
                    if (gpuVendor == "NVIDIA")
                    {
                        var gpuRAM = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId).GetDeviceProperties(true).TotalMemory;
                        gpu.gpuMemory = Convert.ToUInt64(gpuRAM);
                    }
                }
            }
            return gpu;
        }

        private List<DiskInfo> GetDiskInfoList() 
        { 
            List<DiskInfo> disks = new List<DiskInfo>();
            //using (var searcher = new ManagementObjectSearcher("select VolumeName,Size,FreeSpace from Win32_LogicalDisk"))
            using (var searcher = new ManagementObjectSearcher("select * from Win32_LogicalDisk"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    DiskInfo disk = new DiskInfo();
                    disk.diskName = obj["VolumeName"].ToString();
                    disk.diskTotalSize = Convert.ToUInt64(obj["Size"]);
                    disk.diskFreeSpace = Convert.ToUInt64(obj["FreeSpace"]);
                    disks.Add(disk);
                }
            }
            return disks;
        }
    }
}
