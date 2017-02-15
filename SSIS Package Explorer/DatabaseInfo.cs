using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSIS_Package_Reader
{
    public static class ConnectionInfo
    {
        public static string ServerName;
        public static string UserName;
        public static CommonEnum.AuthenticationMode Authentication;
        public static string Password;
        public static DatabaseList Databases = new DatabaseList();
    }

    public class DatabaseInfo
    {
        public string Name { get; set; }
        public override string ToString() { return Name; }
    }

    /// <summary>
    /// A list of "DatabaseInfo". 
    /// This represents the custom "IList" datasource of anything listed in a CheckBoxComboBox.
    /// </summary>
    public class DatabaseList : List<DatabaseInfo>
    {
    }
}
