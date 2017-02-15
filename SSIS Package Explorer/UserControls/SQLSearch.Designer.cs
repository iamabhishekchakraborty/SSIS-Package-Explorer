namespace SSIS_Package_Reader.UserControls
{
    partial class SQLSearch
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
            PresentationControls.CheckBoxProperties checkBoxProperties2 = new PresentationControls.CheckBoxProperties();
            this.toolTipConnectDB = new System.Windows.Forms.ToolTip(this.components);
            this.grpBoxSQLSearch = new System.Windows.Forms.GroupBox();
            this.chkRegularExpression = new System.Windows.Forms.CheckBox();
            this.chkWholeword = new System.Windows.Forms.CheckBox();
            this.statusStripResults = new System.Windows.Forms.StatusStrip();
            this.filterStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.showAllLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmbDatabases = new PresentationControls.CheckBoxComboBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.gridViewResult = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Preview = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemaName = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Database = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ServerName = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.ObjectType = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.Property = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.toolTipTxtSearch = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipCmbSelectDB = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipBtnSearch = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipCmbSelectSQLServerObj = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipResultGrid = new System.Windows.Forms.ToolTip(this.components);
            this.intialBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grpBoxSQLSearch.SuspendLayout();
            this.statusStripResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intialBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBoxSQLSearch
            // 
            this.grpBoxSQLSearch.Controls.Add(this.chkRegularExpression);
            this.grpBoxSQLSearch.Controls.Add(this.chkWholeword);
            this.grpBoxSQLSearch.Controls.Add(this.statusStripResults);
            this.grpBoxSQLSearch.Controls.Add(this.cmbDatabases);
            this.grpBoxSQLSearch.Controls.Add(this.lblDatabase);
            this.grpBoxSQLSearch.Controls.Add(this.gridViewResult);
            this.grpBoxSQLSearch.Controls.Add(this.btnSearch);
            this.grpBoxSQLSearch.Controls.Add(this.lblSearch);
            this.grpBoxSQLSearch.Controls.Add(this.txtSearch);
            this.grpBoxSQLSearch.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxSQLSearch.Location = new System.Drawing.Point(0, 0);
            this.grpBoxSQLSearch.Name = "grpBoxSQLSearch";
            this.grpBoxSQLSearch.Size = new System.Drawing.Size(690, 541);
            this.grpBoxSQLSearch.TabIndex = 16;
            this.grpBoxSQLSearch.TabStop = false;
            this.grpBoxSQLSearch.Text = "SQL Server Search";
            // 
            // chkRegularExpression
            // 
            this.chkRegularExpression.AutoSize = true;
            this.chkRegularExpression.Location = new System.Drawing.Point(140, 48);
            this.chkRegularExpression.Name = "chkRegularExpression";
            this.chkRegularExpression.Size = new System.Drawing.Size(148, 18);
            this.chkRegularExpression.TabIndex = 50;
            this.chkRegularExpression.Text = "Use Regular Expression";
            this.chkRegularExpression.UseVisualStyleBackColor = true;
            this.chkRegularExpression.CheckedChanged += new System.EventHandler(this.chkRegularExpression_CheckedChanged);
            // 
            // chkWholeword
            // 
            this.chkWholeword.AutoSize = true;
            this.chkWholeword.Location = new System.Drawing.Point(9, 47);
            this.chkWholeword.Name = "chkWholeword";
            this.chkWholeword.Size = new System.Drawing.Size(125, 18);
            this.chkWholeword.TabIndex = 49;
            this.chkWholeword.Text = "Match Whole Word";
            this.chkWholeword.UseVisualStyleBackColor = true;
            this.chkWholeword.CheckedChanged += new System.EventHandler(this.chkWholeword_CheckedChanged);
            // 
            // statusStripResults
            // 
            this.statusStripResults.AutoSize = false;
            this.statusStripResults.BackColor = System.Drawing.Color.White;
            this.statusStripResults.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStripResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterStatusLabel,
            this.showAllLabel});
            this.statusStripResults.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStripResults.Location = new System.Drawing.Point(412, 47);
            this.statusStripResults.Name = "statusStripResults";
            this.statusStripResults.Size = new System.Drawing.Size(273, 19);
            this.statusStripResults.SizingGrip = false;
            this.statusStripResults.TabIndex = 48;
            this.statusStripResults.Text = "statusStrip1";
            // 
            // filterStatusLabel
            // 
            this.filterStatusLabel.Name = "filterStatusLabel";
            this.filterStatusLabel.Size = new System.Drawing.Size(0, 14);
            this.filterStatusLabel.Visible = false;
            // 
            // showAllLabel
            // 
            this.showAllLabel.IsLink = true;
            this.showAllLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.showAllLabel.Name = "showAllLabel";
            this.showAllLabel.Size = new System.Drawing.Size(53, 14);
            this.showAllLabel.Text = "Show &All";
            this.showAllLabel.Visible = false;
            this.showAllLabel.Click += new System.EventHandler(this.showAllLabel_Click);
            // 
            // cmbDatabases
            // 
            checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbDatabases.CheckBoxProperties = checkBoxProperties2;
            this.cmbDatabases.DisplayMemberSingleItem = "";
            this.cmbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(365, 18);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(151, 22);
            this.cmbDatabases.TabIndex = 47;
            this.toolTipCmbSelectDB.SetToolTip(this.cmbDatabases, "Use this combobox to select the database(s).");
            this.cmbDatabases.CheckBoxCheckedChanged += new System.EventHandler(this.cmbDatabases_CheckBoxCheckedChanged);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Location = new System.Drawing.Point(299, 20);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(70, 15);
            this.lblDatabase.TabIndex = 43;
            this.lblDatabase.Text = "Database: ";
            // 
            // gridViewResult
            // 
            this.gridViewResult.AllowUserToAddRows = false;
            this.gridViewResult.AllowUserToDeleteRows = false;
            this.gridViewResult.AllowUserToOrderColumns = true;
            this.gridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.Preview,
            this.ObjectName,
            this.SchemaName,
            this.Database,
            this.ServerName,
            this.ObjectType,
            this.Property});
            this.gridViewResult.Location = new System.Drawing.Point(4, 69);
            this.gridViewResult.Name = "gridViewResult";
            this.gridViewResult.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.gridViewResult.RowHeadersVisible = false;
            this.gridViewResult.Size = new System.Drawing.Size(681, 467);
            this.gridViewResult.TabIndex = 42;
            this.toolTipResultGrid.SetToolTip(this.gridViewResult, "Grid to display the saerch result.");
            this.gridViewResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewResult_CellContentClick);
            this.gridViewResult.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridViewResult_DataBindingComplete);
            this.gridViewResult.BindingContextChanged += new System.EventHandler(this.gridViewResult_BindingContextChanged);
            this.gridViewResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridViewResult_KeyDown);
            // 
            // Select
            // 
            this.Select.Frozen = true;
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Select.Visible = false;
            this.Select.Width = 50;
            // 
            // Preview
            // 
            this.Preview.Frozen = true;
            this.Preview.HeaderText = "Preview";
            this.Preview.Name = "Preview";
            this.Preview.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Preview.Text = "Preview";
            this.Preview.ToolTipText = "Column to get the preview of object script.";
            this.Preview.Width = 80;
            // 
            // ObjectName
            // 
            this.ObjectName.HeaderText = "Object Name";
            this.ObjectName.Name = "ObjectName";
            this.ObjectName.ReadOnly = true;
            this.ObjectName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ObjectName.ToolTipText = "Object consisting the search keyword.";
            this.ObjectName.Width = 160;
            // 
            // SchemaName
            // 
            this.SchemaName.DropDownListBoxMaxLines = 30;
            this.SchemaName.HeaderText = "Schema Name";
            this.SchemaName.Name = "SchemaName";
            this.SchemaName.ReadOnly = true;
            this.SchemaName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SchemaName.ToolTipText = "Schema name of the search.";
            this.SchemaName.Width = 120;
            // 
            // Database
            // 
            this.Database.DropDownListBoxMaxLines = 30;
            this.Database.HeaderText = "Database";
            this.Database.Name = "Database";
            this.Database.ReadOnly = true;
            this.Database.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Database.ToolTipText = "Database of the object found.";
            // 
            // ServerName
            // 
            this.ServerName.DropDownListBoxMaxLines = 30;
            this.ServerName.HeaderText = "Server Name";
            this.ServerName.Name = "ServerName";
            this.ServerName.ReadOnly = true;
            this.ServerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ServerName.ToolTipText = "Server name of the object.";
            this.ServerName.Visible = false;
            this.ServerName.Width = 120;
            // 
            // ObjectType
            // 
            this.ObjectType.HeaderText = "Object Type";
            this.ObjectType.Name = "ObjectType";
            this.ObjectType.ReadOnly = true;
            this.ObjectType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ObjectType.ToolTipText = "Type of the object.";
            this.ObjectType.Width = 160;
            // 
            // Property
            // 
            this.Property.HeaderText = "Property";
            this.Property.Name = "Property";
            this.Property.ReadOnly = true;
            this.Property.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Property.ToolTipText = "Property where the keyword found.";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.BackgroundImage = global::SSIS_Package_Reader.Properties.Resources.BackGround;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(552, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(88, 27);
            this.btnSearch.TabIndex = 41;
            this.btnSearch.Text = "SEARCH";
            this.toolTipBtnSearch.SetToolTip(this.btnSearch, "Button to start search process.");
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(6, 22);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(55, 15);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search: ";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(67, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(213, 22);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "sep_retailstore";
            this.toolTipTxtSearch.SetToolTip(this.txtSearch, "Textbox to enter search keyword.");
            // 
            // SQLSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.grpBoxSQLSearch);
            this.Name = "SQLSearch";
            this.Size = new System.Drawing.Size(690, 542);
            this.Load += new System.EventHandler(this.SQLSearch_Load);
            this.EnabledChanged += new System.EventHandler(this.SQLSearch_EnabledChanged);
            this.grpBoxSQLSearch.ResumeLayout(false);
            this.grpBoxSQLSearch.PerformLayout();
            this.statusStripResults.ResumeLayout(false);
            this.statusStripResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intialBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTipConnectDB;
        private System.Windows.Forms.GroupBox grpBoxSQLSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView gridViewResult;
        private System.Windows.Forms.Label lblDatabase;
        private PresentationControls.CheckBoxComboBox cmbDatabases;
        private System.Windows.Forms.ToolTip toolTipTxtSearch;
        private System.Windows.Forms.ToolTip toolTipCmbSelectDB;
        private System.Windows.Forms.ToolTip toolTipBtnSearch;
        private System.Windows.Forms.ToolTip toolTipCmbSelectSQLServerObj;
        private System.Windows.Forms.ToolTip toolTipResultGrid;
        private System.Windows.Forms.BindingSource intialBindingSource;
        private System.Windows.Forms.StatusStrip statusStripResults;
        private System.Windows.Forms.ToolStripStatusLabel filterStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel showAllLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewButtonColumn Preview;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectName;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn SchemaName;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Database;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ServerName;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn ObjectType;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn Property;
        private System.Windows.Forms.CheckBox chkRegularExpression;
        private System.Windows.Forms.CheckBox chkWholeword;
    }
}
