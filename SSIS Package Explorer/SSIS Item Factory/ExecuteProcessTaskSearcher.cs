using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ExecuteProcess;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Execute Process Task
    public class ExecuteProcessTaskSearcher:ISSISItem
    {
        string currentItemType = "Execute Process Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            ExecuteProcess executeProcessTask = (ExecuteProcess)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, executeProcessTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, ExecuteProcess task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (CommonUtility.CheckStringExistence(task.Executable, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> Executable";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = "";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.Arguments, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> Arguments";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = "";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.WorkingDirectory, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> WorkingDirectory";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = "";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.StandardInputVariable, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> StandardInputVariable";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = "";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.StandardOutputVariable, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> StandardOutputVariable";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = "";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.StandardErrorVariable, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> StandardErrorVariable";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = "";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            return SSISItems;
        }
    }
}
