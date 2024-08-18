using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInformationTest.Model
{
    public class DiskInfo
    {
        public string diskName {  get; set; }        
        public UInt64 diskTotalSize { get; set; }
        
        public UInt64 diskFreeSpace { get; set; }
        public UInt64 diskUsedSpace { get => CalculateUsedSpace(diskTotalSize, diskFreeSpace); }

        public override string ToString()
        {
            string diskString = diskName;
            diskString += "\n" + diskTotalSize.ToString();
            diskString += "\n" + diskFreeSpace.ToString();
            diskString += "\n" + diskUsedSpace.ToString();
            return diskString;
        }

        UInt64 CalculateUsedSpace(UInt64 total, UInt64 free) 
        { 
            return total - free;
        }
    }
}
