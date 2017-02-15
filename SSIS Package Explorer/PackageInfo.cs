using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;

namespace SSIS_Package_Reader
{
    public class PackageInfo
    {
        public string GUID;
        public string FileName;
        public string Name;
        public string Type;
    }
}
