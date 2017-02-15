using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.WebServiceTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Web Service Task
    public class WebServiceTaskSearcher:ISSISItem
    {
        string currentItemType = "Web Service Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            WebServiceTask webServiceTask = (WebServiceTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, webServiceTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, WebServiceTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (CommonUtility.CheckStringExistence(task.ServiceName, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> ServiceName";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = hostedTask.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            if (CommonUtility.CheckStringExistence(task.WsdlFile, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> WsdlFile";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = hostedTask.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            if (task.OutputType == DTSOutputType.Variable)
            {
                if (CommonUtility.CheckStringExistence(task.OutputLocation, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> OutputLocationVariable";
                    taskNameItem.Name = currentItemName;
                    taskNameItem.GUID = hostedTask.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    SSISItems.Add(taskNameItem);
                }
            }
            else
            {
                if (CommonUtility.CheckStringExistence(task.OutputLocation, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> OutputLocation";
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
