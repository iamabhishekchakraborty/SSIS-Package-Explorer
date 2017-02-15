using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Dts.Runtime;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    public static class CommonUtility
    {
        public static List<PackageItem> SearchVariables(Variables variables, string searchPattern, PackageInfo packageData, string parentItemName, string parentItemType)
        {
            List<PackageItem> packageItemsFound = new List<PackageItem>();

            foreach (Variable variable in variables)
            {
                if (CheckStringExistence(variable.Name, searchPattern) && !variable.SystemVariable)
                {
                    PackageItem variablePackageItem = new PackageItem();
                    variablePackageItem.Type = parentItemType + " -> Variable -> Name";
                    variablePackageItem.Name = parentItemName + " -> " + variable.Name;
                    variablePackageItem.GUID = variable.ID;
                    variablePackageItem.InterfaceName = packageData.Name;
                    variablePackageItem.FileName = packageData.FileName;

                    if (!SearchPackageItems(variablePackageItem.Name, variablePackageItem.Type, packageItemsFound))
                    {
                        packageItemsFound.Add(variablePackageItem);
                    }
                }

                if (CheckStringExistence(variable.Value.ToString(), searchPattern) && !variable.SystemVariable)
                {
                    PackageItem variablePackageItem = new PackageItem();
                    variablePackageItem.Type = parentItemType + " -> Variable -> Value";
                    variablePackageItem.Name = parentItemName + " -> " + variable.Name;
                    variablePackageItem.GUID = variable.ID;
                    variablePackageItem.InterfaceName = packageData.Name;
                    variablePackageItem.FileName = packageData.FileName;

                    if (!SearchPackageItems(variablePackageItem.Name, variablePackageItem.Type, packageItemsFound))
                    {
                        packageItemsFound.Add(variablePackageItem);
                    }

                }

                if ((variable.Expression == null) ? false : CheckStringExistence(variable.Expression.ToString(), searchPattern))
                {
                    PackageItem variablePackageItem = new PackageItem();
                    variablePackageItem.Type = parentItemType + " -> Variable -> Expression";
                    variablePackageItem.Name = parentItemName + " -> " + variable.Name;
                    variablePackageItem.GUID = variable.ID;
                    variablePackageItem.InterfaceName = packageData.Name;
                    variablePackageItem.FileName = packageData.FileName;

                    if (!SearchPackageItems(variablePackageItem.Name, variablePackageItem.Type, packageItemsFound))
                    {
                        packageItemsFound.Add(variablePackageItem);
                    }
                }
            }

            return packageItemsFound;
        }

        private static bool SearchPackageItems(string variableName, string type, List<PackageItem> packageItemsFound)
        {
            var matchedPackageItems = packageItemsFound.Select(packageItem => packageItem.Name == variableName && packageItem.Type == type);

            return (matchedPackageItems.Count() > 0);
        }

        public static void CheckDuplicatesAndAdd(List<PackageItem> packageItemsFinal, List<PackageItem> packageItemsToInsert)
        {
            foreach (PackageItem item in packageItemsToInsert)
            {
                if (item.Type.IndexOf("Variable -> Value", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    if (!packageItemsFinal.Exists(packageItem => packageItem.GUID == item.GUID && packageItem.FileName == item.FileName &&
                         packageItem.Type.IndexOf("Variable -> Value", StringComparison.InvariantCultureIgnoreCase) > 0))
                    {
                        packageItemsFinal.Add(item);
                    }
                }
                else if (item.Type.IndexOf("Variable -> Expression", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    if (!packageItemsFinal.Exists(packageItem => packageItem.GUID == item.GUID && packageItem.FileName == item.FileName
                        && packageItem.Type.IndexOf("Variable -> Expression", StringComparison.InvariantCultureIgnoreCase) > 0))
                    {
                        packageItemsFinal.Add(item);
                    }
                }
                else if (item.Type.IndexOf("Variable -> Name", StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    if (!packageItemsFinal.Exists(packageItem => packageItem.GUID == item.GUID && packageItem.FileName == item.FileName
                        && packageItem.Type.IndexOf("Variable -> Name", StringComparison.InvariantCultureIgnoreCase) > 0))
                    {
                        packageItemsFinal.Add(item);
                    }
                }
                else //if (!packageItemsFinal.Exists(packageItem => packageItem.GUID == item.GUID && packageItem.Type == item.Type && packageItem.FileName == item.FileName))
                {
                    packageItemsFinal.Add(item);
                }
            }
        }

        public static List<PackageItem> SearchPrecedenceConstraints(PrecedenceConstraints precedenceConstraints, string searchPattern, PackageInfo packageData, string belongsTo)
        {
            List<PackageItem> packageItemsFinal = new List<PackageItem>();
            string itemDescrition = "Connector (" + belongsTo + " -> ";
            foreach (PrecedenceConstraint precedenceConstraint in precedenceConstraints)
            {
                PackageItem precedenceConstraintPackageItem = new PackageItem();
                precedenceConstraintPackageItem.Type = "Connector -> Expression";
                precedenceConstraintPackageItem.GUID = precedenceConstraint.ID;
                precedenceConstraintPackageItem.InterfaceName = packageData.Name;
                precedenceConstraintPackageItem.FileName = packageData.FileName;

                if (precedenceConstraint.Expression != null)
                {
                    if (CheckStringExistence(precedenceConstraint.Expression.ToString(), searchPattern))
                    {
                        itemDescrition += GetDescriptionByClassType(itemDescrition, precedenceConstraint.PrecedenceExecutable) + " <-> ";
                        itemDescrition += GetDescriptionByClassType(itemDescrition, precedenceConstraint.ConstrainedExecutable) + ")";

                        precedenceConstraintPackageItem.Name = itemDescrition;
                        packageItemsFinal.Add(precedenceConstraintPackageItem);
                    }
                }
            }

            return packageItemsFinal;
        }

        private static string GetDescriptionByClassType(string itemDescrition, object objectParticipated)
        {
            try
            {
                if (objectParticipated is TaskHost)
                {
                    itemDescrition = ((TaskHost)objectParticipated).Name;
                }
                else if (objectParticipated is Sequence)
                {
                    itemDescrition += ((Sequence)objectParticipated).Name;
                }
                else if (objectParticipated is ForLoop)
                {
                    itemDescrition += ((ForLoop)objectParticipated).Name;
                }
                else if (objectParticipated is ForEachLoop)
                {
                    itemDescrition += ((ForEachLoop)objectParticipated).Name;
                }
            }
            catch (Exception)
            {
                itemDescrition = "";
            }

            return itemDescrition;
        }

        public static bool CheckStringExistence(string sourceString, string subString)
        {
            sourceString = (sourceString == null) ? "" : sourceString;
            subString = (subString == null) ? "" : subString;
            
            //This is for a regular expression search
            /*
            if (Regex.Match((sourceString == null) ? "" : sourceString, (subString == null) ? "":subString, RegexOptions.IgnoreCase).Success)
            {
                return true;
            }
            */

            //if (sourceString.IndexOf("SEP_RetailStore", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //{
            //    //return true;
            //}

            if (SearchOptions.IsCaseSensitive)
            {
                if (SearchOptions.MatchSubstring)
                {
                    if (SearchOptions.ExpressionType == SearchOptions.ExpressionTypes.None)
                    {
                        return sourceString.Contains(subString);
                    }
                    else if (SearchOptions.ExpressionType == SearchOptions.ExpressionTypes.RegularExpression)
                    {
                        Match m = Regex.Match(sourceString, subString, RegexOptions.None);

                        if (m.Success)
                        {
                            return true;
                        }
                    }
                    else if (SearchOptions.ExpressionType == SearchOptions.ExpressionTypes.WildCard)
                    {
                        //The Like string operator provides string pattern matching in Visual Basic.
                        // * matches zero or more of any character.
                        // ? matches any single character.
                        // # matches any single digit.

                        //Match ranges of characters by enclosing the range in brackets:
                        //    [A-C] Matches any of A, B, or C
                        //    [ABC] Same as [A-C]
                        // Match characters not in a range by using !
                        //    [!A-Z]  Any character not including A-Z

                        bool IsMatched = Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString(sourceString, subString, CompareMethod.Binary);
                        return IsMatched;
                    }
                }
                else if (SearchOptions.MatchWholeWord)
                {
                    string pattern = @"(?<!\w)" + Regex.Escape(subString) + @"(?!\w)";

                    Match m = Regex.Match(sourceString, pattern, RegexOptions.None);

                    if (m.Success)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                if (SearchOptions.MatchSubstring)
                {
                    if (SearchOptions.ExpressionType == SearchOptions.ExpressionTypes.None)
                    {
                        //This is for substring search
                        if (sourceString.IndexOf(subString, StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            return true;
                        }
                        
                        //return (sourceString.IndexOf(subString, StringComparison.InvariantCultureIgnoreCase) >= 0);
                    }
                    else if (SearchOptions.ExpressionType == SearchOptions.ExpressionTypes.RegularExpression)
                    {
                        Match m = Regex.Match(sourceString, subString, RegexOptions.IgnoreCase);

                        if (m.Success)
                        {
                            return true;
                        }
                    }
                    else if (SearchOptions.ExpressionType == SearchOptions.ExpressionTypes.WildCard)
                    {
                        //The Like string operator provides string pattern matching in Visual Basic.
                        // * matches zero or more of any character.
                        // ? matches any single character.
                        // # matches any single digit.

                        //Match ranges of characters by enclosing the range in brackets:
                        //    [A-C] Matches any of A, B, or C
                        //    [ABC] Same as [A-C]
                        // Match characters not in a range by using !
                        //    [!A-Z]  Any character not including A-Z

                        bool IsMatched = Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString(sourceString, subString, CompareMethod.Text);
                        return IsMatched;
                    }
                }
                else if (SearchOptions.MatchWholeWord)
                {
                    string pattern = @"(?<!\w)" + Regex.Escape(subString) + @"(?!\w)";

                    Match m = Regex.Match(sourceString, pattern, RegexOptions.IgnoreCase);

                    if (m.Success)
                    {
                        return true;
                    }
                }
            }

            

            return false;
        }

        public static List<PackageItem> SearchProperties(DtsProperties properties, string searchPattern,object currentObj , PackageInfo packageData, string parentItemName, string parentItemType)
        {
            List<PackageItem> packageItemsFound = new List<PackageItem>();

            foreach (DtsProperty property in properties)
            {
                if (CommonUtility.CheckStringExistence(property.Name, searchPattern))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = parentItemType + " -> Property(Expression) -> Name";
                    taskNameItem.Name = parentItemName + " -> " + property.Name;
                    taskNameItem.GUID = "";
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    packageItemsFound.Add(taskNameItem);
                }

                string expression = property.GetExpression(currentObj);

                expression = expression == null ? "" : @expression.Replace("\\", "").Replace("\"", "").Replace("+", "");

                string patternToSearchModified;

                if (SearchOptions.ExpressionType == SearchOptions.ExpressionTypes.RegularExpression)
                {
                    patternToSearchModified = searchPattern;
                }
                else
                {
                    patternToSearchModified = @searchPattern.Replace("\\", "").Replace("\"", "").Replace("+", "");
                }

                if (CommonUtility.CheckStringExistence(expression, patternToSearchModified))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = parentItemType + " -> Property(Expression) -> Value";
                    taskNameItem.Name = parentItemName+" -> " + property.Name;
                    taskNameItem.GUID = "";
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    packageItemsFound.Add(taskNameItem);
                }
            }

            return packageItemsFound;
        }

    }
}
