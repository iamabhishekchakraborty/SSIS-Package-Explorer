using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.IO;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    public interface ISSISItem
    {
        List<PackageItem> Search(string searchPattern, object SSISExecutable, PackageInfo packageData,string parentName="",string parentType = "");
    }
}
