using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SSIS_Package_Reader
{
    public static class SQLServerSMO
    {
        public static ServerConnection ServerConnectionSMOObj;
        public static Server ServerSMOObj;
    }
}
