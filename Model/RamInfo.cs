using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInformationTest.Model
{
    public class RamInfo
    {
        public string ramManufacturer {  get; set; }
        public string ramBankNumber {  get; set; }
        public UInt64 ramCapacity { get; set; }
        public int ramSpeed { get; set; }

        public override string ToString()
        {
            string ramString = ramManufacturer;
            ramString += "\n" + ramBankNumber;
            ramString += "\n" + ramCapacity.ToString();
            ramString += "\n" + ramSpeed.ToString();
            return ramString;
        }
    }
}
