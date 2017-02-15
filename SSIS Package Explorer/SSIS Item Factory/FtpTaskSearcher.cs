using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.FtpTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //FTP Task
    public class FtpTaskSearcher:ISSISItem
    {
        string currentItemType = "FTP Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            FtpTask ftpTask = (FtpTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, ftpTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, FtpTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (task.IsLocalPathVariable)
            {
                if (CommonUtility.CheckStringExistence(task.LocalPath, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> LocalPathVariable";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = "";
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }
            else
            {
                if (CommonUtility.CheckStringExistence(task.LocalPath, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> LocalPath";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = "";
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }
            
            if (CommonUtility.CheckStringExistence(task.OperationName, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> OperationName";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = "";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (task.IsRemotePathVariable)
            {
                if (CommonUtility.CheckStringExistence(task.RemotePath, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> RemotePathVariable";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = "";
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }
            else
            {
                if (CommonUtility.CheckStringExistence(task.RemotePath, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> RemotePath";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = "";
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }
            
            return SSISItems;
        }
    }
}
