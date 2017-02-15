namespace SSIS_Package_Reader.UserControls
{
    partial class SSRSSearch
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SSRSSearch));
            this.lblSelect = new System.Windows.Forms.Label();
            this.cmbExpressionTypes = new System.Windows.Forms.ComboBox();
            this.chkBoxUse = new System.Windows.Forms.CheckBox();
            this.chkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.chkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.rbtnFile = new System.Windows.Forms.RadioButton();
            this.txtFileFolderName = new System.Windows.Forms.TextBox();
            this.txtSearchString = new System.Windows.Forms.TextBox();
            this.rbtnFolder = new System.Windows.Forms.RadioButton();
            this.grpBoxSSRSReportSearch = new System.Windows.Forms.GroupBox();
            this.lblSSISSearchTitle = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.fdlgFile = new System.Windows.Forms.OpenFileDialog();
            this.toolTipResetButton = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipCancelButton = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipSearchTxtBox = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipFolderRadio = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipFileRadioButton = new System.Windows.Forms.ToolTip(this.components);
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFdlg = new System.Windows.Forms.SaveFileDialog();
            this.toolTipSelectFile = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipPackageLocationName = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipSearchButton = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipMatchCase = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipMatchWholeWord = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipUseCheckBox = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipExpressionTypeComboBox = new System.Windows.Forms.ToolTip(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpenDialog = new System.Windows.Forms.Button();
            this.grpBoxSSRSReportSearch.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.BackColor = System.Drawing.Color.Transparent;
            this.lblSelect.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelect.Location = new System.Drawing.Point(87, 188);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(56, 15);
            this.lblSelect.TabIndex = 29;
            this.lblSelect.Text = "Report :";
            // 
            // cmbExpressionTypes
            // 
            this.cmbExpressionTypes.AutoCompleteCustomSource.AddRange(new string[] {
            "None",
            "Regular Expression"});
            this.cmbExpressionTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpressionTypes.Enabled = false;
            this.cmbExpressionTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbExpressionTypes.FormattingEnabled = true;
            this.cmbExpressionTypes.Items.AddRange(new object[] {
            "Regular Expression",
            "Wildcards"});
            this.cmbExpressionTypes.Location = new System.Drawing.Point(419, 313);
            this.cmbExpressionTypes.Name = "cmbExpressionTypes";
            this.cmbExpressionTypes.Size = new System.Drawing.Size(121, 21);
            this.cmbExpressionTypes.TabIndex = 39;
            this.toolTipExpressionTypeComboBox.SetToolTip(this.cmbExpressionTypes, "Use this combobox to select a particular expression type.");
            this.cmbExpressionTypes.SelectedIndexChanged += new System.EventHandler(this.cmbExpressionTypes_SelectedIndexChanged);
            // 
            // chkBoxUse
            // 
            this.chkBoxUse.AutoSize = true;
            this.chkBoxUse.BackColor = System.Drawing.Color.Transparent;
            this.chkBoxUse.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxUse.Location = new System.Drawing.Point(373, 314);
            this.chkBoxUse.Name = "chkBoxUse";
            this.chkBoxUse.Size = new System.Drawing.Size(45, 18);
            this.chkBoxUse.TabIndex = 38;
            this.chkBoxUse.Text = "Use";
            this.toolTipUseCheckBox.SetToolTip(this.chkBoxUse, "Check this box, for enabling the expression type selection combo box.");
            this.chkBoxUse.UseVisualStyleBackColor = false;
            this.chkBoxUse.CheckedChanged += new System.EventHandler(this.chkBoxUse_CheckedChanged);
            // 
            // chkBoxMatchWholeWord
            // 
            this.chkBoxMatchWholeWord.AutoSize = true;
            this.chkBoxMatchWholeWord.BackColor = System.Drawing.Color.Transparent;
            this.chkBoxMatchWholeWord.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxMatchWholeWord.Location = new System.Drawing.Point(247, 314);
            this.chkBoxMatchWholeWord.Name = "chkBoxMatchWholeWord";
            this.chkBoxMatchWholeWord.Size = new System.Drawing.Size(125, 18);
            this.chkBoxMatchWholeWord.TabIndex = 37;
            this.chkBoxMatchWholeWord.Text = "Match Whole Word";
            this.toolTipMatchWholeWord.SetToolTip(this.chkBoxMatchWholeWord, "Chek this box, for word search.");
            this.chkBoxMatchWholeWord.UseVisualStyleBackColor = false;
            this.chkBoxMatchWholeWord.CheckedChanged += new System.EventHandler(this.chkBoxMatchWholeWord_CheckedChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(87, 262);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(55, 15);
            this.lblSearch.TabIndex = 34;
            this.lblSearch.Text = "Search :";
            // 
            // chkBoxMatchCase
            // 
            this.chkBoxMatchCase.AutoSize = true;
            this.chkBoxMatchCase.BackColor = System.Drawing.Color.Transparent;
            this.chkBoxMatchCase.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxMatchCase.Location = new System.Drawing.Point(159, 314);
            this.chkBoxMatchCase.Name = "chkBoxMatchCase";
            this.chkBoxMatchCase.Size = new System.Drawing.Size(85, 18);
            this.chkBoxMatchCase.TabIndex = 36;
            this.chkBoxMatchCase.Text = "Match Case";
            this.toolTipMatchCase.SetToolTip(this.chkBoxMatchCase, "Check this box, for case sensitive search.");
            this.chkBoxMatchCase.UseVisualStyleBackColor = false;
            this.chkBoxMatchCase.CheckedChanged += new System.EventHandler(this.chkBoxMatchCase_CheckedChanged);
            // 
            // rbtnFile
            // 
            this.rbtnFile.AutoSize = true;
            this.rbtnFile.BackColor = System.Drawing.Color.Transparent;
            this.rbtnFile.Checked = true;
            this.rbtnFile.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnFile.Location = new System.Drawing.Point(162, 217);
            this.rbtnFile.Name = "rbtnFile";
            this.rbtnFile.Size = new System.Drawing.Size(43, 18);
            this.rbtnFile.TabIndex = 32;
            this.rbtnFile.TabStop = true;
            this.rbtnFile.Text = "File";
            this.toolTipFileRadioButton.SetToolTip(this.rbtnFile, "Check this button to search a single file");
            this.rbtnFile.UseVisualStyleBackColor = false;
            this.rbtnFile.CheckedChanged += new System.EventHandler(this.rbtnFile_CheckedChanged);
            // 
            // txtFileFolderName
            // 
            this.txtFileFolderName.BackColor = System.Drawing.SystemColors.Window;
            this.txtFileFolderName.Location = new System.Drawing.Point(162, 185);
            this.txtFileFolderName.Name = "txtFileFolderName";
            this.txtFileFolderName.ReadOnly = true;
            this.txtFileFolderName.Size = new System.Drawing.Size(375, 20);
            this.txtFileFolderName.TabIndex = 30;
            this.toolTipPackageLocationName.SetToolTip(this.txtFileFolderName, "Text box to display the file or folder full path.");
            // 
            // txtSearchString
            // 
            this.txtSearchString.Location = new System.Drawing.Point(162, 259);
            this.txtSearchString.Name = "txtSearchString";
            this.txtSearchString.Size = new System.Drawing.Size(375, 20);
            this.txtSearchString.TabIndex = 35;
            this.toolTipSearchTxtBox.SetToolTip(this.txtSearchString, "Textbox to enter the search pattern.");
            // 
            // rbtnFolder
            // 
            this.rbtnFolder.AutoSize = true;
            this.rbtnFolder.BackColor = System.Drawing.Color.Transparent;
            this.rbtnFolder.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnFolder.Location = new System.Drawing.Point(238, 217);
            this.rbtnFolder.Name = "rbtnFolder";
            this.rbtnFolder.Size = new System.Drawing.Size(58, 18);
            this.rbtnFolder.TabIndex = 33;
            this.rbtnFolder.Text = "Folder";
            this.toolTipFolderRadio.SetToolTip(this.rbtnFolder, "Check this button to search set of files in a folder.");
            this.rbtnFolder.UseVisualStyleBackColor = false;
            this.rbtnFolder.CheckedChanged += new System.EventHandler(this.rbtnFolder_CheckedChanged);
            // 
            // grpBoxSSRSReportSearch
            // 
            this.grpBoxSSRSReportSearch.Controls.Add(this.lblSSISSearchTitle);
            this.grpBoxSSRSReportSearch.Controls.Add(this.statusBar);
            this.grpBoxSSRSReportSearch.Controls.Add(this.lblSelect);
            this.grpBoxSSRSReportSearch.Controls.Add(this.rbtnFolder);
            this.grpBoxSSRSReportSearch.Controls.Add(this.btnReset);
            this.grpBoxSSRSReportSearch.Controls.Add(this.txtSearchString);
            this.grpBoxSSRSReportSearch.Controls.Add(this.cmbExpressionTypes);
            this.grpBoxSSRSReportSearch.Controls.Add(this.txtFileFolderName);
            this.grpBoxSSRSReportSearch.Controls.Add(this.btnSubmit);
            this.grpBoxSSRSReportSearch.Controls.Add(this.rbtnFile);
            this.grpBoxSSRSReportSearch.Controls.Add(this.chkBoxUse);
            this.grpBoxSSRSReportSearch.Controls.Add(this.chkBoxMatchCase);
            this.grpBoxSSRSReportSearch.Controls.Add(this.btnCancel);
            this.grpBoxSSRSReportSearch.Controls.Add(this.lblSearch);
            this.grpBoxSSRSReportSearch.Controls.Add(this.btnOpenDialog);
            this.grpBoxSSRSReportSearch.Controls.Add(this.chkBoxMatchWholeWord);
            this.grpBoxSSRSReportSearch.Location = new System.Drawing.Point(3, 3);
            this.grpBoxSSRSReportSearch.Name = "grpBoxSSRSReportSearch";
            this.grpBoxSSRSReportSearch.Size = new System.Drawing.Size(687, 540);
            this.grpBoxSSRSReportSearch.TabIndex = 43;
            this.grpBoxSSRSReportSearch.TabStop = false;
            // 
            // lblSSISSearchTitle
            // 
            this.lblSSISSearchTitle.AutoSize = true;
            this.lblSSISSearchTitle.Font = new System.Drawing.Font("Cambria", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSSISSearchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(159)))), ((int)(((byte)(216)))));
            this.lblSSISSearchTitle.Location = new System.Drawing.Point(155, 40);
            this.lblSSISSearchTitle.Name = "lblSSISSearchTitle";
            this.lblSSISSearchTitle.Size = new System.Drawing.Size(356, 47);
            this.lblSSISSearchTitle.TabIndex = 45;
            this.lblSSISSearchTitle.Text = "SSRS Report Search";
            // 
            // statusBar
            // 
            this.statusBar.AutoSize = false;
            this.statusBar.BackColor = System.Drawing.Color.Transparent;
            this.statusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLbl});
            this.statusBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusBar.Location = new System.Drawing.Point(0, 515);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(685, 22);
            this.statusBar.TabIndex = 43;
            // 
            // toolStripStatusLbl
            // 
            this.toolStripStatusLbl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripStatusLbl.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLbl.Name = "toolStripStatusLbl";
            this.toolStripStatusLbl.Size = new System.Drawing.Size(0, 17);
            // 
            // fdlgFile
            // 
            this.fdlgFile.FileName = "ofdlgFile";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReset.BackgroundImage")));
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(297, 364);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(103, 35);
            this.btnReset.TabIndex = 41;
            this.btnReset.Text = "RESET";
            this.toolTipResetButton.SetToolTip(this.btnReset, "Button to reset all the fileds.");
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSubmit.BackgroundImage = global::SSIS_Package_Reader.Properties.Resources.BackGround;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(137, 364);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(103, 35);
            this.btnSubmit.TabIndex = 40;
            this.btnSubmit.Text = "SEARCH";
            this.toolTipSearchButton.SetToolTip(this.btnSubmit, "Button to start the search process.");
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCancel.BackgroundImage = global::SSIS_Package_Reader.Properties.Resources.BackGround;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(453, 364);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 35);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "CANCEL";
            this.toolTipCancelButton.SetToolTip(this.btnCancel, "Button to stop the cancel process.");
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOpenDialog
            // 
            this.btnOpenDialog.BackColor = System.Drawing.Color.Black;
            this.btnOpenDialog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpenDialog.BackgroundImage")));
            this.btnOpenDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenDialog.ForeColor = System.Drawing.Color.White;
            this.btnOpenDialog.Location = new System.Drawing.Point(553, 177);
            this.btnOpenDialog.Name = "btnOpenDialog";
            this.btnOpenDialog.Size = new System.Drawing.Size(40, 35);
            this.btnOpenDialog.TabIndex = 31;
            this.btnOpenDialog.Text = "...";
            this.toolTipSelectFile.SetToolTip(this.btnOpenDialog, "Button to open file or folder dialogue.");
            this.btnOpenDialog.UseVisualStyleBackColor = false;
            this.btnOpenDialog.Click += new System.EventHandler(this.btnOpenDialog_Click);
            // 
            // SSRSSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.grpBoxSSRSReportSearch);
            this.Name = "SSRSSearch";
            this.Size = new System.Drawing.Size(690, 542);
            this.toolTipFileRadioButton.SetToolTip(this, "Check this button, if you want to search a single file.");
            this.grpBoxSSRSReportSearch.ResumeLayout(false);
            this.grpBoxSSRSReportSearch.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cmbExpressionTypes;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox chkBoxUse;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOpenDialog;
        private System.Windows.Forms.CheckBox chkBoxMatchWholeWord;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.CheckBox chkBoxMatchCase;
        private System.Windows.Forms.RadioButton rbtnFile;
        private System.Windows.Forms.TextBox txtFileFolderName;
        private System.Windows.Forms.TextBox txtSearchString;
        private System.Windows.Forms.RadioButton rbtnFolder;
        private System.Windows.Forms.GroupBox grpBoxSSRSReportSearch;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLbl;
        private System.Windows.Forms.OpenFileDialog fdlgFile;
        private System.Windows.Forms.ToolTip toolTipResetButton;
        private System.Windows.Forms.ToolTip toolTipCancelButton;
        private System.Windows.Forms.ToolTip toolTipSearchTxtBox;
        private System.Windows.Forms.ToolTip toolTipFolderRadio;
        private System.Windows.Forms.ToolTip toolTipFileRadioButton;
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
        private System.Windows.Forms.SaveFileDialog saveFdlg;
        private System.Windows.Forms.ToolTip toolTipSelectFile;
        private System.Windows.Forms.ToolTip toolTipPackageLocationName;
        private System.Windows.Forms.ToolTip toolTipSearchButton;
        private System.Windows.Forms.ToolTip toolTipMatchCase;
        private System.Windows.Forms.ToolTip toolTipMatchWholeWord;
        private System.Windows.Forms.ToolTip toolTipUseCheckBox;
        private System.Windows.Forms.ToolTip toolTipExpressionTypeComboBox;
        private System.Windows.Forms.Label lblSSISSearchTitle;
    }
}
