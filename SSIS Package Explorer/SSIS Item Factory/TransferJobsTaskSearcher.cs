using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.TransferJobsTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Transfer Jobs Task
    public class TransferJobsTaskSearcher:ISSISItem
    {
        string currentItemType = "Transfer Jobs Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            TransferJobsTask transferJobsTask = (TransferJobsTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, transferJobsTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, TransferJobsTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (!task.TransferAllJobs)
            {
                foreach (String item in task.JobsList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedJob";
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
