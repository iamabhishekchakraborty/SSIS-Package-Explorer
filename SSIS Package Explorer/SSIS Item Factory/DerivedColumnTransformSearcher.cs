using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Derived Column Transform
    public class DerivedColumnTransformSearcher:ISSISItem
    {
        string currentItemType = "Derived Column Transform";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            IDTSComponentMetaData100 derivedColumnTransform = (IDTSComponentMetaData100)executable;

            currentItemName = (parentName != "") ? parentName + " -> " + derivedColumnTransform.Name : derivedColumnTransform.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            if (CommonUtility.CheckStringExistence(derivedColumnTransform.Name, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = derivedColumnTransform.ComponentClassID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(derivedColumnTransform.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = derivedColumnTransform.ComponentClassID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(SearchPattern(searchPattern, derivedColumnTransform, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, IDTSComponentMetaData100 component, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            try
            {
                foreach (IDTSOutput100 output in component.OutputCollection)
                {
                    foreach (IDTSOutputColumn100 column in output.OutputColumnCollection)
                    {
                        if (column.CustomPropertyCollection.Count > 1)
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


                            string columnPropertyValue = column.CustomPropertyCollection["FriendlyExpression"].Value.ToString();

                            if (CommonUtility.CheckStringExistence(columnPropertyValue, searchPattern))
                            {
                                PackageItem taskItem = new PackageItem();
                                taskItem.Type = currentItemType + " -> OutputColumn -> Expression";
                                taskItem.Name = currentItemName + " -> " + columnName;
                                taskItem.GUID = component.ComponentClassID;
                                taskItem.ConnectionName = "";
                                taskItem.FileName = packageData.FileName;
                                taskItem.InterfaceName = packageData.Name;
                                SSISItems.Add(taskItem);
                            }
                        }   
                    }
                }

                foreach (IDTSInput100 input in component.InputCollection)
                {
                    foreach (IDTSInputColumn100 column in input.InputColumnCollection)
                    {
                        if (column.CustomPropertyCollection.Count > 1)
                        {
                            string columnName = column.Name;

                            if (CommonUtility.CheckStringExistence(columnName, searchPattern))
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


                            string columnPropertyValue = column.CustomPropertyCollection["FriendlyExpression"].Value.ToString();

                            if (CommonUtility.CheckStringExistence(columnPropertyValue, searchPattern))
                            {
                                PackageItem taskItem = new PackageItem();
                                taskItem.Type = currentItemType + " -> InputColumn -> Expression";
                                taskItem.Name = currentItemName + " -> " + columnName;
                                taskItem.GUID = component.ComponentClassID;
                                taskItem.ConnectionName = "";
                                taskItem.FileName = packageData.FileName;
                                taskItem.InterfaceName = packageData.Name;
                                SSISItems.Add(taskItem);
                            }
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
