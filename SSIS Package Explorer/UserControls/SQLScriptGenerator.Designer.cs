namespace SSIS_Package_Reader.UserControls
{
    partial class SQLScriptGenerator
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
            this.grpBoxSQLScriptGenerator = new System.Windows.Forms.GroupBox();
            this.cmbDatabases = new System.Windows.Forms.ComboBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cmbDBObjects = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.fbdFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.grpBoxSQLScriptGenerator.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxSQLScriptGenerator
            // 
            this.grpBoxSQLScriptGenerator.Controls.Add(this.statusBar);
            this.grpBoxSQLScriptGenerator.Controls.Add(this.cmbDatabases);
            this.grpBoxSQLScriptGenerator.Controls.Add(this.lblDatabase);
            this.grpBoxSQLScriptGenerator.Controls.Add(this.btnGenerate);
            this.grpBoxSQLScriptGenerator.Controls.Add(this.cmbDBObjects);
            this.grpBoxSQLScriptGenerator.Controls.Add(this.label1);
            this.grpBoxSQLScriptGenerator.Location = new System.Drawing.Point(3, 3);
            this.grpBoxSQLScriptGenerator.Name = "grpBoxSQLScriptGenerator";
            this.grpBoxSQLScriptGenerator.Size = new System.Drawing.Size(684, 536);
            this.grpBoxSQLScriptGenerator.TabIndex = 0;
            this.grpBoxSQLScriptGenerator.TabStop = false;
            this.grpBoxSQLScriptGenerator.Text = "SQL Script Generator";
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(229, 79);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(244, 21);
            this.cmbDatabases.TabIndex = 44;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Location = new System.Drawing.Point(118, 82);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(65, 13);
            this.lblDatabase.TabIndex = 43;
            this.lblDatabase.Text = "Database:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGenerate.BackgroundImage = global::SSIS_Package_Reader.Properties.Resources.BackGround;
            this.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(254, 261);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(143, 27);
            this.btnGenerate.TabIndex = 42;
            this.btnGenerate.Text = "GENERATE SCRIPTS";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbDBObjects
            // 
            this.cmbDBObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDBObjects.FormattingEnabled = true;
            this.cmbDBObjects.Items.AddRange(new object[] {
            "FUNCTION",
            "STORED_PROCEDURE",
            "USER_TABLE",
            "VIEW",
            "SYNONYM",
            "TYPE_TABLE"});
            this.cmbDBObjects.Location = new System.Drawing.Point(229, 138);
            this.cmbDBObjects.Name = "cmbDBObjects";
            this.cmbDBObjects.Size = new System.Drawing.Size(244, 21);
            this.cmbDBObjects.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL Object Type:";
            // 
            // statusBar
            // 
            this.statusBar.AutoSize = false;
            this.statusBar.BackColor = System.Drawing.Color.Transparent;
            this.statusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLbl});
            this.statusBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusBar.Location = new System.Drawing.Point(2, 511);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(685, 22);
            this.statusBar.TabIndex = 45;
            // 
            // toolStripStatusLbl
            // 
            this.toolStripStatusLbl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripStatusLbl.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLbl.Name = "toolStripStatusLbl";
            this.toolStripStatusLbl.Size = new System.Drawing.Size(0, 17);
            // 
            // SQLScriptGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBoxSQLScriptGenerator);
            this.Name = "SQLScriptGenerator";
            this.Size = new System.Drawing.Size(690, 542);
            this.EnabledChanged += new System.EventHandler(this.SQLScriptGenerator_EnabledChanged);
            this.grpBoxSQLScriptGenerator.ResumeLayout(false);
            this.grpBoxSQLScriptGenerator.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxSQLScriptGenerator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDBObjects;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ComboBox cmbDatabases;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLbl;
        private System.Windows.Forms.FolderBrowserDialog fbdFolder;
    }
}
