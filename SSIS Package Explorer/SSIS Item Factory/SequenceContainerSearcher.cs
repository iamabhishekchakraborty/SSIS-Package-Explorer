using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.IO;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Sequence Container
    public class SequenceContainerSearcher : ISSISItem
    {
        string currentItemType = "Sequence Container";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable sequenceContainer = (Executable)executable;

            List<PackageItem> packageItems = new List<PackageItem>();

            Sequence container = ((Sequence)sequenceContainer);
            Variables sequenceContainerVariables = container.Variables;
            
            currentItemName = (parentName != "") ? parentName + " -> " + container.Name : container.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            packageItems.AddRange(CommonUtility.SearchVariables(sequenceContainerVariables, searchPattern, packageData, currentItemName, currentItemType));
            packageItems.AddRange(CommonUtility.SearchProperties(container.Properties, searchPattern, container, packageData, currentItemName, currentItemType));

            Executables executables = container.Executables;

            //Search name of the container
            if (CommonUtility.CheckStringExistence(container.Name,searchPattern))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = currentItemType + " -> Name";
                taskNameItem.Name = currentItemName;
                taskNameItem.GUID = container.ID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                packageItems.Add(taskNameItem);
            }

            //Search description of the task
            if (CommonUtility.CheckStringExistence(container.Description, searchPattern))
            {
                PackageItem taskDescItem = new PackageItem();
                taskDescItem.Type = currentItemType + " -> Description";
                taskDescItem.Name = currentItemName;
                taskDescItem.GUID = container.ID;
                taskDescItem.InterfaceName = packageData.Name;
                taskDescItem.FileName = packageData.FileName;
                packageItems.Add(taskDescItem);
            }

            //Search in the connectors between tasks
            if (executables.Count > 0)
            {
                packageItems.AddRange(CommonUtility.SearchPrecedenceConstraints(container.PrecedenceConstraints, searchPattern, packageData, currentItemName));
            }

            //Search in each task inside container
            foreach (Executable exec in executables)
            {
                SearchContainerItems(searchPattern, exec, packageItems,packageData);
            }

            return packageItems;
        }

        private void SearchContainerItems(string searchPattren, Executable executable, List<PackageItem> packageItems, PackageInfo packageData)
        {
            ISSISItem item = SSISItemFactory.GetObject(executable);

            if (item != null)
            {
                CommonUtility.CheckDuplicatesAndAdd(packageItems, item.Search(searchPattren, executable, packageData, currentItemName, currentItemType));
            }
        }
    }
}
