namespace SSIS_Package_Reader
{
    partial class AdvancedDBObjectSearch
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlToolbarBack = new System.Windows.Forms.Panel();
            this.toolBarApplications = new System.Windows.Forms.ToolStrip();
            this.toolBtnPackageExplorer = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSQLSearch = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSSRSSearch = new System.Windows.Forms.ToolStripButton();
            this.grpBoxAll = new System.Windows.Forms.GroupBox();
            this.grpBoxConnectToServer = new System.Windows.Forms.GroupBox();
            this.lblServerName = new System.Windows.Forms.Label();
            this.btnConnectDB = new System.Windows.Forms.Button();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblAuthMode = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.cmbAutheticationMode = new System.Windows.Forms.ComboBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.ucSQLScriptGenerator = new SSIS_Package_Reader.UserControls.SQLScriptGenerator();
            this.ucSSRSSearch = new SSIS_Package_Reader.UserControls.SSRSSearch();
            this.ucSSISPackageSearch = new SSIS_Package_Reader.UserControls.PackageSearch();
            this.ucSQLSearch = new SSIS_Package_Reader.UserControls.SQLSearch();
            this.pnlToolbarBack.SuspendLayout();
            this.toolBarApplications.SuspendLayout();
            this.grpBoxAll.SuspendLayout();
            this.grpBoxConnectToServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbarBack
            // 
            this.pnlToolbarBack.BackColor = System.Drawing.Color.Transparent;
            this.pnlToolbarBack.Controls.Add(this.toolBarApplications);
            this.pnlToolbarBack.Controls.Add(this.grpBoxAll);
            this.pnlToolbarBack.Location = new System.Drawing.Point(0, -1);
            this.pnlToolbarBack.Margin = new System.Windows.Forms.Padding(0);
            this.pnlToolbarBack.Name = "pnlToolbarBack";
            this.pnlToolbarBack.Size = new System.Drawing.Size(942, 665);
            this.pnlToolbarBack.TabIndex = 17;
            // 
            // toolBarApplications
            // 
            this.toolBarApplications.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.toolBarApplications.AutoSize = false;
            this.toolBarApplications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(78)))), ((int)(((byte)(105)))));
            this.toolBarApplications.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolBarApplications.CanOverflow = false;
            this.toolBarApplications.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBarApplications.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBarApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnPackageExplorer,
            this.toolBtnSQLSearch,
            this.toolBtnSSRSSearch});
            this.toolBarApplications.Location = new System.Drawing.Point(-1, -2);
            this.toolBarApplications.Name = "toolBarApplications";
            this.toolBarApplications.Padding = new System.Windows.Forms.Padding(0);
            this.toolBarApplications.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolBarApplications.Size = new System.Drawing.Size(937, 50);
            this.toolBarApplications.TabIndex = 15;
            // 
            // toolBtnPackageExplorer
            // 
            this.toolBtnPackageExplorer.AutoSize = false;
            this.toolBtnPackageExplorer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnPackageExplorer.Image = global::SSIS_Package_Reader.Properties.Resources.SSISPackageSearch;
            this.toolBtnPackageExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnPackageExplorer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnPackageExplorer.Name = "toolBtnPackageExplorer";
            this.toolBtnPackageExplorer.Size = new System.Drawing.Size(40, 35);
            this.toolBtnPackageExplorer.ToolTipText = "Search SSIS Package File(s)";
            this.toolBtnPackageExplorer.Click += new System.EventHandler(this.toolBtnPackageExplorer_Click);
            // 
            // toolBtnSQLSearch
            // 
            this.toolBtnSQLSearch.AutoSize = false;
            this.toolBtnSQLSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSQLSearch.Image = global::SSIS_Package_Reader.Properties.Resources.SqlDBSearch;
            this.toolBtnSQLSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnSQLSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSQLSearch.Name = "toolBtnSQLSearch";
            this.toolBtnSQLSearch.Size = new System.Drawing.Size(40, 35);
            this.toolBtnSQLSearch.ToolTipText = "Search DB Objects";
            this.toolBtnSQLSearch.Click += new System.EventHandler(this.toolBtnSQLSearch_Click);
            // 
            // toolBtnSSRSSearch
            // 
            this.toolBtnSSRSSearch.AutoSize = false;
            this.toolBtnSSRSSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSSRSSearch.Image = global::SSIS_Package_Reader.Properties.Resources.SSRSReport;
            this.toolBtnSSRSSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnSSRSSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSSRSSearch.Name = "toolBtnSSRSSearch";
            this.toolBtnSSRSSearch.Size = new System.Drawing.Size(40, 35);
            this.toolBtnSSRSSearch.ToolTipText = "Search SSRS Report File(s)";
            this.toolBtnSSRSSearch.Click += new System.EventHandler(this.toolBtnSSRSSearch_Click);
            // 
            // grpBoxAll
            // 
            this.grpBoxAll.BackColor = System.Drawing.Color.Transparent;
            this.grpBoxAll.Controls.Add(this.ucSQLScriptGenerator);
            this.grpBoxAll.Controls.Add(this.ucSSRSSearch);
            this.grpBoxAll.Controls.Add(this.ucSSISPackageSearch);
            this.grpBoxAll.Controls.Add(this.ucSQLSearch);
            this.grpBoxAll.Controls.Add(this.grpBoxConnectToServer);
            this.grpBoxAll.Location = new System.Drawing.Point(5, 54);
            this.grpBoxAll.Name = "grpBoxAll";
            this.grpBoxAll.Size = new System.Drawing.Size(928, 565);
            this.grpBoxAll.TabIndex = 18;
            this.grpBoxAll.TabStop = false;
            // 
            // grpBoxConnectToServer
            // 
            this.grpBoxConnectToServer.Controls.Add(this.lblServerName);
            this.grpBoxConnectToServer.Controls.Add(this.btnConnectDB);
            this.grpBoxConnectToServer.Controls.Add(this.txtServerName);
            this.grpBoxConnectToServer.Controls.Add(this.txtPassword);
            this.grpBoxConnectToServer.Controls.Add(this.lblAuthMode);
            this.grpBoxConnectToServer.Controls.Add(this.lblPassword);
            this.grpBoxConnectToServer.Controls.Add(this.cmbAutheticationMode);
            this.grpBoxConnectToServer.Controls.Add(this.txtUserName);
            this.grpBoxConnectToServer.Controls.Add(this.lblUserName);
            this.grpBoxConnectToServer.Location = new System.Drawing.Point(7, 10);
            this.grpBoxConnectToServer.Name = "grpBoxConnectToServer";
            this.grpBoxConnectToServer.Size = new System.Drawing.Size(213, 540);
            this.grpBoxConnectToServer.TabIndex = 17;
            this.grpBoxConnectToServer.TabStop = false;
            this.grpBoxConnectToServer.Text = "Connect";
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.BackColor = System.Drawing.Color.Transparent;
            this.lblServerName.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerName.Location = new System.Drawing.Point(17, 42);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(89, 15);
            this.lblServerName.TabIndex = 7;
            this.lblServerName.Text = "Server Name:";
            // 
            // btnConnectDB
            // 
            this.btnConnectDB.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConnectDB.BackgroundImage = global::SSIS_Package_Reader.Properties.Resources.BackGround;
            this.btnConnectDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConnectDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnectDB.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectDB.ForeColor = System.Drawing.Color.White;
            this.btnConnectDB.Location = new System.Drawing.Point(49, 214);
            this.btnConnectDB.Name = "btnConnectDB";
            this.btnConnectDB.Size = new System.Drawing.Size(97, 28);
            this.btnConnectDB.TabIndex = 14;
            this.btnConnectDB.Text = "CONNECT";
            this.btnConnectDB.UseVisualStyleBackColor = false;
            this.btnConnectDB.Click += new System.EventHandler(this.btnConnectDB_Click);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(20, 59);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(161, 20);
            this.txtServerName.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(20, 179);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(161, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // lblAuthMode
            // 
            this.lblAuthMode.AutoSize = true;
            this.lblAuthMode.BackColor = System.Drawing.Color.Transparent;
            this.lblAuthMode.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthMode.Location = new System.Drawing.Point(17, 84);
            this.lblAuthMode.Name = "lblAuthMode";
            this.lblAuthMode.Size = new System.Drawing.Size(101, 15);
            this.lblAuthMode.TabIndex = 10;
            this.lblAuthMode.Text = "Authentication:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(17, 163);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(72, 15);
            this.lblPassword.TabIndex = 13;
            this.lblPassword.Text = "Password: ";
            // 
            // cmbAutheticationMode
            // 
            this.cmbAutheticationMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutheticationMode.FormattingEnabled = true;
            this.cmbAutheticationMode.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cmbAutheticationMode.Location = new System.Drawing.Point(19, 100);
            this.cmbAutheticationMode.Name = "cmbAutheticationMode";
            this.cmbAutheticationMode.Size = new System.Drawing.Size(161, 21);
            this.cmbAutheticationMode.TabIndex = 2;
            this.cmbAutheticationMode.SelectedIndexChanged += new System.EventHandler(this.cmbAutheticationMode_SelectedIndexChanged);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(20, 140);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(161, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(17, 124);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(80, 15);
            this.lblUserName.TabIndex = 11;
            this.lblUserName.Text = "User Name: ";
            // 
            // ucSQLScriptGenerator
            // 
            this.ucSQLScriptGenerator.Location = new System.Drawing.Point(227, 10);
            this.ucSQLScriptGenerator.Name = "ucSQLScriptGenerator";
            this.ucSQLScriptGenerator.Size = new System.Drawing.Size(690, 542);
            this.ucSQLScriptGenerator.TabIndex = 19;
            // 
            // ucSSRSSearch
            // 
            this.ucSSRSSearch.BackColor = System.Drawing.Color.Transparent;
            this.ucSSRSSearch.Location = new System.Drawing.Point(225, 8);
            this.ucSSRSSearch.Name = "ucSSRSSearch";
            this.ucSSRSSearch.Size = new System.Drawing.Size(690, 542);
            this.ucSSRSSearch.TabIndex = 18;
            // 
            // ucSSISPackageSearch
            // 
            this.ucSSISPackageSearch.BackColor = System.Drawing.Color.Transparent;
            this.ucSSISPackageSearch.Location = new System.Drawing.Point(225, 8);
            this.ucSSISPackageSearch.Name = "ucSSISPackageSearch";
            this.ucSSISPackageSearch.Size = new System.Drawing.Size(690, 542);
            this.ucSSISPackageSearch.TabIndex = 1;
            // 
            // ucSQLSearch
            // 
            this.ucSQLSearch.BackColor = System.Drawing.Color.Transparent;
            this.ucSQLSearch.Location = new System.Drawing.Point(226, 11);
            this.ucSQLSearch.Name = "ucSQLSearch";
            this.ucSQLSearch.Size = new System.Drawing.Size(694, 547);
            this.ucSQLSearch.TabIndex = 0;
            // 
            // AdvancedDBObjectSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 664);
            this.Controls.Add(this.pnlToolbarBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AdvancedDBObjectSearch";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced DB Object Search";
            this.pnlToolbarBack.ResumeLayout(false);
            this.toolBarApplications.ResumeLayout(false);
            this.toolBarApplications.PerformLayout();
            this.grpBoxAll.ResumeLayout(false);
            this.grpBoxConnectToServer.ResumeLayout(false);
            this.grpBoxConnectToServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolbarBack;
        private System.Windows.Forms.ToolStrip toolBarApplications;
        private System.Windows.Forms.ToolStripButton toolBtnPackageExplorer;
        private System.Windows.Forms.ToolStripButton toolBtnSQLSearch;
        private System.Windows.Forms.ToolStripButton toolBtnSSRSSearch;
        private System.Windows.Forms.GroupBox grpBoxAll;
        private UserControls.SSRSSearch ucSSRSSearch;
        private UserControls.PackageSearch ucSSISPackageSearch;
        private UserControls.SQLSearch ucSQLSearch;
        private System.Windows.Forms.GroupBox grpBoxConnectToServer;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.Button btnConnectDB;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblAuthMode;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.ComboBox cmbAutheticationMode;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private UserControls.SQLScriptGenerator ucSQLScriptGenerator;

    }
}