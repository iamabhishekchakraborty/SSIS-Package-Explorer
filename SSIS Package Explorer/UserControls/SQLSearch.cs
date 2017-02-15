using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.Data.SqlClient;
using PresentationControls;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;
using DataGridViewAutoFilter;
using System.Text.RegularExpressions;


namespace SSIS_Package_Reader.UserControls
{
    public partial class SQLSearch : UserControl
    {
        private List<SQLObject> allObjects = new List<SQLObject>();
        private List<SQLObject> searchedObjects = new List<SQLObject>();
        private List<SQLObject> filteredObjects = new List<SQLObject>();
        private List<string> selectedDatabases = new List<string>();
        private string searchString = "";

        public SQLSearch()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.gridViewResult.AutoResizeColumns();
        }

        private void SQLSearch_Load(object sender, EventArgs e)
        {
            filteredObjects = new List<SQLObject>();
            BindDataToGrid();
        }

        private void SQLSearch_EnabledChanged(object sender, EventArgs e)
        {

            if (this.Enabled)
            {
                txtSearch.Text = "";
                cmbDatabases.DataSource = null;
                cmbDatabases.Clear();

                if (ConnectionInfo.Databases.Count > 0)
                {
                    cmbDatabases.Items.Add("Select All");
                }

                foreach (DatabaseInfo database in ConnectionInfo.Databases)
                {
                    cmbDatabases.Items.Add(database.Name);
                }
            }
            else
            {
                txtSearch.Text = "";
                cmbDatabases.SelectedIndex = -1;
                cmbDatabases.Clear();

                searchedObjects = new List<SQLObject>();
                BindDataToGrid();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            selectedDatabases = new List<string>();

            if (txtSearch.Text == "")
            {
                MessageBox.Show("Please enter the search text.");
                return;
            }
            else if (txtSearch.Text.Length < 3)
            {
                MessageBox.Show("Search text must contain minimum of 3 characters.");
                return;
            }

            foreach (var item in cmbDatabases.CheckBoxItems)
            {
                if (item.Checked && item.Text != "Select All" && item.Text != "")
                {
                    selectedDatabases.Add(item.Text);
                }
            }

            if (selectedDatabases.Count == 0)
            {
                MessageBox.Show("Select atleast one database from the list");
                return;
            }

            allObjects = new List<SQLObject>();
            searchedObjects = new List<SQLObject>();
            filteredObjects = new List<SQLObject>();

            foreach (string database in selectedDatabases)
            {
                FetchObjects(database);
            }

            //Setting the search string
            searchString = txtSearch.Text;

            SearchObjects();

            if (searchedObjects.Count == 0)
            {
                MessageBox.Show("No objects found.");
            }
            BindDataToGrid();

        }

        private void BindDataToGrid()
        {
            gridViewResult.Columns["ObjectName"].DataPropertyName = "ObjectName";
            gridViewResult.Columns["SchemaName"].DataPropertyName = "SchemaName";
            gridViewResult.Columns["Database"].DataPropertyName = "Database";
            gridViewResult.Columns["ServerName"].DataPropertyName = "Server";
            gridViewResult.Columns["ObjectType"].DataPropertyName = "ObjectType";
            gridViewResult.Columns["Property"].DataPropertyName = "Property";
            gridViewResult.Columns["Preview"].DataPropertyName = "ButtonText";

            //Grid data source must be a binding source,else the filter is not enabled.
            BindingSource source = new BindingSource(CommonMethods.ToDataTable<SQLObject>(searchedObjects), null);
            gridViewResult.DataSource = source;
            gridViewResult.Refresh();
        }

        private void FetchObjects(string database)
        {
            SqlConnection conn = null;
            string windowsAuthConnectionString = "Data Source={0};Initial Catalog={1};Persist Security Info=True;integrated security=true;";
            string sqlAuthConnectionString = "Data Source={0};Initial Catalog={1};User Id={2};Password={3};";

            string connectionString = "";

            if (ConnectionInfo.Authentication == CommonEnum.AuthenticationMode.WindowsAuthentication)
            {
                connectionString = String.Format(windowsAuthConnectionString, ConnectionInfo.ServerName, database);
            }
            else if (ConnectionInfo.Authentication == CommonEnum.AuthenticationMode.SQLServerAuthentication)
            {
                connectionString = String.Format(sqlAuthConnectionString, ConnectionInfo.ServerName,
                                                database,
                                                ConnectionInfo.UserName,
                                                ConnectionInfo.Password);
            }

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string CmdString = "SELECT	  name AS ObjectName , SCHEMA_NAME(schema_id) SchemaName" +
                                   "        , DB_NAME() [Database], @@SERVERNAME [Server]" +
                                   "        , type ShortType, type_desc ObjectType, ISNULL(OBJECT_DEFINITION(object_id),'') [Definition] " +
                                   "FROM	sys.objects " +
                                   "WHERE type IN ('U','P','FN','IF','TF','V','SN','TR')" +

                                   "UNION ALL " +

                                   "SELECT	  TT.name AS ObjectName, SCHEMA_NAME(TT.schema_id) SchemaName" +
                                   "        , DB_NAME() [Database], @@SERVERNAME [Server]" +
                                   "        , type ShortType, type_desc ObjectType, ISNULL(OBJECT_DEFINITION(object_id),'') [Definition] " +
                                   "FROM    sys.table_types TT " +
                                   "        INNER JOIN sys.objects O ON O.object_id = TT.type_table_object_id  ";

                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SQLObject obj = new SQLObject();
                    obj.ObjectName = reader["ObjectName"].ToString();
                    obj.SchemaName = reader["SchemaName"].ToString();
                    obj.Database = reader["Database"].ToString();
                    obj.Server = reader["Server"].ToString();
                    obj.ShortType = reader["ShortType"].ToString();
                    obj.ObjectType = reader["ObjectType"].ToString();
                    obj.Definition = reader["Definition"].ToString();
                    allObjects.Add(obj);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    conn.Close();
                }

                MessageBox.Show(ex.Message);
            }
        }

        private void SearchObjects()
        {
            searchedObjects = new List<SQLObject>();

            if (chkWholeword.Checked)
            {
                #region "Matching the names of objects"

                List<SQLObject> tempNameMatches = new List<SQLObject>(allObjects.Where(obj =>
                    Regex.Match(obj.ObjectName, @"(?<!\w)" + Regex.Escape(searchString) + @"(?!\w)", RegexOptions.IgnoreCase).Success == true));
                List<SQLObject> nameMatches = tempNameMatches.ConvertAll(match => (SQLObject)DeepClone(match));

                (from namematch in nameMatches
                 select namematch).ToList().ForEach((namematch) =>
                 {
                     namematch.Property = "Name";
                 });

                searchedObjects.AddRange(nameMatches);

                #endregion

                #region "Matching the definition of objects"
                List<SQLObject> tempDefinitionMatches = new List<SQLObject>(allObjects.Where(obj =>
                    Regex.Match(obj.Definition, @"(?<!\w)" + Regex.Escape(searchString) + @"(?!\w)", RegexOptions.IgnoreCase).Success == true));
                List<SQLObject> definitionMatches = tempDefinitionMatches.ConvertAll(match => (SQLObject)DeepClone(match));

                (from definitionMatch in definitionMatches
                 select definitionMatch).ToList().ForEach((definitionMatch) =>
                 {
                     definitionMatch.Property = "Definition";
                 });

                searchedObjects.AddRange(definitionMatches);
                #endregion
            }
            else if (chkRegularExpression.Checked)
            {
                #region "Matching the names of objects"

                List<SQLObject> tempNameMatches = new List<SQLObject>(allObjects.Where(obj => 
                    Regex.Match(obj.ObjectName, searchString, RegexOptions.IgnoreCase).Success == true));
                List<SQLObject> nameMatches = tempNameMatches.ConvertAll(match => (SQLObject)DeepClone(match));

                (from namematch in nameMatches
                 select namematch).ToList().ForEach((namematch) =>
                 {
                     namematch.Property = "Name";
                 });

                searchedObjects.AddRange(nameMatches);

                #endregion

                #region "Matching the definition of objects"
                List<SQLObject> tempDefinitionMatches = new List<SQLObject>(allObjects.Where(obj => 
                    Regex.Match(obj.Definition, searchString, RegexOptions.IgnoreCase).Success == true));
                List<SQLObject> definitionMatches = tempDefinitionMatches.ConvertAll(match => (SQLObject)DeepClone(match));

                (from definitionMatch in definitionMatches
                 select definitionMatch).ToList().ForEach((definitionMatch) =>
                 {
                     definitionMatch.Property = "Definition";
                 });

                searchedObjects.AddRange(definitionMatches);
                #endregion
            }
            else
            {
                #region "Matching the names of objects"

                List<SQLObject> tempNameMatches = new List<SQLObject>(allObjects.Where(obj => obj.ObjectName.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0));
                List<SQLObject> nameMatches = tempNameMatches.ConvertAll(match => (SQLObject)DeepClone(match));

                (from namematch in nameMatches
                 select namematch).ToList().ForEach((namematch) =>
                 {
                     namematch.Property = "Name";
                 });

                searchedObjects.AddRange(nameMatches);

                #endregion

                #region "Matching the definition of objects"
                List<SQLObject> tempDefinitionMatches = new List<SQLObject>(allObjects.Where(obj => obj.Definition.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0));
                List<SQLObject> definitionMatches = tempDefinitionMatches.ConvertAll(match => (SQLObject)DeepClone(match));

                (from definitionMatch in definitionMatches
                 select definitionMatch).ToList().ForEach((definitionMatch) =>
                 {
                     definitionMatch.Property = "Definition";
                 });

                searchedObjects.AddRange(definitionMatches);
                #endregion
            }
        }

        public static object DeepClone(object source)
        {
            MemoryStream m = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(m, source);
            m.Position = 0;
            return b.Deserialize(m);
        }

        private void gridViewResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7) //Assuming the button column as second column, if not can change the index
            {
                if (e.RowIndex > -1)
                {
                    //check if anything needs to be validated here
                    DataGridViewRow row = gridViewResult.Rows[e.RowIndex];
                    SQLObject obj = new SQLObject();
                    obj.ObjectName = row.Cells["ObjectName"].Value.ToString();
                    obj.SchemaName = row.Cells["SchemaName"].Value.ToString();
                    obj.Database = row.Cells["Database"].Value.ToString();
                    obj.Server = row.Cells["ServerName"].Value.ToString();
                    //obj.ShortType = row.Cells["ShortType"].Value.ToString();
                    obj.ObjectType = row.Cells["ObjectType"].Value.ToString();

                    FormScriptPreview frm = new FormScriptPreview();
                    frm.SearchString = searchString;
                    frm.ObjectScript = GetchObjectScript(obj);
                    frm.IsRegex = chkRegularExpression.Checked;
                    frm.IsWholeMatch = chkWholeword.Checked;

                    if (frm.ObjectScript.Count > 0)
                    {
                        frm.ShowDialog();
                    }
                }

            }
        }

        private StringCollection GetchObjectScript(SQLObject obj)
        {
            Database db = SQLServerSMO.ServerSMOObj.Databases[obj.Database];

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
                    table.AnsiNullsStatus = false;
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return table.Script(scriptOptions);
                }
                else if (obj.ObjectType == "SQL_STORED_PROCEDURE")
                {
                    StoredProcedure sp = db.StoredProcedures[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return sp.Script(scriptOptions);
                }
                else if (obj.ObjectType == "SQL_SCALAR_FUNCTION" || obj.ObjectType == "SQL_INLINE_TABLE_VALUED_FUNCTION"
                    || obj.ObjectType == "SQL_TABLE_VALUED_FUNCTION")
                {
                    UserDefinedFunction udf = db.UserDefinedFunctions[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return udf.Script(scriptOptions);
                }
                else if (obj.ObjectType == "VIEW")
                {
                    Microsoft.SqlServer.Management.Smo.View view = db.Views[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return view.Script(scriptOptions);
                }
                else if (obj.ObjectType == "SYNONYM")
                {
                    Synonym synonym = db.Synonyms[obj.ObjectName, obj.SchemaName];
                    ScriptingOptions scriptOptions = new ScriptingOptions();
                    scriptOptions.IncludeHeaders = true;
                    scriptOptions.SchemaQualify = true;
                    return synonym.Script(scriptOptions);
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
                                scriptOptions.IncludeHeaders = true;
                                scriptOptions.SchemaQualify = true;
                                return trigger.Script(scriptOptions);
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
                            scriptOptions.IncludeHeaders = true;
                            scriptOptions.SchemaQualify = true;
                            return userDefinedTT.Script(scriptOptions);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new StringCollection();
        }

        private void cmbDatabases_CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            CheckBoxComboBoxItem senderItem = (CheckBoxComboBoxItem)sender;

            if (senderItem.Text == "Select All")
            {
                if (senderItem.Checked)
                {
                    foreach (var item in cmbDatabases.CheckBoxItems)
                    {
                        if (item.Text != "Select All")
                        {
                            item.Checked = true;
                        }

                    }
                }
                else
                {
                    foreach (var item in cmbDatabases.CheckBoxItems)
                    {
                        if (item.Text != "Select All")
                        {
                            item.Checked = false;
                        }
                    }
                }
            }
        }

        private void gridViewResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
                .GetFilterStatus(gridViewResult);
            if (String.IsNullOrEmpty(filterStatus))
            {
                showAllLabel.Visible = false;
                filterStatusLabel.Visible = false;
            }
            else
            {
                showAllLabel.Visible = true;
                filterStatusLabel.Visible = true;
                filterStatusLabel.Text = filterStatus;
            }
        }

        private void gridViewResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell =
                    gridViewResult.CurrentCell.OwningColumn.HeaderCell as
                    DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        private void gridViewResult_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void showAllLabel_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(gridViewResult);
        }

        private void chkWholeword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWholeword.Checked)
            {
                chkRegularExpression.Checked = false;
            }
        }

        private void chkRegularExpression_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegularExpression.Checked)
            {
                chkWholeword.Checked = false;
            }
        }
    }
}
