namespace SSIS_Package_Reader
{
    partial class FormScriptPreview
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScriptPreview));
            this.toolTipUp = new System.Windows.Forms.ToolTip(this.components);
            this.btnUp = new System.Windows.Forms.Button();
            this.toolTipNext = new System.Windows.Forms.ToolTip(this.components);
            this.btnNext = new System.Windows.Forms.Button();
            this.toolTipReplace = new System.Windows.Forms.ToolTip(this.components);
            this.btnReplace = new System.Windows.Forms.Button();
            this.toolTipSave = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTipDown = new System.Windows.Forms.ToolTip(this.components);
            this.btnDown = new System.Windows.Forms.Button();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.lblFind = new System.Windows.Forms.Label();
            this.saveFdlgSaveScript = new System.Windows.Forms.SaveFileDialog();
            this.lblReplace = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.lblMatchesFound = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Location = new System.Drawing.Point(897, 5);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(46, 43);
            this.btnUp.TabIndex = 23;
            this.toolTipUp.SetToolTip(this.btnUp, "Click to find previous occurence of searched pattern");
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnNext.BackgroundImage = global::SSIS_Package_Reader.Properties.Resources.Capture;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(344, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(57, 28);
            this.btnNext.TabIndex = 27;
            this.btnNext.Text = "NEXT";
            this.toolTipNext.SetToolTip(this.btnNext, "Click to find the string");
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReplace.BackgroundImage = global::SSIS_Package_Reader.Properties.Resources.Capture;
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplace.ForeColor = System.Drawing.Color.White;
            this.btnReplace.Location = new System.Drawing.Point(761, 12);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(78, 28);
            this.btnReplace.TabIndex = 31;
            this.btnReplace.Text = "REPLACE";
            this.toolTipReplace.SetToolTip(this.btnReplace, "Click to replace a string");
            this.btnReplace.UseVisualStyleBackColor = false;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Location = new System.Drawing.Point(920, 49);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 34);
            this.btnSave.TabIndex = 20;
            this.toolTipSave.SetToolTip(this.btnSave, "Click to save the script");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Location = new System.Drawing.Point(845, 5);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(46, 43);
            this.btnDown.TabIndex = 24;
            this.toolTipDown.SetToolTip(this.btnDown, "Click to find next occurence of pattern searched");
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(468, 17);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(286, 20);
            this.txtReplace.TabIndex = 30;
            this.txtReplace.TextChanged += new System.EventHandler(this.txtReplace_TextChanged);
            // 
            // lblFind
            // 
            this.lblFind.AutoSize = true;
            this.lblFind.BackColor = System.Drawing.Color.Transparent;
            this.lblFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFind.Location = new System.Drawing.Point(13, 20);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(39, 13);
            this.lblFind.TabIndex = 25;
            this.lblFind.Text = "Find :";
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.BackColor = System.Drawing.Color.Transparent;
            this.lblReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplace.Location = new System.Drawing.Point(408, 20);
            this.lblReplace.Name = "lblReplace";
            this.lblReplace.Size = new System.Drawing.Size(62, 13);
            this.lblReplace.TabIndex = 29;
            this.lblReplace.Text = "Replace :";
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(52, 17);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(286, 20);
            this.txtFind.TabIndex = 26;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // txtScript
            // 
            this.txtScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScript.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScript.HideSelection = false;
            this.txtScript.Location = new System.Drawing.Point(6, 85);
            this.txtScript.Name = "txtScript";
            this.txtScript.ReadOnly = true;
            this.txtScript.Size = new System.Drawing.Size(949, 462);
            this.txtScript.TabIndex = 22;
            this.txtScript.Text = "";
            this.txtScript.WordWrap = false;
            // 
            // lblMatchesFound
            // 
            this.lblMatchesFound.AutoSize = true;
            this.lblMatchesFound.Location = new System.Drawing.Point(56, 45);
            this.lblMatchesFound.Name = "lblMatchesFound";
            this.lblMatchesFound.Size = new System.Drawing.Size(0, 13);
            this.lblMatchesFound.TabIndex = 28;
            // 
            // FormScriptPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 552);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.lblFind);
            this.Controls.Add(this.lblReplace);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.lblMatchesFound);
            this.MinimumSize = new System.Drawing.Size(978, 590);
            this.Name = "FormScriptPreview";
            this.Text = "Script Preview";
            this.Load += new System.EventHandler(this.FormScriptPreview_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTipUp;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.ToolTip toolTipNext;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ToolTip toolTipReplace;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.ToolTip toolTipSave;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTipDown;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.SaveFileDialog saveFdlgSaveScript;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.RichTextBox txtScript;
        private System.Windows.Forms.Label lblMatchesFound;
    }
}