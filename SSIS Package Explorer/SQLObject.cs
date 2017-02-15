using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace SSIS_Package_Reader
{
    [Serializable]
    public class SQLObject
    {
        public string ObjectName { get; set; }
        public string SchemaName { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }
        public string ShortType;
        public string ObjectType { get; set; }
        public string Definition;
        public string Property { get; set; }
        public string ButtonText { get { return "Preview"; } }
    }
}
