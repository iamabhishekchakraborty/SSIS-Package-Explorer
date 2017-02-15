using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;

namespace SSIS_Package_Reader
{
    public partial class FormScriptPreview : Form
    {
        public string SearchString;
        public StringCollection ObjectScript = new StringCollection();
        public bool IsWholeMatch = false;
        public bool IsRegex = false;
        private Dictionary<int, int> patternIndexes = new Dictionary<int, int>();
        private List<int> manualFindIndexes = new List<int>();

        private string Script
        {
            get
            {
                string script = "";

                foreach (var item in ObjectScript)
                {
                    script += item.ToString() + "\n";
                }
                return script;
            }
        }

        public FormScriptPreview()
        {
            InitializeComponent();
        }

        private void FormScriptPreview_Load(object sender, EventArgs e)
        {
            //Intializing the cursor position, so that while clicking the UP or DOWN button will be easily manageble.
            txtScript.SelectionStart = 0;

            txtScript.Text = Script;

            FindAndMarkText();

            txtScript.Select(0, 0);
            PreloadActivities();
        }

        private void PreloadActivities()
        {
            btnNext.Enabled = false;
            btnReplace.Enabled = false;
            txtFind.Text = "";
        }

        private void FindAndMarkText()
        {
            int cursorPos = txtScript.SelectionStart;

            patternIndexes = FindAllIndexes(txtScript.Text, SearchString);

            RichTextBox temp = new RichTextBox();
            temp.Rtf = txtScript.Rtf;

            foreach (var item in patternIndexes)
            {
                temp.Select(item.Key, item.Value);
                temp.SelectionBackColor = Color.Yellow;
            }

            temp.Select(cursorPos, 0);

            txtScript.Rtf = temp.Rtf;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            AutoSearchDown();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            AutoSearchUp();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ManualSearch();
        }

        private void AutoSearchUp()
        {
            if (patternIndexes.Count > 0)
            {
                if (txtScript.SelectionStart <= patternIndexes.ElementAt(0).Key)
                {
                    txtScript.SelectionStart = 0;
                }
            }

            for (int cursor = patternIndexes.Count - 1; cursor >= 0; cursor--)
            {
                KeyValuePair<int, int> index = patternIndexes.ElementAt(cursor);
                if (index.Key < txtScript.SelectionStart)
                {
                    txtScript.SelectionStart = index.Key;
                    txtScript.Focus();
                    return;
                }
            }
        }

        private void AutoSearchDown()
        {
            if (patternIndexes.Count > 0)
            {
                if (txtScript.SelectionStart >= patternIndexes.ElementAt(patternIndexes.Count - 1).Key)
                {
                    txtScript.SelectionStart = 0;
                }
            }

            foreach (var cursor in patternIndexes)
            {
                if (cursor.Key > txtScript.SelectionStart)
                {
                    txtScript.SelectionStart = cursor.Key;
                    txtScript.Focus();
                    return;
                }
            }
        }

        private void ManualSearch()
        {
            if (manualFindIndexes.Count > 0)
            {
                if (txtScript.SelectionStart >= manualFindIndexes[manualFindIndexes.Count - 1])
                {
                    txtScript.SelectionStart = 0;
                }
            }

            foreach (int cursor in manualFindIndexes)
            {
                if (cursor > txtScript.SelectionStart)
                {
                    txtScript.Select(cursor, txtFind.Text.Length);
                    return;
                }
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            manualFindIndexes = ManualFindAllIndexes(txtScript.Text, txtFind.Text);

            if (manualFindIndexes.Count > 0)
            {
                btnNext.Enabled = true;
                lblMatchesFound.Text = manualFindIndexes.Count + " matches";
            }
            else
            {
                btnNext.Enabled = false;
                lblMatchesFound.Text = "";
            }
        }

        private Dictionary<int, int> FindAllIndexes(string str, string pattern)
        {
            Dictionary<int, int> indexes = new Dictionary<int, int>();

            if (pattern != null)
            {
                if (pattern != "")
                {
                    if (IsWholeMatch)
                    {
                        foreach (Match match in Regex.Matches(str, @"(?<!\w)" + Regex.Escape(pattern) + @"(?!\w)", RegexOptions.IgnoreCase))
                        {
                            indexes.Add(match.Index, match.Length);
                        }
                    }
                    else if (IsRegex)
                    {
                        foreach (Match match in Regex.Matches(str, pattern, RegexOptions.IgnoreCase))
                        {
                            indexes.Add(match.Index, match.Length);
                        }
                    }
                    else
                    {
                        int prevIndex = 0; // so we start at index 0
                        int index;
                        while ((index = str.IndexOf(pattern, prevIndex, StringComparison.InvariantCultureIgnoreCase)) != -1)
                        {
                            prevIndex = index + pattern.Length + 1;
                            indexes.Add(index, pattern.Length);
                        }
                    }
                }
            }
            return indexes;
        }

        private List<int> ManualFindAllIndexes(string str, string pattern)
        {
            List<int> indexes = new List<int>();

            if (pattern != "")
            {
                if (pattern != null)
                {
                    int prevIndex = 0; // so we start at index 0
                    int index;
                    while ((index = str.IndexOf(pattern, prevIndex, StringComparison.InvariantCultureIgnoreCase)) != -1)
                    {
                        prevIndex = index + pattern.Length + 1;
                        indexes.Add(index);
                    }
                }
            }
            return indexes;
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txtFind.Focused)
            {
                ManualSearch();
            }
            else if (e.KeyValue == 13 && txtReplace.Focused)
            {
                btnReplace.PerformClick();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                txtFind.Focus();
                txtFind.SelectAll();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.R))
            {
                txtReplace.Focus();
                txtReplace.SelectAll();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                btnSave.PerformClick();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtReplace_TextChanged(object sender, EventArgs e)
        {
            if (txtReplace.Text.Length > 0)
            {
                btnReplace.Enabled = true;
            }
            else
            {
                btnReplace.Enabled = false;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (txtScript.SelectedText.Length == 0 || txtScript.SelectedText.ToUpper() != txtFind.Text.ToUpper())
            {
                ManualSearch();
            }
            else if (txtScript.SelectedText.ToUpper() == txtFind.Text.ToUpper())
            {
                txtScript.SelectedText = txtReplace.Text;
                //txtScript.Rtf = txtScript.Rtf.Replace(txtScript.SelectedText, txtReplace.Text);
                manualFindIndexes = ManualFindAllIndexes(txtScript.Text, txtFind.Text);

                if (manualFindIndexes.Count > 0)
                {
                    btnNext.Enabled = true;
                    lblMatchesFound.Text = manualFindIndexes.Count + " matches";
                }
                else
                {
                    btnNext.Enabled = false;
                    lblMatchesFound.Text = "";
                }

                FindAndMarkText();

                ManualSearch();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFdlgSaveScript.Filter = "SQL Files|*.sql|Text Files|*.txt";

            if (saveFdlgSaveScript.ShowDialog() == DialogResult.OK)
            {

                bool writeSucess = false;

                writeSucess = WriteToFile(saveFdlgSaveScript.FileName, txtScript.Text);

                if (writeSucess)
                {
                    MessageBox.Show("Successfully written into file.", "SQL Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private bool WriteToFile(string file, string text)
        {
            //If you want to change any data, also check in the method "WriteToExcelFile"
            try
            {
                text.Replace("\n", Environment.NewLine);
                using (var stream = File.CreateText(file))
                {
                    stream.Write(text);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing into file: " + ex.Message, "SQL search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }
    }
}
