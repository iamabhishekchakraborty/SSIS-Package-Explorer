using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.DMQueryTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Data Mining Query Task
    public class DMQueryTaskSearcher:ISSISItem
    {
        string currentItemType = "Data Mining Query Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            DMQueryTask DMQueryTask = (DMQueryTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, DMQueryTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, DMQueryTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (CommonUtility.CheckStringExistence(task.ModelName, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> MiningModelName";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = task.InputConnection+"(Input Connection), "+task.InputConnection +" (Output Connection)";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.ModelStructureName, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> ModelStructureName";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.OutputTableName, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> OutputTableName";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.QueryBuilderQueryString, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> QueryBuilderQueryString";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.QueryBuilderSpecification, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> QueryBuilderSpecification";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            if (CommonUtility.CheckStringExistence(task.QueryString, searchPattern))
            {
                PackageItem executeSQLTaskItem = new PackageItem();
                executeSQLTaskItem.Type = currentItemType + " -> QueryString";
                executeSQLTaskItem.Name = currentItemName;
                executeSQLTaskItem.GUID = hostedTask.ID;
                executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                executeSQLTaskItem.FileName = packageData.FileName;
                executeSQLTaskItem.InterfaceName = packageData.Name;
                SSISItems.Add(executeSQLTaskItem);
            }

            foreach (InputParameter inputparam in task.InputParameters)
            {
                if (CommonUtility.CheckStringExistence(inputparam.ParameterName, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> Query Parameter Name";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }

                if (CommonUtility.CheckStringExistence(inputparam.DtsVariableName, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> Query Parameter Variable";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }

            foreach (ResultParameter resultparam in task.ResultParameters)
            {
                if (CommonUtility.CheckStringExistence(resultparam.ResultName, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> Query Result Name";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }

                if (CommonUtility.CheckStringExistence(resultparam.DtsVariableName, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> Query Result Variable";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.InputConnection + "(Input Connection), " + task.InputConnection + " (Output Connection)";
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }

            return SSISItems;
        }

    }
}
