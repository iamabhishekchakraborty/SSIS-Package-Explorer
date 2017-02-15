using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.WmiDataReaderTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //WMI Data Reader Task
    public class WmiDataReaderTaskSearcher:ISSISItem
    {
        string currentItemType = "WMI Data Reader Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;
            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            WmiDataReaderTask wmiDataReaderTask = (WmiDataReaderTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(CommonUtility.SearchVariables(hostedTask.Variables, searchPattern, packageData, hostedTask.Name, currentItemType));
            SSISItems.AddRange(CommonUtility.SearchProperties(hostedTask.Properties, searchPattern, hostedTask, packageData, currentItemName, currentItemType));

            SSISItems.AddRange(SearchPattern(searchPattern, wmiDataReaderTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, WmiDataReaderTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (task.WqlQuerySourceType == QuerySourceType.Variable)
            {
                if (CommonUtility.CheckStringExistence(task.WqlQuerySource, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> WqlQuerySourceVariable";
                    taskNameItem.Name = currentItemName;
                    taskNameItem.GUID = hostedTask.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    SSISItems.Add(taskNameItem);
                }
            }
            else if (task.WqlQuerySourceType == QuerySourceType.DirectInput)
            {
                if (CommonUtility.CheckStringExistence(task.WqlQuerySource, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> WqlQuerySource";
                    taskNameItem.Name = currentItemName;
                    taskNameItem.GUID = hostedTask.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    SSISItems.Add(taskNameItem);
                }    
            }

            if (task.DestinationType == DestinationType.Variable)
            {
                if (CommonUtility.CheckStringExistence(task.Destination, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> DestinationVariable";
                    taskNameItem.Name = currentItemName;
                    taskNameItem.GUID = hostedTask.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    SSISItems.Add(taskNameItem);
                }
            }

            return SSISItems;
        }

    }
}
