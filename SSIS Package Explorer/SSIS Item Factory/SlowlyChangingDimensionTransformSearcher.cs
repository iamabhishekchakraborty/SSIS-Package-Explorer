using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Slowly Changing Dimension Transform
    public class SlowlyChangingDimensionTransformSearcher:ISSISItem
    {
        string currentItemType = "Slowly Changing Dimension Transform";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            IDTSComponentMetaData100 slowlyChangingDimensionTransform = (IDTSComponentMetaData100)executable;

            currentItemName = (parentName != "") ? parentName + " -> " + slowlyChangingDimensionTransform.Name : slowlyChangingDimensionTransform.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            if (CommonUtility.CheckStringExistence(slowlyChangingDimensionTransform.Name, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = slowlyChangingDimensionTransform.ComponentClassID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(slowlyChangingDimensionTransform.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = slowlyChangingDimensionTransform.ComponentClassID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(SearchPattern(searchPattern, slowlyChangingDimensionTransform, packageData));

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
                    taskItem.Type = currentItemType + " -> SqlCommand";
                    taskItem.Name = currentItemName;
                    taskItem.GUID = component.ComponentClassID;
                    taskItem.ConnectionName = "";
                    taskItem.FileName = packageData.FileName;
                    taskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(taskItem);
                }

                propertyValue = component.CustomPropertyCollection["CurrentRowWhere"].Value == null ? "" : component.CustomPropertyCollection["CurrentRowWhere"].Value.ToString();
                string currentRowWhere = propertyValue;

                if (CommonUtility.CheckStringExistence(currentRowWhere, searchPattern))
                {
                    PackageItem taskItem = new PackageItem();
                    taskItem.Type = currentItemType + " -> CurrentRowWhere";
                    taskItem.Name = currentItemName;
                    taskItem.GUID = component.ComponentClassID;
                    taskItem.ConnectionName = "";
                    taskItem.FileName = packageData.FileName;
                    taskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(taskItem);
                }

                propertyValue = component.CustomPropertyCollection["InferredMemberIndicator"].Value == null ? "" : component.CustomPropertyCollection["InferredMemberIndicator"].Value.ToString();
                string inferredMemberIndicator = propertyValue;

                if (CommonUtility.CheckStringExistence(inferredMemberIndicator, searchPattern))
                {
                    PackageItem taskItem = new PackageItem();
                    taskItem.Type = currentItemType + " -> InferredMemberIndicator";
                    taskItem.Name = currentItemName;
                    taskItem.GUID = component.ComponentClassID;
                    taskItem.ConnectionName = "";
                    taskItem.FileName = packageData.FileName;
                    taskItem.InterfaceName = packageData.Name;
                    SSISItems.Add(taskItem);
                }
            }
            catch (Exception ex)
            {

            }

            return SSISItems;
        }
    }
}
