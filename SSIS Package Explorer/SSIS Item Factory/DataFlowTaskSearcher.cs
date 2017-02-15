using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Pipeline;


namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Data Flow Task
    public class DataFlowTaskSearcher:ISSISItem
    {
        string currentItemType = "Data Flow Task";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable taskItem = (Executable)executable;

            List<PackageItem> SSISItems = new List<PackageItem>();

            TaskHost hostedTask = taskItem as TaskHost;
            MainPipe dataFlowTask = (MainPipe)hostedTask.InnerObject;

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

            foreach (IDTSComponentMetaData100 comp in dataFlowTask.ComponentMetaDataCollection)
            {
                SearchContainerItems(searchPattern, comp, SSISItems, packageData);
            }

            SSISItems.AddRange(CommonUtility.SearchVariables(hostedTask.Variables, searchPattern, packageData, currentItemName, currentItemType));
            SSISItems.AddRange(CommonUtility.SearchProperties(hostedTask.Properties, searchPattern, hostedTask, packageData, currentItemName, currentItemType));

            return SSISItems;
        }

        private void SearchContainerItems(string searchPattren, IDTSComponentMetaData100 component, List<PackageItem> packageItems, PackageInfo packageData)
        {
            ISSISItem item = SSISItemFactory.GetObject(component);

            if (item != null)
            {
                CommonUtility.CheckDuplicatesAndAdd(packageItems, item.Search(searchPattren, component, packageData, currentItemName, currentItemType));
            }
        }
    }
}
