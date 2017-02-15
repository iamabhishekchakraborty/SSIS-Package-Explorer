using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Tasks.TransferLoginsTask;
using Microsoft.SqlServer.Dts.Runtime;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Transfer Logins Task
    public class TransferLoginsTaskSearcher:ISSISItem
    {
        string currentItemType = "Transfer Logins Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;
            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            TransferLoginsTask transferLoginsTask = (TransferLoginsTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, transferLoginsTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, TransferLoginsTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (task.LoginsToTransfer == LoginsToTransfer.SelectedLogins)
            {
                foreach (String item in task.LoginsList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedLogin";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (task.LoginsToTransfer == LoginsToTransfer.AllLoginsFromSelectedDatabases)
            {
                foreach (String item in task.DatabasesList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedDatabase";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }
            
            return SSISItems;
        }
    }
}
