using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Management.DatabaseMaintenance;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Rebuild Index Task
    public class DbMaintenanceReindexTaskSearcher:ISSISItem
    {
        string currentItemType = "Rebuild Index Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            DbMaintenanceReindexTask DbMaintenanceReindexTask = (DbMaintenanceReindexTask)hostedTask.InnerObject;

            currentItemName = (parentName != "") ? parentName + " -> " + hostedTask.Name : hostedTask.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            //Search name of the container
            if (CommonUtility.CheckStringExistence(hostedTask.Name, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = hostedTask.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(hostedTask.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = hostedTask.ID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(CommonUtility.SearchVariables(hostedTask.Variables, searchPattern, packageData, currentItemName, currentItemType));
            SSISItems.AddRange(CommonUtility.SearchProperties(hostedTask.Properties, searchPattern, hostedTask, packageData, currentItemName, currentItemType));

            SSISItems.AddRange(SearchPattern(searchPattern, DbMaintenanceReindexTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, DbMaintenanceReindexTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (CommonUtility.CheckStringExistence(task.SqlStatementSource, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> T-SQL Statement";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = hostedTask.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            if (CommonUtility.CheckStringExistence(task.Description[1].ToString().Replace("Databases: ",""), searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Selected Databases";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = hostedTask.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            return SSISItems;
        }
    }
}
