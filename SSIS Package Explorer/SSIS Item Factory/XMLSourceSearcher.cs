using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //XML Source
    public class XMLSourceSearcher:ISSISItem
    {
        string accessModeDataFromFile = "0";
        string accessModeFileNameVariable = "1";
        string accessModeDataVariable = "2";

        string currentItemType = "XML Source";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            IDTSComponentMetaData100 XMLSource = (IDTSComponentMetaData100)executable;

            currentItemName = (parentName != "") ? parentName + " -> " + XMLSource.Name : XMLSource.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            if (CommonUtility.CheckStringExistence(XMLSource.Name, searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = XMLSource.ComponentClassID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                SSISItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(XMLSource.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = XMLSource.ComponentClassID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                SSISItems.Add(taskDescItem);
            }

            SSISItems.AddRange(SearchPattern(searchPattern, XMLSource, packageData));

            return SSISItems;
        }

        private List<PackageItem> SearchPattern(string searchPattern, IDTSComponentMetaData100 component, PackageInfo packageData)
        {
            List<PackageItem> SSISItems = new List<PackageItem>();

            try
            {
                string accessMode = component.CustomPropertyCollection["AccessMode"].Value.ToString();
                string useInlineSchema = component.CustomPropertyCollection["UseInlineSchema"].Value.ToString();

                if (accessMode == accessModeDataFromFile)
                {
                    string XMLFileLocation = component.CustomPropertyCollection["XMLData"].Value.ToString();

                    if (CommonUtility.CheckStringExistence(XMLFileLocation, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> XMLFileLocation";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }
                }
                else if (accessMode == accessModeFileNameVariable)
                {
                    string XMLFileLocationVariable = component.CustomPropertyCollection["XMLDataVariable"].Value.ToString();

                    if (CommonUtility.CheckStringExistence(XMLFileLocationVariable, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> XMLFileLocationVariable";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }
                }
                else if (accessMode == accessModeDataVariable)
                {
                    string XMLDataInVariable = component.CustomPropertyCollection["XMLDataVariable"].Value.ToString();

                    if (CommonUtility.CheckStringExistence(XMLDataInVariable, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> XMLDataInVariable";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }
                }

                if (useInlineSchema == "false")
                {
                    string XSDLocation = component.CustomPropertyCollection["XMLSchemaDefinition"].Value.ToString();

                    if (CommonUtility.CheckStringExistence(XSDLocation, searchPattern))
                    {
                        PackageItem taskItem = new PackageItem();
                        taskItem.Type = currentItemType + " -> XSD Location";
                        taskItem.Name = currentItemName;
                        taskItem.GUID = component.ComponentClassID;
                        taskItem.ConnectionName = "";
                        taskItem.FileName = packageData.FileName;
                        taskItem.InterfaceName = packageData.Name;
                        SSISItems.Add(taskItem);
                    }
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
