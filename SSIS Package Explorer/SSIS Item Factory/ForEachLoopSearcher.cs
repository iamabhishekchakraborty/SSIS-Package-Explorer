using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    //Foreach Loop Container
    public class ForEachLoopSearcher:ISSISItem
    {
        string currentItemType = "Foreach Loop Container";
        string currentItemName = "";

        public List<PackageItem> Search(string searchPattern, object executable, PackageInfo packageData, string parentName = "", string parentType = "")
        {
            Executable forEachLoopContainer = (Executable)executable;

            List<PackageItem> packageItems = new List<PackageItem>();

            ForEachLoop container = ((ForEachLoop)forEachLoopContainer);
            Variables variables = container.Variables;

            currentItemName = (parentName != "") ? parentName + " -> " + container.Name : container.Name;
            currentItemType = (parentType != "") ? parentType + " -> " + currentItemType : currentItemType;

            packageItems.AddRange(CommonUtility.SearchVariables(variables, searchPattern, packageData, currentItemName, currentItemType));
            packageItems.AddRange(CommonUtility.SearchProperties(container.Properties, searchPattern, container, packageData, currentItemName, currentItemType));

            Executables executables = container.Executables;

            //Search name of the container
            if (CommonUtility.CheckStringExistence(container.Name, searchPattern))
            {
                PackageItem variablePackageItem = new PackageItem();
                variablePackageItem.Type = currentItemType + " -> Name";
                variablePackageItem.Name = currentItemName;
                variablePackageItem.GUID = container.ID;
                variablePackageItem.InterfaceName = packageData.Name;
                variablePackageItem.FileName = packageData.FileName;
                packageItems.Add(variablePackageItem);
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
                SearchContainerItems(searchPattern, exec, packageItems, packageData);
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
