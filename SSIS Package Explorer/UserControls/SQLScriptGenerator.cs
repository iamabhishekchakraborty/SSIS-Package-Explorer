using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;
using System.IO;

namespace SSIS_Package_Reader.UserControls
{
    public partial class SQLScriptGenerator : UserControl
    {
        BackgroundWorker backgroundThread;
        int totalObjects = 0;
        string database = "";
        string objectType = "";
        string FolderSavePath = "";

        List<string> ObjectTypes = new List<string> { "FUNCTION", "STORED_PROCEDURE", "USER_TABLE", "VIEW" };
        // "SYNONYM", "TYPE_TABLE" --Not implemented

        public SQLScriptGenerator()
        {
            InitializeComponent();

            backgroundThread = new BackgroundWorker();
            backgroundThread.DoWork += new DoWorkEventHandler(Background_DoWork);
            backgroundThread.ProgressChanged += new ProgressChangedEventHandler
                    (Background_ProgressChanged);
            backgroundThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (Background_RunWorkerCompleted);
            backgroundThread.WorkerReportsProgress = true;
            backgroundThread.WorkerSupportsCancellation = true;

            cmbDBObjects.DataSource = ObjectTypes;
        }

        private void SQLScriptGenerator_EnabledChanged(object sender, EventArgs e)
        {
            List<string> databases = new List<string>();

            if (this.Enabled)
            {
                foreach (var database in SQLServerSMO.ServerSMOObj.Databases)
                {
                    databases.Add(((Microsoft.SqlServer.Management.Smo.NamedSmoObject)(database)).Name);
                }

                if (databases.Count > 0)
                {
                    cmbDatabases.DataSource = databases;
                    cmbDatabases.SelectedIndex = 0;
                    cmbDBObjects.SelectedIndex = 0;
                }
            }
            else
            {
                cmbDatabases.DataSource = null;
                cmbDBObjects.SelectedIndex = -1;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnGenerate.Text == "GENERATE SCRIPTS")
            {
                FolderSavePath = "";
                database = cmbDatabases.SelectedValue.ToString();
                objectType = cmbDBObjects.SelectedValue.ToString();


                if (fbdFolder.ShowDialog() == DialogResult.OK)
                {
                    FolderSavePath = fbdFolder.SelectedPath;
                    btnGenerate.Text = "CANCEL";
                    backgroundThread.RunWorkerAsync();
                }
            }
            else
            {
                btnGenerate.Text = "GENERATE SCRIPTS";

                if (backgroundThread.IsBusy)
                {
                    // Notify the worker thread that a cancel has been requested.
                    // The cancel will not actually happen until the thread in the
                    // DoWork checks the m_oWorker.CancellationPending flag. 
                    backgroundThread.CancelAsync();
                }
            }
        }

        void Background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled)
            {
                toolStripStatusLbl.Text = "Operation Cancelled..";
            }

            // Check to see if an error occurred in the background process.

            else if (e.Error != null)
            {
                toolStripStatusLbl.Text = "Error while performing background operation..";
            }
            else
            {
                // Everything completed normally.
                toolStripStatusLbl.Text = "Operation Completed..";
            }

            btnGenerate.Text = "GENERATE SCRIPTS";
        }

        void Background_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            // This function fires on the UI thread so it's safe to edit
            // the UI control directly, no funny business with Control.Invoke :)
            // Update the progressBar with the integer supplied to us from the
            // ReportProgress() function.  

            toolStripStatusLbl.Text = "Generating......(" + e.ProgressPercentage.ToString() + "/" + totalObjects.ToString() + ") completed.";
        }

        void Background_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // The sender is the BackgroundWorker object we need it to
                // report progress and check for cancellation.
                //NOTE : Never play with the UI thread here...
                string returnCode = StartGeneratingScripts(database, objectType);

                if (returnCode == "0")
                {
                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was cancelled.
                    e.Cancel = true;
                    return;
                }
                //else if (returnCode != "1")
                //{
                //    throw new Exception(returnCode);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }

        private string StartGeneratingScripts(string database, string objectType)
        {
            totalObjects = 0;
            int fileSearched = 0;

            Database db = SQLServerSMO.ServerSMOObj.Databases[database];

            if (objectType == "USER_TABLE")
            {
                totalObjects = db.Tables.Count;
                
                foreach (Microsoft.SqlServer.Management.Smo.TableViewTableTypeBase table in db.Tables)
                {
                    SQLObject obj = new SQLObject();
                    obj.ObjectName = table.Name;
                    obj.SchemaName = table.Schema;
                    obj.Database = database;
                    obj.ObjectType = objectType;

                    string data = GetObjectScript(db,obj);

                    WriteToFile(FolderSavePath + "\\"+obj.SchemaName + "." + obj.ObjectName + ".sql", data);

                    backgroundThread.ReportProgress(++fileSearched);

                    if (backgroundThread.CancellationPending)
                    {
                        // Set the e.Cancel flag so that the WorkerCompleted event
                        // knows that the process was cancelled.
                        return "0";
                    }
                }
            }
            else if (objectType == "STORED_PROCEDURE")
            {
                totalObjects = db.StoredProcedures.Count;

                foreach (Microsoft.SqlServer.Management.Smo.StoredProcedure procedure in db.StoredProcedures)
                {
                    if (!procedure.IsSystemObject)
                    {
                        SQLObject obj = new SQLObject();
                        obj.ObjectName = procedure.Name;
                        obj.SchemaName = procedure.Schema;
                        obj.Database = database;
                        obj.ObjectType = objectType;

                        string data = GetObjectScript(db, obj);

                        WriteToFile(FolderSavePath + "\\" + obj.SchemaName + "." + obj.ObjectName + ".sql", data);

                        backgroundThread.ReportProgress(++fileSearched);

                        if (backgroundThread.CancellationPending)
                        {
                            // Set the e.Cancel flag so that the WorkerCompleted event
                            // knows that the process was cancelled.
                            return "0";
                        }
                    }
                }
            }
            else if (objectType == "FUNCTION")
            {
                totalObjects = db.UserDefinedFunctions.Count;
                foreach (Microsoft.SqlServer.Management.Smo.UserDefinedFunction function in db.UserDefinedFunctions)
                {
                    if (!function.IsSystemObject)
                    {
                        SQLObject obj = new SQLObject();
                        obj.ObjectName = function.Name;
                        obj.SchemaName = function.Schema;
                        obj.Database = database;
                        obj.ObjectType = objectType;

                        string data = GetObjectScript(db, obj);

                        WriteToFile(FolderSavePath + "\\" + obj.SchemaName + "." + obj.ObjectName + ".sql", data);

                        backgroundThread.ReportProgress(++fileSearched);

                        if (backgroundThread.CancellationPending)
                        {
                            // Set the e.Cancel flag so that the WorkerCompleted event
                            // knows that the process was cancelled.
                            return "0";
                        }
                    }
                }
            }
            else if (objectType == "VIEW")
            {
                totalObjects = db.Views.Count;
                foreach (Microsoft.SqlServer.Management.Smo.View view in db.Views)
                {
                    if (!view.IsSystemObject)
                    {
                        SQLObject obj = new SQLObject();
                        obj.ObjectName = view.Name;
                        obj.SchemaName = view.Schema;
                        obj.Database = database;
                        obj.ObjectType = objectType;

                        string data = GetObjectScript(db, obj);

                        WriteToFile(FolderSavePath + "\\" + obj.SchemaName + "." + obj.ObjectName + ".sql", data);

                        backgroundThread.ReportProgress(++fileSearched);

                        if (backgroundThread.CancellationPending)
                        {
                            // Set the e.Cancel flag so that the WorkerCompleted event
                            // knows that the process was cancelled.
                            return "0";
                        }
                    }
                }
            }
            //else if (objectType == "SYNONYM")
            //{
            //    Synonym synonym = db.Synonyms[obj.ObjectName, obj.SchemaName];
            //    ScriptingOptions scriptOptions = new ScriptingOptions();
            //    scriptOptions.IncludeHeaders = true;
            //    scriptOptions.SchemaQualify = true;
            //    return synonym.Script(scriptOptions);
            //}
            //else if (objectType == "SQL_TRIGGER")
            //{
            //    foreach (Table table in db.Tables)
            //    {
            //        foreach (Trigger trigger in table.Triggers)
            //        {
            //            if (trigger.Name == obj.ObjectName)
            //            {
            //                ScriptingOptions scriptOptions = new ScriptingOptions();
            //                scriptOptions.IncludeHeaders = true;
            //                scriptOptions.SchemaQualify = true;
            //                return trigger.Script(scriptOptions);
            //            }
            //        }
            //    }
            //}
            //else if (objectType == "TYPE_TABLE")
            //{
            //    foreach (UserDefinedTableType userDefinedTT in db.UserDefinedTableTypes)
            //    {
            //        if (userDefinedTT.Name == obj.ObjectName && userDefinedTT.Schema == obj.SchemaName)
            //        {
            //            ScriptingOptions scriptOptions = new ScriptingOptions();
            //            scriptOptions.IncludeHeaders = true;
            //            scriptOptions.SchemaQualify = true;
            //            return userDefinedTT.Script(scriptOptions);
            //        }
            //    }
            //}

            return "";
        }

        private string GetObjectScript(Database db,SQLObject obj)
        {
            try
            {
                //--U 	USER_TABLE -- No Value
                //--P 	SQL_STORED_PROCEDURE -- Has Value
                //FN	SQL_SCALAR_FUNCTION -- Has Value
                //IF	SQL_INLINE_TABLE_VALUED_FUNCTION -- Has Value
                //TF	SQL_TABLE_VALUED_FUNCTION -- Has Value
                //V 	VIEW -- Has Value
                //SN	SYNONYM -- No Value
                //TT	TYPE_TABLE

                //F 	FOREIGN_KEY_CONSTRAINT -- No Value
                //PK	PRIMARY_KEY_CONSTRAINT -- No Value
                //D 	DEFAULT_CONSTRAINT -- Value
                //UQ	UNIQUE_CONSTRAINT -- No Value

                if (obj.ObjectType == "USER_TABLE")
                {
                    Table table = db.Tables[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    //scriptOptions.IncludeDatabaseContext = true;
                    //scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return ConvertToString(table.Script(scriptOptions));
                }
                else if (obj.ObjectType == "STORED_PROCEDURE")
                {
                    StoredProcedure sp = db.StoredProcedures[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeDatabaseContext = true;
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return ConvertToString(sp.Script(scriptOptions));
                }
                else if (obj.ObjectType == "FUNCTION")
                {
                    UserDefinedFunction udf = db.UserDefinedFunctions[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeDatabaseContext = true;
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return ConvertToString(udf.Script(scriptOptions));
                }
                else if (obj.ObjectType == "VIEW")
                {
                    Microsoft.SqlServer.Management.Smo.View view = db.Views[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeDatabaseContext = true;
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return ConvertToString(view.Script(scriptOptions));
                }
                else if (obj.ObjectType == "SYNONYM")
                {
                    Synonym synonym = db.Synonyms[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeDatabaseContext = true;
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return ConvertToString(synonym.Script(scriptOptions));
                }
                else if (obj.ObjectType == "SQL_TRIGGER")
                {
                    foreach (Table table in db.Tables)
                    {
                        foreach (Trigger trigger in table.Triggers)
                        {
                            if (trigger.Name == obj.ObjectName)
                            {
                                ScriptingOptions scriptOptions = new ScriptingOptions();
                                scriptOptions.IncludeDatabaseContext = true;
                                scriptOptions.IncludeHeaders = true;
                                scriptOptions.SchemaQualify = true;
                                return ConvertToString(trigger.Script(scriptOptions));
                            }
                        }
                    }
                }
                else if (obj.ObjectType == "TYPE_TABLE")
                {
                    foreach (UserDefinedTableType userDefinedTT in db.UserDefinedTableTypes)
                    {
                        if (userDefinedTT.Name == obj.ObjectName && userDefinedTT.Schema == obj.SchemaName)
                        {
                            ScriptingOptions scriptOptions = new ScriptingOptions();
                            scriptOptions.IncludeDatabaseContext = true;
                            scriptOptions.IncludeHeaders = true;
                            scriptOptions.SchemaQualify = true;
                            return ConvertToString(userDefinedTT.Script(scriptOptions));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            return "";
        }

        private string ConvertToString(StringCollection ObjectScript)
        {
            string script = "";
            foreach (var item in ObjectScript)
            {
                if (item.ToString().ToLower() == "SET ANSI_NULLS ON".ToLower() || item.ToString().ToLower() == "SET QUOTED_IDENTIFIER ON".ToLower())
                {
                    script += item.ToString() + "\n" + "GO\n";
                }
                else
                {
                    script += item.ToString() + "\n";
                }

                
            }
            
            return script;
        }

        private bool WriteToFile(string fileName, string data)
        {
            //If you want to change any data, also check in the method "WriteToExcelFile"
            try
            {
                using (var stream = File.CreateText(fileName))
                {
                    stream.Write(data);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing into file: " + ex.Message, "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }
    }
}
