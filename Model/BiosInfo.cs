using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInformationTest.Model
{
    public class BiosInfo
    {
        public string biosManufacturer { get; set; }
        public string biosVersion { get; set; }

        public override string ToString()
        {
            string biosString = biosManufacturer;
            biosString += "\n" + biosVersion;
            return biosString;
        }
    }
}
