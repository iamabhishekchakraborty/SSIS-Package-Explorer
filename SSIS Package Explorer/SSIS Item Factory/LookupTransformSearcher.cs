using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Lookup Transform
    public class LookupTransformSearcher:ISSISItem
    {
        string currentItemType = "Lookup Transform";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            IDTSComponentMetaData100 lookupTransform = (IDTSComponentMetaData100)executable;

            currentItemName = (parentName != "") ? parentName + " -> " + lookupTransform.Name : lookupTransform.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            if (CommonUtility.CheckStringExistence(lookupTransform.Name, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = lookupTransform.ComponentClassID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(lookupTransform.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = lookupTransform.ComponentClassID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(SearchPattern(searchPattern, lookupTransform, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, IDTSComponentMetaData100 component, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            try
            {
                var propertyValue = component.CustomPropertyCollection["SqlCommand"].Value == null ? "" : component.CustomPropertyCollection["SqlCommand"].Value.ToString();
                string sqlCommand = propertyValue;

                if (CommonUtility.CheckStringExistence(sqlCommand, searchPattern))
                {
                    PackageItem taskItem = new PackageItem();
                    taskItem.Type = currentItemType + " -> SqlCommand/Table or View";
                    taskItem.Name = currentItemName;
                    taskItem.GUID = component.ComponentClassID;
                    taskItem.ConnectionName = "";
                    taskItem.FileName = packageData.FileName;
                    taskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(taskItem);
                }

                propertyValue = component.CustomPropertyCollection["SqlCommandParam"].Value == null ? "" : component.CustomPropertyCollection["SqlCommandParam"].Value.ToString();
                string customQuery = propertyValue;

                if (CommonUtility.CheckStringExistence(customQuery, searchPattern))
                {
                    PackageItem taskItem = new PackageItem();
                    taskItem.Type = currentItemType + " -> Advanced (Custom Query)";
                    taskItem.Name = currentItemName;
                    taskItem.GUID = component.ComponentClassID;
                    taskItem.ConnectionName = "";
                    taskItem.FileName = packageData.FileName;
                    taskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(taskItem);
                }

                foreach (IDTSInput100 input in component.InputCollection)
                {
                    foreach (IDTSInputColumn100 column in input.InputColumnCollection)
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
