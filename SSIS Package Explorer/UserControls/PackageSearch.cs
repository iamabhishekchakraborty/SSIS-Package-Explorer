using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.IO;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;
using SSIS_Package_Reader.SSIS_Item_Factory;
using System.Threading;
using ExcelLibrary.SpreadSheet;

namespace SSIS_Package_Reader.UserControls
{
    public partial class PackageSearch : UserControl
    {
        bool IsFileSelected = false;
        BackgroundWorker backgroundThread;
        int totalFilesToSearch = 0;
        string searchString = "";

        List<PackageItem> packageItemsFound = new List<PackageItem>();
        List<ErrorInfo> errors = new List<ErrorInfo>();

        public PackageSearch()
        {
            InitializeComponent();

            SearchOptions.ExpressionType = SearchOptions.ExpressionTypes.None;

            fdlgFile.Filter = "SSIS Package files (*.dtsx)|*.dtsx";

            backgroundThread = new BackgroundWorker();

            toolStripStatusLbl.Text = "Start Searching..";
            statusBar.SizingGrip = false;
            btnCancel.Enabled = false;

            // Create a background worker thread that ReportsProgress &
            // SupportsCancellation
            // Hook up the appropriate events.
            backgroundThread.DoWork += new DoWorkEventHandler(Background_DoWork);
            backgroundThread.ProgressChanged += new ProgressChangedEventHandler
                    (Background_ProgressChanged);
            backgroundThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                    (Background_RunWorkerCompleted);
            backgroundThread.WorkerReportsProgress = true;
            backgroundThread.WorkerSupportsCancellation = true;

        }

        //This code will remove the screen flickering
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        /// <summary>
        /// On completed do the appropriate task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        void Background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled)
            {
                toolStripStatusLbl.Text = "Search Cancelled..";
            }

            // Check to see if an error occurred in the background process.

            else if (e.Error != null)
            {
                toolStripStatusLbl.Text = "Error while performing background operation..";
            }
            else
            {
                // Everything completed normally.
                toolStripStatusLbl.Text = "Search Completed..";
            }

            try
            {
                //Check whether any items found based on the search criteria, and write result into file
                if (packageItemsFound.Count > 0 || errors.Count > 0)
                {
                    saveFdlg.Filter = "Excel Files|*.xls|Text Files|*.txt";

                    if (saveFdlg.ShowDialog() == DialogResult.OK)
                    {
                        //string ext = System.IO.Path.GetExtension(saveFdlg.FileName);
                        bool writeSucess = false;
                        if (saveFdlg.FilterIndex == 1) //Selected the filter to write into excel file
                        {
                            writeSucess = WriteToExcelFile(saveFdlg.FileName, packageItemsFound);
                        }
                        else
                        {
                            writeSucess = WriteToFile(saveFdlg.FileName, packageItemsFound);
                        }

                        if (writeSucess)
                        {
                            MessageBox.Show("Successfully written into file.", "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (errors.Count > 0)
                            {
                                MessageBox.Show("Not able to search some files. Please check for errors at end of the output file.", "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }


                            DialogResult dialogResult = MessageBox.Show("Do you want to open file ?", "SSIS Package Explorer", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(@saveFdlg.FileName);
                            }
                        }
                    }
                }
                else
                {
                    if (e.Error == null && !e.Cancelled)
                    {
                        MessageBox.Show("No item found in provided package(s).", "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //Change the status of the buttons on the UI accordingly
            totalFilesToSearch = 0;
            btnSubmit.Enabled = true;
            btnReset.Enabled = true;
            btnCancel.Enabled = false;
            btnOpenDialog.Enabled = true;

            txtSearchString.Enabled = true;
            rbtnFile.Enabled = true;
            rbtnFolder.Enabled = true;

            searchString = "";

            chkBoxMatchCase.Enabled = true;
            chkBoxMatchWholeWord.Enabled = true;
            chkBoxUse.Enabled = true;

            if (chkBoxUse.Checked)
            {
                cmbExpressionTypes.Enabled = true;
            }
        }

        void Background_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            // This function fires on the UI thread so it's safe to edit
            // the UI control directly, no funny business with Control.Invoke :)
            // Update the progressBar with the integer supplied to us from the
            // ReportProgress() function.  

            toolStripStatusLbl.Text = "Searching......(" + e.ProgressPercentage.ToString() + "/" + totalFilesToSearch.ToString() + ") completed.";
        }

        void Background_DoWork(object sender, DoWorkEventArgs e)
        {
            packageItemsFound = new List<PackageItem>();
            errors = new List<ErrorInfo>();

            try
            {
                // The sender is the BackgroundWorker object we need it to
                // report progress and check for cancellation.
                //NOTE : Never play with the UI thread here...
                string returnCode = StartSearching(searchString);

                if (returnCode == "0")
                {
                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was cancelled.
                    e.Cancel = true;
                    return;
                }
                else if (returnCode != "1")
                {
                    throw new Exception(returnCode);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }

        private void btnOpenDialog_Click(object sender, EventArgs e)
        {
            txtFileFolderName.Text = "";

            if (rbtnFile.Checked)
            {
                if (fdlgFile.ShowDialog() == DialogResult.OK)
                {
                    txtFileFolderName.Text = fdlgFile.FileName;
                    IsFileSelected = true;
                }
                else
                {
                    IsFileSelected = false;
                }
            }
            else if (rbtnFolder.Checked)
            {
                if (fbdFolder.ShowDialog() == DialogResult.OK)
                {
                    txtFileFolderName.Text = fbdFolder.SelectedPath;
                    IsFileSelected = true;
                }
                else
                {
                    IsFileSelected = false;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFileFolderName.Text = "";
            txtSearchString.Text = "";
            rbtnFile.Checked = true;

            totalFilesToSearch = 0;
            btnSubmit.Enabled = true;
            btnReset.Enabled = true;
            btnCancel.Enabled = false;

            txtSearchString.Enabled = true;
            rbtnFile.Enabled = true;
            rbtnFolder.Enabled = true;

            chkBoxMatchCase.Checked = false;
            chkBoxMatchWholeWord.Checked = false;
            chkBoxUse.Checked = false;
            cmbExpressionTypes.SelectedIndex = -1;

            btnOpenDialog.Enabled = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFileFolderName.Text == "")
                {
                    btnOpenDialog.PerformClick();

                    if (!IsFileSelected)
                    {
                        return;
                    }
                }

                IsFileSelected = false;
                if (txtSearchString.Text == "")
                {
                    MessageBox.Show("Enter a string to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSearchString.Focus();
                    return;
                }
                else if (txtSearchString.Text.Length < 3)
                {
                    MessageBox.Show("Search term should be minimum 3 characters length.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSearchString.Focus();
                    return;
                }

                //Change the status of the buttons on the UI accordingly
                totalFilesToSearch = 0;
                btnSubmit.Enabled = false;
                btnReset.Enabled = false;
                btnCancel.Enabled = true;
                txtSearchString.Enabled = false;
                rbtnFile.Enabled = false;
                rbtnFolder.Enabled = false;
                btnOpenDialog.Enabled = false;

                chkBoxMatchCase.Enabled = false;
                chkBoxMatchWholeWord.Enabled = false;
                chkBoxUse.Enabled = false;
                cmbExpressionTypes.Enabled = false;

                searchString = txtSearchString.Text;
                backgroundThread.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string StartSearching(string patternToSearch)
        {
            toolStripStatusLbl.Text = "Searching Started..";
            Package dtsPackage = null;
            Microsoft.SqlServer.Dts.Runtime.Application dtsApplication = null;
            try
            {
                bool isDir = ((File.GetAttributes(txtFileFolderName.Text) & FileAttributes.Directory) == FileAttributes.Directory);

                dtsApplication = new Microsoft.SqlServer.Dts.Runtime.Application();

                if (!isDir)
                {
                    totalFilesToSearch = 1;
                    dtsPackage = dtsApplication.LoadPackage(txtFileFolderName.Text, null);
                    string fileName = @Path.GetFullPath(txtFileFolderName.Text);

                    SearchPackage(dtsPackage, packageItemsFound, fileName, patternToSearch);
                    backgroundThread.ReportProgress(1);
                }
                else
                {
                    string[] packageFiles = Directory.GetFiles(txtFileFolderName.Text, "*.dtsx", SearchOption.AllDirectories);
                    totalFilesToSearch = packageFiles.Count();
                    int filesSearched = 0;
                    backgroundThread.ReportProgress(1);

                    foreach (string packageFile in packageFiles)
                    {
                        if (backgroundThread.CancellationPending)
                        {
                            // Set the e.Cancel flag so that the WorkerCompleted event
                            // knows that the process was cancelled.
                            return "0";
                        }

                        try
                        {
                            dtsPackage = dtsApplication.LoadPackage(packageFile, null);
                            string fileName = @Path.GetFullPath(packageFile);

                            SearchPackage(dtsPackage, packageItemsFound, fileName, patternToSearch);

                            filesSearched++;
                            backgroundThread.ReportProgress(filesSearched);
                        }
                        catch (Exception e)
                        {
                            ErrorInfo error = new ErrorInfo();
                            error.ErrorFile = packageFile;
                            error.ErrorMessage = e.Message;
                            error.StackTrace = e.StackTrace;
                            errors.Add(error);
                            //No logging done.
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (dtsPackage != null)
                {
                    dtsPackage.Dispose();
                }

                if (dtsApplication != null)
                {
                    dtsApplication = null;
                }
            }

            return "1";
        }

        private void SearchPackage(Package dtsPackage, List<PackageItem> packageItemsFound, string fileName, string patternToSearch)
        {
            PackageInfo packageData = new PackageInfo();
            packageData.GUID = dtsPackage.ID;
            packageData.Name = dtsPackage.Name;
            packageData.FileName = fileName;
            packageData.Type = dtsPackage.Description;

            CommonUtility.CheckDuplicatesAndAdd(packageItemsFound, CommonUtility.SearchVariables(dtsPackage.Variables, patternToSearch, packageData, dtsPackage.Name, "Package"));

            if (CommonUtility.CheckStringExistence(dtsPackage.CreatorName, patternToSearch))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = "Package -> CreatorName";
                taskNameItem.Name = packageData.Name;
                taskNameItem.GUID = packageData.GUID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                packageItemsFound.Add(taskNameItem);
            }

            if (CommonUtility.CheckStringExistence(dtsPackage.CreatorComputerName, patternToSearch))
            {
                PackageItem taskNameItem = new PackageItem();
                taskNameItem.Type = "Package -> CreatorComputerName";
                taskNameItem.Name = packageData.Name;
                taskNameItem.GUID = packageData.GUID;
                taskNameItem.InterfaceName = packageData.Name;
                taskNameItem.FileName = packageData.FileName;
                packageItemsFound.Add(taskNameItem);
            }

            //Searching Logging providers
            foreach (LogProvider provider in dtsPackage.LogProviders)
            {
                if (CommonUtility.CheckStringExistence(provider.Name, patternToSearch))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = "Package -> Log Provider -> Name";
                    taskNameItem.Name = packageData.Name + " -> " + provider.Name;
                    taskNameItem.GUID = provider.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    packageItemsFound.Add(taskNameItem);
                }

                if (CommonUtility.CheckStringExistence(provider.Description, patternToSearch))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = "Package -> Log Provider -> Description";
                    taskNameItem.Name = packageData.Name + " -> " + provider.Name;
                    taskNameItem.GUID = provider.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    packageItemsFound.Add(taskNameItem);
                }

                if (CommonUtility.CheckStringExistence(provider.ConfigString, patternToSearch))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = "Package -> Log Provider -> Connection Name";
                    taskNameItem.Name = packageData.Name + " -> " + provider.Name;
                    taskNameItem.GUID = provider.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    packageItemsFound.Add(taskNameItem);
                }
            }

            //Searching all the connections
            foreach (ConnectionManager connection in dtsPackage.Connections)
            {
                if (CommonUtility.CheckStringExistence(connection.Name, patternToSearch))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = "Package -> Connection -> Name";
                    taskNameItem.Name = packageData.Name + " -> " + connection.Name;
                    taskNameItem.GUID = connection.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    packageItemsFound.Add(taskNameItem);
                }

                if (CommonUtility.CheckStringExistence(connection.Description, patternToSearch))
                {
                    PackageItem taskNameItem = new PackageItem();
                    taskNameItem.Type = "Package -> Connection -> Description";
                    taskNameItem.Name = packageData.Name + " -> " + connection.Name;
                    taskNameItem.GUID = connection.ID;
                    taskNameItem.InterfaceName = packageData.Name;
                    taskNameItem.FileName = packageData.FileName;
                    packageItemsFound.Add(taskNameItem);
                }


                foreach (DtsProperty property in connection.Properties)
                {
                    string expression = property.GetExpression(connection);

                    expression = expression == null ? "" : @expression.Replace("\\", "").Replace("\"", "").Replace("+", "");

                    string patternToSearchModified;

                    if (SearchOptions.ExpressionType == SearchOptions.ExpressionTypes.RegularExpression)
                    {
                        patternToSearchModified = patternToSearch;
                    }
                    else
                    {
                        patternToSearchModified = @patternToSearch.Replace("\\", "").Replace("\"", "").Replace("+", "");
                    }


                    if (CommonUtility.CheckStringExistence(expression, patternToSearchModified))
                    {
                        PackageItem taskNameItem = new PackageItem();
                        taskNameItem.Type = "Package -> Connection -> Property -> Expression";
                        taskNameItem.Name = packageData.Name + " -> " + connection.Name + " -> " + property.Name;
                        taskNameItem.GUID = connection.ID;
                        taskNameItem.InterfaceName = packageData.Name;
                        taskNameItem.FileName = packageData.FileName;
                        packageItemsFound.Add(taskNameItem);
                    }
                }
            }

            if (dtsPackage.Executables.Count > 0)
            {

                CommonUtility.CheckDuplicatesAndAdd(packageItemsFound, CommonUtility.SearchPrecedenceConstraints(dtsPackage.PrecedenceConstraints, txtSearchString.Text, packageData, packageData.Type));
            }

            foreach (Executable item in dtsPackage.Executables)
            {
                ISSISItem SSISItem = SSISItemFactory.GetObject(item);

                if (SSISItem != null)
                {
                    CommonUtility.CheckDuplicatesAndAdd(packageItemsFound, SSISItem.Search(txtSearchString.Text, item, packageData));
                }
            }
        }

        private bool WriteToFile(string file, List<PackageItem> packageItemsFound)
        {
            //If you want to change any data, also check in the method "WriteToExcelFile"
            try
            {
                using (var stream = File.CreateText(file))
                {
                    stream.WriteLine("Searched for:~" + txtSearchString.Text + "" + Environment.NewLine + Environment.NewLine);
                    string row = "File Path~File Name~Item Name Path~Item Type Path";
                    stream.WriteLine(row);

                    foreach (PackageItem item in packageItemsFound)
                    {
                        row = string.Format("{0}~{1}~{2}~{3}", Path.GetDirectoryName(item.FileName), Path.GetFileName(item.FileName), item.Name, item.Type);

                        stream.WriteLine(row);
                    }

                    stream.WriteLine(Environment.NewLine + Environment.NewLine);
                    stream.WriteLine("ERRORS");

                    if (errors.Count > 0)
                    {
                        row = "File Path~File Name~Error Message";
                        stream.WriteLine(row);

                        foreach (ErrorInfo error in errors)
                        {
                            row = string.Format("{0}~{1}~{2}", Path.GetDirectoryName(error.ErrorFile), Path.GetFileName(error.ErrorFile), error.ErrorMessage);

                            stream.WriteLine(row);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing into file: " + ex.Message, "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        private bool WriteToExcelFile(string file, List<PackageItem> packageItemsFound)
        {
            try
            {
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("Search Result");
                int row = 0;

                //Writing the data into first row
                worksheet.Cells[row, 0] = new Cell("Searched for:");
                worksheet.Cells[row, 1] = new Cell(txtSearchString.Text);

                row = row + 3;
                //Writing the header data
                worksheet.Cells[row, 0] = new Cell("File Path");
                worksheet.Cells[row, 1] = new Cell("File Name");
                worksheet.Cells[row, 2] = new Cell("Item Name Path");
                worksheet.Cells[row, 3] = new Cell("Item Type Path");

                row++;

                //Writing the remaining data
                foreach (PackageItem item in packageItemsFound)
                {
                    worksheet.Cells[row, 0] = new Cell(Path.GetDirectoryName(item.FileName));
                    worksheet.Cells[row, 1] = new Cell(Path.GetFileName(item.FileName));
                    worksheet.Cells[row, 2] = new Cell(item.Name);
                    worksheet.Cells[row, 3] = new Cell(item.Type);
                    row++;
                }

                row = row + 2;
                worksheet.Cells[row, 0] = new Cell("ERRORS");
                row++;
                if (errors.Count > 0)
                {
                    worksheet.Cells[row, 0] = new Cell("File Path");
                    worksheet.Cells[row, 1] = new Cell("File Name");
                    worksheet.Cells[row, 2] = new Cell("Error Message");

                    row++;

                    foreach (ErrorInfo error in errors)
                    {
                        worksheet.Cells[row, 0] = new Cell(Path.GetDirectoryName(error.ErrorFile));
                        worksheet.Cells[row, 1] = new Cell(Path.GetFileName(error.ErrorFile));
                        worksheet.Cells[row, 2] = new Cell(error.ErrorMessage);
                        row++;
                    }
                }

                row = row + 2;

                //This is a dummy data written into the file to fix a unknown error in this library

                for (int rowNo = row; rowNo < row + 150; rowNo++)
                {
                    for (int colNo = 0; colNo < 4; colNo++)
                    {
                        worksheet.Cells[rowNo, colNo] = new Cell(" ");

                    }
                }

                //worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00"); 
                //worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY\-MM\-DD"); 
                //worksheet.Cells.ColumnWidth[0, 1] = 3000; 
                workbook.Worksheets.Add(worksheet);
                workbook.Save(file);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing into file: " + ex.Message, "SSIS Package Explorer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundThread.IsBusy)
            {
                // Notify the worker thread that a cancel has been requested.
                // The cancel will not actually happen until the thread in the
                // DoWork checks the m_oWorker.CancellationPending flag. 
                backgroundThread.CancelAsync();
            }
        }

        private void rbtnFile_CheckedChanged(object sender, EventArgs e)
        {
            txtFileFolderName.Text = "";
        }

        private void rbtnFolder_CheckedChanged(object sender, EventArgs e)
        {
            txtFileFolderName.Text = "";
        }

        private void chkBoxMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxMatchCase.Checked)
            {
                SearchOptions.IsCaseSensitive = true;
            }
            else
            {
                SearchOptions.IsCaseSensitive = false;
            }
        }

        private void chkBoxMatchWholeWord_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxMatchWholeWord.Checked)
            {
                SearchOptions.MatchWholeWord = true;
                SearchOptions.MatchSubstring = false;
            }
            else
            {
                SearchOptions.MatchSubstring = true;
                SearchOptions.MatchWholeWord = false;
            }
        }

        private void chkBoxUse_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxUse.Checked)
            {
                cmbExpressionTypes.Enabled = true;
                chkBoxMatchWholeWord.Checked = false;
                chkBoxMatchWholeWord.Enabled = false;
                cmbExpressionTypes.SelectedIndex = -1;
            }
            else
            {
                cmbExpressionTypes.Enabled = false;
                chkBoxMatchWholeWord.Enabled = true;
                SearchOptions.ExpressionType = SearchOptions.ExpressionTypes.None;
            }
        }

        private void cmbExpressionTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbExpressionTypes.SelectedIndex == -1)
            {
                SearchOptions.ExpressionType = SearchOptions.ExpressionTypes.None;
            }
            else if (cmbExpressionTypes.SelectedIndex == 0)
            {
                SearchOptions.ExpressionType = SearchOptions.ExpressionTypes.RegularExpression;
            }
            else if (cmbExpressionTypes.SelectedIndex == 1)
            {
                SearchOptions.ExpressionType = SearchOptions.ExpressionTypes.WildCard;
            }
            else
            {
                SearchOptions.ExpressionType = SearchOptions.ExpressionTypes.None;
            }
        }
    }
}
