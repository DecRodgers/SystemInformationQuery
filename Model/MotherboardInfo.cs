using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInformationTest.Model
{
    public class MotherboardInfo
    {
        public string? motherboardModel { get; set; }
        public string? motherboardVendor { get; set; }

        public override string ToString()
        {
            string motherboardString;
            motherboardString = motherboardModel;
            motherboardString += "\n"+ motherboardVendor;
            return motherboardString;
        }
    }
}
