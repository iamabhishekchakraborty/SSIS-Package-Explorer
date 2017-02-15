using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.XMLTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //XML Task
    public class XMLTaskSearcher:ISSISItem
    {
        string currentItemType = "XML Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;
            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            XMLTask XMLTask = (XMLTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, XMLTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, XMLTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (task.SourceType == DTSXMLSourceType.Variable)
            {
                if (CommonUtility.CheckStringExistence(task.Source, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> XMLSourceVariable";
                    taskNameItem.Name = currentItemName;
                    taskNameItem.GUID = hostedTask.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    SSISItems.Add(taskNameItem);
                }
            }
            else if (task.SourceType == DTSXMLSourceType.DirectInput)
            {
                if (CommonUtility.CheckStringExistence(task.Source, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> XMLSource";
                    taskNameItem.Name = currentItemName;
                    taskNameItem.GUID = hostedTask.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    SSISItems.Add(taskNameItem);
                }
            }

            if (task.SecondOperandType == DTSXMLSourceType.DirectInput)
            {
                if (CommonUtility.CheckStringExistence(task.SecondOperand, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> XMLSecondOperand";
                    taskNameItem.Name = currentItemName;
                    taskNameItem.GUID = hostedTask.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    SSISItems.Add(taskNameItem);
                }
            }
            else if (task.SecondOperandType == DTSXMLSourceType.Variable)
            {
                if (CommonUtility.CheckStringExistence(task.SecondOperand, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = currentItemType + " -> XMLSecondOperandVariable";
                    taskNameItem.Name = currentItemName;
                    taskNameItem.GUID = hostedTask.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    SSISItems.Add(taskNameItem);
                }
            }

            if (task.SaveOperationResult)
            {
                if (task.DestinationType == DTSXMLSaveResultTo.Variable)
                {
                    if (CommonUtility.CheckStringExistence(task.Destination, searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> XMLDestinationVariable";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
                else if (task.SecondOperandType == DTSXMLSourceType.Variable)
                {
                    if (CommonUtility.CheckStringExistence(task.Destination, searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> XMLDestination";
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
