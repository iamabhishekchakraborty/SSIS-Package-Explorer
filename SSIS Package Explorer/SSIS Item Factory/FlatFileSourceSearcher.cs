using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Flat File Source
    public class FlatFileSourceSearcher:ISSISItem
    {
        string currentItemType = "Flat File Source";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            IDTSComponentMetaData100 flatFileSource = (IDTSComponentMetaData100)executable;

            currentItemName = (parentName != "") ? parentName + " -> " + flatFileSource.Name : flatFileSource.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            //Search name of the container

            if (CommonUtility.CheckStringExistence(flatFileSource.Name, searchPattern))
            {
                PackageItem taskItem = new PackageItem();
                taskItem.Type = currentItemType + " -> Name";
                taskItem.Name = currentItemName;
                taskItem.GUID = flatFileSource.ComponentClassID;
                taskItem.InterfaceName = packageData.Name;
                taskItem.FileName = packageData.FileName;
                
                SSISItems.Add(taskItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(flatFileSource.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = flatFileSource.ComponentClassID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(SearchPattern(searchPattern, flatFileSource, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, IDTSComponentMetaData100 component, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            try
            {
                var propertyValue = component.CustomPropertyCollection["FileNameColumnName"].Value == null ? "" : component.CustomPropertyCollection["FileNameColumnName"].Value.ToString();
                string fileName = propertyValue;

                if (CommonUtility.CheckStringExistence(fileName, searchPattern))
                {
                    PackageItem taskItem = new PackageItem();
                    taskItem.Type = currentItemType + " -> FileNameColumnName";
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
