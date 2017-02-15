using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.DataTransformationServices.Tasks.DTSProcessingTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Code for Analysis Services Execute DDL Task
    public class ASExecuteDDLTaskSearcher: ISSISItem
    {
        string currentItemType = "Analysis Services Execute DDL Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            ASExecuteDDLTask ASExecuteDDLTask = (ASExecuteDDLTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, ASExecuteDDLTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, ASExecuteDDLTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (task.SourceType == DDLSourceType.DirectInput)
            {
                if (CommonUtility.CheckStringExistence(task.Source, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> DDL Statement";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.ConnectionName;
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }
            else if (task.SourceType == DDLSourceType.Variable)
            {
                if (CommonUtility.CheckStringExistence(task.Source, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> DDL Source Variable";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.ConnectionName;
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }

            return SSISItems;
        }
    }
}
