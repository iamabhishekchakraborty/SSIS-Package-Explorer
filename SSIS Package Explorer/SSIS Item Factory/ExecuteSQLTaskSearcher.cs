using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.IO;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;
using Microsoft.SqlServer.Dts.Tasks;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Execute SQL Task
    public class ExecuteSQLTaskSearcher: ISSISItem
    {
        string currentItemType = "Execute SQL Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            ExecuteSQLTask executeSQLTask = (ExecuteSQLTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, executeSQLTask, hostedTask, packageData));
           
            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, ExecuteSQLTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (task.SqlStatementSourceType == SqlStatementSourceType.DirectInput)
            {
                if (CommonUtility.CheckStringExistence(task.SqlStatementSource, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> SQLStatement";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.Connection;
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);   
                }
            }

            foreach (IDTSResultBinding resultBinding in task.ResultSetBindings)
            {
                string variableName = resultBinding.DtsVariableName == null ? "" : resultBinding.DtsVariableName;
                string resultName = resultBinding.ResultName == null ? "" : resultBinding.ResultName.ToString();

                if (CommonUtility.CheckStringExistence(variableName, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> Result Set Binding Variable";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.Connection;
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }

                if (CommonUtility.CheckStringExistence(resultName, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> Result Set Binding Name";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.Connection;
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }

            foreach (IDTSParameterBinding parameterBinding in task.ParameterBindings)
            {
                string variableName = parameterBinding.DtsVariableName == null ? "" : parameterBinding.DtsVariableName;
                string parameterName = parameterBinding.ParameterName == null ? "" : parameterBinding.ParameterName.ToString();

                if (CommonUtility.CheckStringExistence(variableName, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> Parameter Mapping Variable";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.Connection;
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }

                if (CommonUtility.CheckStringExistence(parameterName, searchPattern))
                {
                    PackageItem executeSQLTaskItem = new PackageItem();
                    executeSQLTaskItem.Type = currentItemType + " -> Parameter Mapped";
                    executeSQLTaskItem.Name = currentItemName;
                    executeSQLTaskItem.GUID = hostedTask.ID;
                    executeSQLTaskItem.ConnectionName = task.Connection;
                    executeSQLTaskItem.FileName = packageData.FileName;
                    executeSQLTaskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(executeSQLTaskItem);
                }
            }

            return SSISItems;
        }
    }
}
