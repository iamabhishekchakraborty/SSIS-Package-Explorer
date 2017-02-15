using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    public class FlatFileDestSearcher:ISSISItem
    {
        string currentItemType = "Flat File Destination";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            IDTSComponentMetaData100 flatFileDest = (IDTSComponentMetaData100)executable;

            currentItemName = (parentName != "") ? parentName + " -> " + flatFileDest.Name : flatFileDest.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            //Search name of the item
            if (CommonUtility.CheckStringExistence(flatFileDest.Name, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = flatFileDest.ComponentClassID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(flatFileDest.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = flatFileDest.ComponentClassID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(SearchPattern(searchPattern, flatFileDest, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, IDTSComponentMetaData100 component, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            try
            {
                string headerData = component.CustomPropertyCollection["Header"].Value.ToString();

                if (CommonUtility.CheckStringExistence(headerData, searchPattern))
                {
                    PackageItem taskItem = new PackageItem();
                    taskItem.Type = currentItemType + " -> Header";
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
