﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    public class OLEDBSourceSearcher:ISSISItem
    {
        string accessModeTableOrView = "0";
        string accessModeTableOrViewVariable = "1";
        string accessModeSQLCommand = "2";
        string accessModeSQLCommandVariable = "3";

        string currentItemType = "OLEDB Source";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            IDTSComponentMetaData100 OLEDBSource = (IDTSComponentMetaData100)executable;

            currentItemName = (parentName != "") ? parentName + " -> " + OLEDBSource.Name : OLEDBSource.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            if (CommonUtility.CheckStringExistence(OLEDBSource.Name, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = OLEDBSource.ComponentClassID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(OLEDBSource.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = OLEDBSource.ComponentClassID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(SearchPattern(searchPattern, OLEDBSource, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, IDTSComponentMetaData100 component, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            try
            {
                string accessMode = component.CustomPropertyCollection["AccessMode"].Value.ToString();

                if (accessMode == accessModeTableOrView)
                {
                    string tableORViewName = component.CustomPropertyCollection["OpenRowset"].Value.ToString();

                    if (CommonUtility.CheckStringExistence(tableORViewName, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> TableOrViewName";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }
                }
                else if (accessMode == accessModeTableOrViewVariable)
                {
                    string tableORViewVariableName = component.CustomPropertyCollection["OpenRowsetVariable"].Value.ToString();

                    if (CommonUtility.CheckStringExistence(tableORViewVariableName, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> TableOrViewName Variable";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }
                }
                else if (accessMode == accessModeSQLCommand)
                {
                    string sqlCommand = component.CustomPropertyCollection["SqlCommand"].Value.ToString();

                    if (CommonUtility.CheckStringExistence(sqlCommand, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> SQL Command";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }
                }
                else if (accessMode == accessModeSQLCommandVariable)
                {
                    string sqlCommand = component.CustomPropertyCollection["SqlCommandVariable"].Value.ToString();

                    if (CommonUtility.CheckStringExistence(sqlCommand, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> SQL Command Variable";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }
                }

                string parameters = component.CustomPropertyCollection["ParameterMapping"].Value.ToString();

                if (CommonUtility.CheckStringExistence(parameters, searchPattern))
                {
                    PackageItem taskItem = new PackageItem();
                    taskItem.Type = currentItemType + " -> SQL Command -> Parameters";
                    taskItem.Name = currentItemName;
                    taskItem.GUID = component.ComponentClassID;
                    taskItem.ConnectionName = "";
                    taskItem.FileName = packageData.FileName;
                    taskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(taskItem);
                }

                foreach (IDTSOutput100 output in component.OutputCollection)
                {
                    foreach (IDTSOutputColumn100 column in output.OutputColumnCollection)
                    {
                        string columnName = column.Name;
                        if (CommonUtility.CheckStringExistence(columnName, searchPattern))
                        {
                            PackageItem taskItem = new PackageItem();
                            taskItem.Type = currentItemType + " -> OutputColumnName";
                            taskItem.Name = currentItemName;
                            taskItem.GUID = component.ComponentClassID;
                            taskItem.ConnectionName = "";
                            taskItem.FileName = packageData.FileName;
                            taskItem.InterfaceName = packageData.Name;
                            SSISItems.Add(taskItem);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return SSISItems;
        }
    }
}
