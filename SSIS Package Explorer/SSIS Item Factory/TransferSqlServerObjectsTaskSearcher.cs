using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.TransferSqlServerObjectsTask;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Transfer SQL Server Objects Task
    public class TransferSqlServerObjectsTaskSearcher : ISSISItem
    {
        string currentItemType = "Transfer SQL Server Objects Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            TransferSqlServerObjectsTask transferSqlServerObjectsTask = (TransferSqlServerObjectsTask)hostedTask.InnerObject;

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

            SSISItems.AddRange(SearchPattern(searchPattern, transferSqlServerObjectsTask, hostedTask, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, TransferSqlServerObjectsTask task, TaskHost hostedTask, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            if (CommonUtility.CheckStringExistence(task.SourceDatabase, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> SourceDatabase";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = hostedTask.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            if (CommonUtility.CheckStringExistence(task.DestinationDatabase, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> DestinationDatabase";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = hostedTask.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            if (!task.CopyAllTables)
            {
                foreach (String item in task.TablesList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedTable";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllViews)
            {
                foreach (String item in task.ViewsList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedView";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllStoredProcedures)
            {
                foreach (String item in task.StoredProceduresList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedStoredProcedure";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllUserDefinedFunctions)
            {
                foreach (String item in task.UserDefinedFunctionsList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedUserDefinedFunction";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllDefaults)
            {
                foreach (String item in task.DefaultsList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedDefault";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllUserDefinedDataTypes)
            {
                foreach (String item in task.UserDefinedDataTypesList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedUserDefinedDataType";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllPartitionFunctions)
            {
                foreach (String item in task.PartitionFunctionsList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedPartitionFunction";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllPartitionSchemes)
            {
                foreach (String item in task.PartitionSchemesList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedPartitionScheme";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllSchemas)
            {
                foreach (String item in task.SchemasList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedSchema";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllSqlAssemblies)
            {
                foreach (String item in task.SqlAssembliesList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedSqlAssemblie";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllUserDefinedAggregates)
            {
                foreach (String item in task.UserDefinedAggregatesList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedUserDefinedAggregate";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllUserDefinedTypes)
            {
                foreach (String item in task.UserDefinedTypesList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedUserDefinedType";
                        taskNameItem.Name = currentItemName;
                        taskNameItem.GUID = hostedTask.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        SSISItems.Add(taskNameItem);
                    }
                }
            }

            if (!task.CopyAllXmlSchemaCollections)
            {
                foreach (String item in task.XmlSchemaCollectionsList)
                {
                    if (CommonUtility.CheckStringExistence(item.ToString(), searchPattern))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = currentItemType + " -> SelectedXmlSchemaCollection";
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
