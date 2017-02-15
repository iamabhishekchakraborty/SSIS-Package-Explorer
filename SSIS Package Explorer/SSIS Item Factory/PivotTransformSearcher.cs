﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Pivot Transform
    public class PivotTransformSearcher:ISSISItem
    {
        string currentItemType = "Pivot Transform";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            IDTSComponentMetaData100 pivotTransform = (IDTSComponentMetaData100)executable;

            currentItemName = (parentName != "") ? parentName + " -> " + pivotTransform.Name : pivotTransform.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            if (CommonUtility.CheckStringExistence(pivotTransform.Name, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = pivotTransform.ComponentClassID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(pivotTransform.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = pivotTransform.ComponentClassID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(SearchPattern(searchPattern, pivotTransform, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, IDTSComponentMetaData100 component, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            try
            {
                //Searching Input Data
                foreach (IDTSInput100 input in component.InputCollection)
                {
                    if (CommonUtility.CheckStringExistence(input.Name, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> Input Name";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }

                    if (CommonUtility.CheckStringExistence(input.Description, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> Input Description";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }

                    foreach (IDTSInputColumn100 column in input.InputColumnCollection)
                    {
                        if (CommonUtility.CheckStringExistence(column.Name, searchPattern))
                        {
                            PackageItem taskItem = new PackageItem();
                            taskItem.Type = currentItemType + " -> InputColumnName";
                            taskItem.Name = currentItemName;
                            taskItem.GUID = component.ComponentClassID;
                            taskItem.ConnectionName = "";
                            taskItem.FileName = packageData.FileName;
                            taskItem.InterfaceName = packageData.Name;
                            SSISItems.Add(taskItem);
                        }

                        if (CommonUtility.CheckStringExistence(column.Description, searchPattern))
                        {
                            PackageItem taskItem = new PackageItem();
                            taskItem.Type = currentItemType + " -> InputColumnDescription";
                            taskItem.Name = currentItemName;
                            taskItem.GUID = component.ComponentClassID;
                            taskItem.ConnectionName = "";
                            taskItem.FileName = packageData.FileName;
                            taskItem.InterfaceName = packageData.Name;
                            SSISItems.Add(taskItem);
                        }
                    }
                }

                //Searching output data.
                foreach (IDTSOutput100 output in component.OutputCollection)
                {
                    if (CommonUtility.CheckStringExistence(output.Name, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> Output Name";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }

                    if (CommonUtility.CheckStringExistence(output.Description, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> Output Description";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }

                    foreach (IDTSOutputColumn100 column in output.OutputColumnCollection)
                    {
                        if (CommonUtility.CheckStringExistence(column.Name, searchPattern))
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

                        if (CommonUtility.CheckStringExistence(column.Description, searchPattern))
                        {
                            PackageItem taskItem = new PackageItem();
                            taskItem.Type = currentItemType + " -> OutputColumnDescription";
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
