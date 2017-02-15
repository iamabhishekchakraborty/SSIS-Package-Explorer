using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.ScriptTask;
using Microsoft.SqlServer.VSTAHosting;
using System.Collections;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Script Task
    public class ScriptTaskSearcher:ISSISItem
    {
        string currentItemType = "Script Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable; 
            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            ScriptTask scriptTask = (ScriptTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, scriptTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, ScriptTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (task.ScriptStorage != null)
            {
                VSTAScriptProjectStorage projectFiles = task.ScriptStorage;

                foreach (DictionaryEntry item in projectFiles.ScriptFiles)
                {
                    if (!item.Key.ToString().StartsWith("\\properties") && item.Key.ToString().EndsWith(".cs"))
                    {
                        if (CommonUtility.CheckStringExistence(item.Value.ToString(), searchPattern))
                        {
                            PackageItem taskNameItem = new PackageItem();
                            taskNameItem.Type = currentItemType + " -> ScriptCode";
                            taskNameItem.Name = currentItemName + " -> " + @item.Key.ToString().Replace("\\", "");
                            taskNameItem.GUID = hostedTask.ID;
                            taskNameItem.InterfaceName = packageData.Name;
                            taskNameItem.FileName = packageData.FileName;
                            SSISItems.Add(taskNameItem);
                        }
                    }
                }
            }

            if (CommonUtility.CheckStringExistence(task.ReadOnlyVariables, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> ReadOnlyVariables";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = hostedTask.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            if (CommonUtility.CheckStringExistence(task.ReadWriteVariables, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> ReadWriteVariables";
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
