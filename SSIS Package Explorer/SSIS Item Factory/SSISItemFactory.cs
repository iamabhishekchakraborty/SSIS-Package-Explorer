using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Pipeline;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;
using Microsoft.SqlServer.Dts.Tasks;
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;
using Microsoft.DataTransformationServices.Tasks.DTSProcessingTask;
using Microsoft.SqlServer.Dts.Tasks.BulkInsertTask;
using Microsoft.SqlServer.Dts.Tasks.DMQueryTask;
using Microsoft.SqlServer.Dts.Tasks.DataProfilingTask;
using Microsoft.SqlServer.Dts.Tasks.ExecutePackageTask;
using Microsoft.SqlServer.Dts.Tasks.ExecuteProcess;
using Microsoft.SqlServer.Dts.Tasks.FileSystemTask;
using Microsoft.SqlServer.Dts.Tasks.FtpTask;
using Microsoft.SqlServer.Dts.Tasks.MessageQueueTask;
using Microsoft.SqlServer.Dts.Tasks.ScriptTask;
using Microsoft.SqlServer.Dts.Tasks.SendMailTask;
using Microsoft.SqlServer.Dts.Tasks.TransferDatabaseTask;
using Microsoft.SqlServer.Dts.Tasks.TransferErrorMessagesTask;
using Microsoft.SqlServer.Dts.Tasks.TransferJobsTask;
using Microsoft.SqlServer.Dts.Tasks.TransferLoginsTask;
using Microsoft.SqlServer.Dts.Tasks.TransferStoredProceduresTask;
using Microsoft.SqlServer.Dts.Tasks.TransferSqlServerObjectsTask;
using Microsoft.SqlServer.Dts.Tasks.WebServiceTask;
using Microsoft.SqlServer.Dts.Tasks.WmiDataReaderTask;
using Microsoft.SqlServer.Dts.Tasks.WmiEventWatcherTask;
using Microsoft.SqlServer.Dts.Tasks.XMLTask;
using Microsoft.SqlServer.Management.DatabaseMaintenance;

namespace SSIS_Package_Reader.SSIS_Item_Factory
{
    public class SSISItemFactory
    {
        //This is created for some tasks which are having same Class ID.
        const string COMMON_CLASSID = "{33D831DE-5DCF-48F0-B431-4D327B9E785D}";

        //All these need seperate processing because all of them have same ClassID
        const string ADO_NET_SOURCE_CLASSID = "{33D831DE-5DCF-48F0-B431-4D327B9E785D}"; //ADO .Net Source
        const string ADO_NET_DEST_CLASSID = "{33D831DE-5DCF-48F0-B431-4D327B9E785D}"; //ADO .Net Destination
        const string XML_SOURCE_CLASSID = "{33D831DE-5DCF-48F0-B431-4D327B9E785D}"; //XML Source
        const string SQLSERVER_COMPACT_DEST_CLASSID = "{33D831DE-5DCF-48F0-B431-4D327B9E785D}"; //SQL Server Compact Destination
        const string DATA_READER_DEST_CLASSID = "{33D831DE-5DCF-48F0-B431-4D327B9E785D}"; //DataReaderDest
        const string SCRIPT_COMP_CLASSID = "{33D831DE-5DCF-48F0-B431-4D327B9E785D}"; //Script Component Sub Task

        //Sources
        const string EXCEL_SOURCE_CLASSID = "{9F5C585F-2F02-4622-B273-F75D52419D4A}"; //Excel Source
        const string FLAT_FILE_SOURCE_CLASSID = "{C4D48377-EFD6-4C95-9A0B-049219453431}"; //Flat File Source
        const string OLE_DB_SOURCE_CLASSID = "{D1AA3237-CE07-40F9-AA7D-DB75A2552BAC}"; //OLE DB Source
        const string RAW_FILE_SOURCE_CLASSID = "{4B28FC70-0618-449E-81E7-C5B9E77D308D}"; //Raw File Source
        
        //Destinations
        const string DATAMINING_MODEL_CLASSID = "{1378724A-1297-4BB6-8D42-6D671B299037}";//Data Mining Model Training
        const string DIM_PROCESSING_CLASSID = "{924A5D7E-DC21-4AC9-AE3E-B87B43C61570}";//Dimension Processing
        const string EXCEL_DEST_CLASSID = "{90E2E609-1207-4CB0-A8CE-CC7B8CFE2510}";//Excel Destination
        const string FLAT_FILE_DEST_CLASSID = "{FD4FFB90-EECF-4B5A-A3A7-DE2E1FA8052C}";//Flat File Destination
        const string OLE_DB_DEST_CLASSID = "{63F60893-F24D-421A-A481-86C53A0ADBA8}";//OLE DB Destination
        const string PARTITION_PROC_CLASSID = "{9FF88729-AC02-4441-94C3-11B57655D23B}";//Partition Processing
        const string RAW_FILE_DEST_CLASSID = "{F84A449B-F8ED-483A-BD9F-9337E8CAD27E}"; //Raw File Destination
        const string RECORDSET_DEST_CLASSID = "{B11B6C93-0A23-43DB-9AAB-1228D0EE314C}"; //Recordset Destination
        const string SQL_SERV_DEST_CLASSID = "{E6CBD480-2E7E-4419-A475-07A015FA2FF6}"; //SQL Server Destination
        
        //Transformations
        const string AGG_CLASSID = "{5ADC3E0D-2722-4394-9195-0543C18AB53A}"; //Aggregate
        const string AUDIT_CLASSID = "{EFFA6B56-84CA-4314-B3C7-F8B576630156}"; //Audit
        const string CACHE_TRANSFORM_CLASSID = "{D9E9689B-7258-488F-B955-265F90D52ED8}"; //Cache Transform
        const string CHARACTER_MAP_CLASSID = "{B8706948-9D67-4E81-9C88-24EED2F1BC4A}"; //Character Map
        const string COND_SPLIT_CLASSID = "{6FA01478-82BB-40D8-B16F-690406F18AEE}"; //Conditional Split
        const string COPY_COLUMN_CLASSID = "{D308460C-4353-4DA6-A457-B41EE51769F6}"; //Copy Column
        const string DATA_CONV_CLASSID = "{A6616787-31C0-4890-BA71-3D47422F4454}"; //Data Conversion
        const string DATA_MINING_QUERY_CLASSID = "{63622C8E-BE4C-4981-954E-E54526FE38C4}"; //Data Mining Query
        const string DERIVED_COLUMN_CLASSID = "{18E9A11B-7393-47C5-9D47-687BE04A6B09}"; //Derived Column
        const string EXPORT_COLUMN_CLASSID = "{AC2C07E7-6818-4D65-B66D-0B1664211568}"; //Export Column
        const string FUZZY_GROUPING_CLASSID = "{045EB6AE-C5ED-49E8-BD21-C6696E456564}"; //Fuzzy Grouping
        const string FUZZY_LOOKUP_CLASSID = "{AD9B9B83-DB60-4188-B57D-93C5155DFACC}"; //Fuzzy Lookup
        const string IMPORT_COLUMN_CLASSID = "{22DF5994-EFB6-4299-BDD6-82077BBF00BC}"; //Import Column
        const string LOOKUP_CLASSID = "{9345248B-9709-4C04-90C1-0853F8B68EE8}"; //Lookup
        const string MERGE_CLASSID = "{FF3FEC06-FAA6-4874-B061-7BEB17A0C215}"; //Merge
        const string MERGE_JOIN = "{C6811950-DD25-48CF-A846-9856185FCB8E}"; //Merge Join
        const string MULTICAST_CLASSID = "{5A44DC2D-5A32-4F6C-8918-C005CD951CFD}"; //Multicast
        const string OLE_DB_COMMAND_CLASSID = "{6B22199D-347E-45F2-A439-AD5C434BD431}"; //OLE DB Command
        const string PERCENT_SAMPLING_CLASSID = "{5E2975CC-9B12-4769-A3BF-9E7CF3E134F7}"; //Percentage Sampling
        const string PIVOT_CLASSID = "{1D55C0C7-52E2-4F6F-BD01-25F7B4A8813D}"; //Pivot
        const string ROW_COUNT_CLASSID = "{BA5B06BC-5EC0-47EC-BFE8-036AB26C6A02}"; //Row Count
        const string ROW_SAMPLING_CLASSID = "{D69604FF-D1EB-4E41-AE48-1F132F18E17F}"; //Row Sampling
        const string SLOWLY_CHANGING_DIMENSION_CLASSID = "{336FEC0D-D431-4A78-A231-67B65C5E06A2}"; //Slowly Changing Dimension
        const string SORT_CLASSID = "{5C680814-BA33-4C2E-86E2-E36F74189C1D}"; //Sort
        const string TERM_EXTRACTION_CLASSID = "{3C705BCA-E5EC-43AD-B733-37890C7430B5}"; //Term Extraction
        const string TERM_LOOKUP_CLASSID = "{B91A578F-C1F1-4815-B14D-73E756272B1C}"; //Term Lookup
        const string UNION_ALL_CLASSID = "{190DA0CA-EB43-4D46-A47C-FAD498103565}"; //Union All
        const string UNPIVOT_CLASSID = "{69E6B4F1-5C46-4E29-BA44-8F354FB954F5}"; //Unpivot

        public static ISSISItem GetObject(object SSISItem)
        {
            TaskHost taskItem = SSISItem as TaskHost;
            
            //Check the package item is a direct task or a container 
            //Note: If taskItem is null then it is not a direct task. Means the task is a container or any other object like "Data Flow Task" or its subtasks.
            if (taskItem == null)
            {
                IDTSComponentMetaData100 component = SSISItem as IDTSComponentMetaData100;
                
                //Check the package item is a container or any other items like sub tasks in a "Data Flow Task".
                if (component == null)
                {
                    if (SSISItem is Sequence)
                    {
                        return new SequenceContainerSearcher();
                    }
                    else if (SSISItem is ForLoop)
                    {
                        return new ForLoopSearcher();
                    }
                    else if (SSISItem is ForEachLoop)
                    {
                        return new ForEachLoopSearcher();
                    }
                }
                else
                {
                    if (component.ComponentClassID == COMMON_CLASSID)
                    {
                        string componentIdentificationString = component.CustomPropertyCollection["UserComponentTypeName"].Value.ToString();

                        if (componentIdentificationString.Contains("ADONETSrc"))
                        {
                            return new ADONetSourceSearcher();
                        }
                        else if (componentIdentificationString.Contains("ADONETDest"))
                        {
                            return new ADONetDestinationSearcher();
                        }
                        else if (componentIdentificationString.Contains("XmlSrc"))
                        {
                            return new XMLSourceSearcher();
                        }
                        else if (componentIdentificationString.Contains("SqlCEDest"))
                        {
                            return new SQLServerCompactDestinationSearcher();
                        }
                        else if (componentIdentificationString.Contains("DataReaderDest"))
                        {
                            return new DataReaderDestinationSearcher();
                        }
                        else if (componentIdentificationString.Contains("TxScript"))
                        {
                            return new ScriptComponentSearcher();
                        }
                    }
                    else if (component.ComponentClassID == EXCEL_SOURCE_CLASSID)
                    {
                        return new ExcelSourceSearcher();
                    }
                    else if (component.ComponentClassID == FLAT_FILE_SOURCE_CLASSID)
                    {
                        return new FlatFileSourceSearcher();
                    }
                    else if (component.ComponentClassID == OLE_DB_SOURCE_CLASSID)
                    {
                        return new OLEDBSourceSearcher();
                    }
                    else if (component.ComponentClassID == RAW_FILE_SOURCE_CLASSID)
                    {
                        return new RawFileSourceSearcher();
                    }
                    else if (component.ComponentClassID == DATAMINING_MODEL_CLASSID)
                    {
                        return new DataMiningModelTrainingSearcher();
                    }
                    else if (component.ComponentClassID == DIM_PROCESSING_CLASSID)
                    {
                        return new DIMProcessingSearcher();
                    }
                    else if (component.ComponentClassID == EXCEL_DEST_CLASSID)
                    {
                        return new ExcelDestSearcher();
                    }
                    else if (component.ComponentClassID == FLAT_FILE_DEST_CLASSID)
                    {
                        return new FlatFileDestSearcher();
                    }
                    else if (component.ComponentClassID == OLE_DB_DEST_CLASSID)
                    {
                        return new OLEDBDestSearcher();
                    }
                    else if (component.ComponentClassID == PARTITION_PROC_CLASSID)
                    {
                        return new PartitionProcessingSearcher();
                    }
                    else if (component.ComponentClassID == RAW_FILE_DEST_CLASSID)
                    {
                        return new RAWFileDestSearcher();
                    }
                    else if (component.ComponentClassID == RECORDSET_DEST_CLASSID)
                    {
                        return new RecordSetDestSearcher();
                    }
                    else if (component.ComponentClassID == SQL_SERV_DEST_CLASSID)
                    {
                        return new SQLServerDestSearcher();
                    }
                    else if (component.ComponentClassID == AGG_CLASSID)
                    {
                        return new AggregateSearcher();
                    }
                    else if (component.ComponentClassID == AUDIT_CLASSID)
                    {
                        return new AuditSearcher();
                    }
                    else if (component.ComponentClassID == CACHE_TRANSFORM_CLASSID)
                    {
                        return new CacheTransformSearcher();
                    }
                    else if (component.ComponentClassID == CHARACTER_MAP_CLASSID)
                    {
                        return new CharacterMapTransformSearcher();
                    }
                    else if (component.ComponentClassID == COND_SPLIT_CLASSID)
                    {
                        return new ConditionalSplitTransformSearcher();
                    }
                    else if (component.ComponentClassID == COPY_COLUMN_CLASSID)
                    {
                        return new CopyColumnTransformSearcher();
                    }
                    else if (component.ComponentClassID == DATA_CONV_CLASSID)
                    {
                        return new DataCoversionTransformSearcher();
                    }
                    else if (component.ComponentClassID == DATA_MINING_QUERY_CLASSID)
                    {
                        return new DataMiningQueryTransformSearcher();
                    }
                    else if (component.ComponentClassID == DERIVED_COLUMN_CLASSID)
                    {
                        return new DerivedColumnTransformSearcher();
                    }
                    else if (component.ComponentClassID == EXPORT_COLUMN_CLASSID)
                    {
                        return new ExportColumnTransformSearcher();
                    }
                    else if (component.ComponentClassID == FUZZY_GROUPING_CLASSID)
                    {
                        return new FuzzyGroupingTransformSearcher();
                    }
                    else if (component.ComponentClassID == FUZZY_LOOKUP_CLASSID)
                    {
                        return new FuzzyLookupTransformSearcher();
                    }
                    else if (component.ComponentClassID == IMPORT_COLUMN_CLASSID)
                    {
                        return new ImportColumnTransformSearcher();
                    }
                    else if (component.ComponentClassID == LOOKUP_CLASSID)
                    {
                        return new LookupTransformSearcher();
                    }
                    else if (component.ComponentClassID == MERGE_CLASSID)
                    {
                        return new MergeTransformSearcher();
                    }
                    else if (component.ComponentClassID == MERGE_JOIN)
                    {
                        return new MergeJoinTransformSearcher();
                    }
                    else if (component.ComponentClassID == SORT_CLASSID)
                    {
                        return new SortTransformSearcher();
                    }
                    else if (component.ComponentClassID == MULTICAST_CLASSID)
                    {
                        return new MulticastTransformSearcher();
                    }
                    else if (component.ComponentClassID == OLE_DB_COMMAND_CLASSID)
                    {
                        return new OLEDBCommandTransformSearcher();
                    }
                    else if (component.ComponentClassID == PERCENT_SAMPLING_CLASSID)
                    {
                        return new PercentageSamplingTransformSearcher();
                    }
                    else if (component.ComponentClassID == PIVOT_CLASSID)
                    {
                        return new PivotTransformSearcher();
                    }
                    else if (component.ComponentClassID == ROW_COUNT_CLASSID)
                    {
                        return new RowCountTransformSearcher();
                    }
                    else if (component.ComponentClassID == ROW_SAMPLING_CLASSID)
                    {
                        return new RowSamplingTransformSearcher();
                    }
                    else if (component.ComponentClassID == SLOWLY_CHANGING_DIMENSION_CLASSID)
                    {
                        return new SlowlyChangingDimensionTransformSearcher();
                    }
                    else if (component.ComponentClassID == TERM_EXTRACTION_CLASSID)
                    {
                        return new TermExtractionTransformSearcher();
                    }
                    else if (component.ComponentClassID == TERM_LOOKUP_CLASSID)
                    {
                        return new TermLookupTransformSearcher();
                    }
                    else if (component.ComponentClassID == UNPIVOT_CLASSID)
                    {
                        return new UnpivotTransformSearcher();
                    }
                    else if (component.ComponentClassID == UNION_ALL_CLASSID)
                    {
                        return new UnionAllTransformSearcher();
                    }
                }
            }
            else
            {
                //There is a reason for using the GetType method two ways because while using "Object is" condition can't able to find the object type
                //in runtime. Hence I used this type of conditions.
                //Note: Do not change this conditions unless until you are confident that the object type is identified correctly during runtime.

                if (taskItem.InnerObject.GetType() == (new ExecuteSQLTask()).GetType())
                {
                    return new ExecuteSQLTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new BulkInsertTask()).GetType())
                {
                    return new BulkInsertTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DMQueryTask()).GetType())
                {
                    return new DMQueryTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DataProfilingTask()).GetType())
                {
                    return new DataProfilingTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new FileSystemTask()).GetType())
                {
                    return new FileSystemTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new FtpTask()).GetType())
                {
                    return new FtpTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new MessageQueueTask()).GetType())
                {
                    return new MessageQueueTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new ScriptTask()).GetType())
                {
                    return new ScriptTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new SendMailTask()).GetType())
                {
                    return new SendMailTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new TransferDatabaseTask()).GetType())
                {
                    return new TransferDatabaseTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new TransferErrorMessagesTask()).GetType())
                {
                    return new TransferErrorMessagesTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new TransferJobsTask()).GetType())
                {
                    return new TransferJobsTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new TransferLoginsTask()).GetType())
                {
                    return new TransferLoginsTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() ==(new TransferStoredProceduresTask()).GetType())
                {
                    return new TransferStoredProceduresTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new TransferSqlServerObjectsTask()).GetType())
                {
                    return new TransferSqlServerObjectsTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new WebServiceTask()).GetType())
                {
                    return new WebServiceTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new WmiDataReaderTask()).GetType())
                {
                    return new WmiDataReaderTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType()  == (new WmiEventWatcherTask()).GetType())
                {
                    return new WmiEventWatcherTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new XMLTask()).GetType())
                {
                    return new XMLTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceBackupTask()).GetType()) 
                {
                    return new DbMaintenanceBackupTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceCheckIntegrityTask()).GetType())
                {
                    return new DbMaintenanceCheckIntegrityTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceExecuteAgentJobTask()).GetType())
                {
                    return new DbMaintenanceExecuteAgentJobTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceTSQLExecuteTask()).GetType())
                {
                    return new DbMaintenanceTSQLExecuteTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceHistoryCleanupTask()).GetType())
                {
                    return new DbMaintenanceHistoryCleanupTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceFileCleanupTask()).GetType())
                {
                    return new DbMaintenanceFileCleanupTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceNotifyOperatorTask()).GetType())
                {
                    return new DbMaintenanceNotifyOperatorTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceReindexTask()).GetType())
                {
                    return new DbMaintenanceReindexTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceDefragmentIndexTask()).GetType())
                {
                    return new DbMaintenanceDefragmentIndexTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceShrinkTask()).GetType())
                {
                    return new DbMaintenanceShrinkTaskSearcher();
                }
                else if (taskItem.InnerObject.GetType() == (new DbMaintenanceUpdateStatisticsTask()).GetType())
                {
                    return new DbMaintenanceUpdateStatisticsTaskSearcher();
                }
                //All these below are COM component based objects hence we need to compare them in the below conditional format only
                else if (taskItem.InnerObject is MainPipe)
                {
                    return new DataFlowTaskSearcher();
                }
                else if (taskItem.InnerObject is ExecutePackageTask)
                {
                    return new ExecPackageTaskSearcher();
                }
                else if (taskItem.InnerObject is ExecuteProcess)
                {
                    return new ExecuteProcessTaskSearcher();
                }
                else if (taskItem.InnerObject is DTSProcessingTask)
                {
                    return new ASProcessingTaskSearcher();
                }
                else if (taskItem.InnerObject is ASExecuteDDLTask)
                {
                    return new ASExecuteDDLTaskSearcher();
                }
            }

            return null;
            //throw new Exception("No Item with this type found");
        }
    }
}
