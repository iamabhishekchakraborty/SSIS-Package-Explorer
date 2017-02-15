using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using System.Security.Principal;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SSIS_Package_Reader
{
    public partial class AdvancedDBObjectSearch : Form
    {
        public AdvancedDBObjectSearch()
        {
            InitializeComponent();
            PreLoadActivities();
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

        private void PreLoadActivities()
        {
            cmbAutheticationMode.SelectedIndex = 0;
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
            txtUserName.Text = currentUser.Name;
            txtUserName.Enabled = false;
            txtPassword.Enabled = false;

            //Making the user controls invisible
            ucSQLSearch.Visible = false;
            ucSSISPackageSearch.Visible = false;
            ucSSRSSearch.Visible = false;
            ucSQLScriptGenerator.Visible = false;

            //Disabling the control, until it is connected to the database
            ucSQLSearch.Enabled = false;
            ucSQLScriptGenerator.Enabled = false;
        }

        private void toolBtnPackageExplorer_Click(object sender, EventArgs e)
        {
            ucSSISPackageSearch.Visible = true;
            ucSQLSearch.Visible = false;
            ucSSRSSearch.Visible = false;
            ucSQLScriptGenerator.Visible = false;
        }

        private void toolBtnSQLSearch_Click(object sender, EventArgs e)
        {
            ucSQLSearch.Visible = true;
            ucSSISPackageSearch.Visible = false;
            ucSSRSSearch.Visible = false;
            ucSQLScriptGenerator.Visible = false;
        }

        private void toolBtnSSRSSearch_Click(object sender, EventArgs e)
        {
            //ucSSRSSearch.Visible = true;
            ucSQLScriptGenerator.Visible = true;
            ucSQLSearch.Visible = false;
            ucSSISPackageSearch.Visible = false;
        }

        private void btnConnectDB_Click(object sender, EventArgs e)
        {
            if (btnConnectDB.Text == "CONNECT")
            {
                ConnectToDB();
            }
            else
            {
                DisconnectDB();
            }
        }

        private void ConnectToDB()
        {
            SqlConnection conn = null;
            string windowsAuthConnectionString = "Data Source={0};Initial Catalog=master;Persist Security Info=True;integrated security=true;";
            string sqlAuthConnectionString = "Data Source={0};Initial Catalog=master;User Id={1};Password={2};";

            string connectionString = "";

            if (cmbAutheticationMode.SelectedIndex == 0)
            {
                connectionString = String.Format(windowsAuthConnectionString, txtServerName.Text);
                ConnectionInfo.Authentication = CommonEnum.AuthenticationMode.WindowsAuthentication;
            }
            else if (cmbAutheticationMode.SelectedIndex == 1)
            {
                connectionString = String.Format(sqlAuthConnectionString, txtServerName.Text, txtUserName.Text, txtPassword.Text);
                ConnectionInfo.Authentication = CommonEnum.AuthenticationMode.SQLServerAuthentication;
            }

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string CmdString = "SELECT Name AS DBName FROM sys.databases --where HAS_DBACCESS(name)= 1 AND OWNER_SID <> 0x01";

                SqlCommand cmd = new SqlCommand(CmdString, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DatabaseInfo database = new DatabaseInfo();
                    database.Name = reader["DBName"].ToString();
                    ConnectionInfo.Databases.Add(database);
                }

                ConnectionInfo.ServerName = txtServerName.Text;
                ConnectionInfo.UserName = txtUserName.Text;
                ConnectionInfo.Password = txtPassword.Text;

                SQLServerSMO.ServerConnectionSMOObj = new ServerConnection(txtServerName.Text);
                //First check type of Authentication
                if (ConnectionInfo.Authentication == CommonEnum.AuthenticationMode.WindowsAuthentication)   //Windows Authentication
                {
                    SQLServerSMO.ServerConnectionSMOObj.LoginSecure = true;
                    SQLServerSMO.ServerSMOObj = new Server(SQLServerSMO.ServerConnectionSMOObj);
                }
                else
                {
                    // Create a new connection to the selected server name
                    SQLServerSMO.ServerConnectionSMOObj.LoginSecure = false;
                    SQLServerSMO.ServerConnectionSMOObj.Login = txtUserName.Text;       //Login User
                    //Login Password
                    SQLServerSMO.ServerConnectionSMOObj.Password = txtPassword.Text;
                    //this.m_ServerConnection.DatabaseName = this.cmbDbName.Text;  //Database Name
                    // Create a new SQL Server object using the connection we created
                    SQLServerSMO.ServerSMOObj = new Server(SQLServerSMO.ServerConnectionSMOObj);
                }

                btnConnectDB.Text = "DISCONNECT";
                
                //Enabling the controls after DB connect
                ucSQLSearch.Enabled = true;
                ucSQLScriptGenerator.Enabled = true;

                txtServerName.Enabled = false;
                cmbAutheticationMode.Enabled = false;

                txtUserName.Enabled = false;
                txtPassword.Enabled = false;

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

        private void DisconnectDB()
        {
            cmbAutheticationMode.SelectedIndex = 0;
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
            txtUserName.Text = currentUser.Name;
            txtPassword.Text = "";
            txtUserName.Enabled = false;
            txtPassword.Enabled = false;
            btnConnectDB.Text = "CONNECT";
            txtServerName.Enabled = true;
            cmbAutheticationMode.Enabled = true;

            ConnectionInfo.ServerName = "";
            ConnectionInfo.UserName = "";
            ConnectionInfo.Password = "";
            ConnectionInfo.Databases = new DatabaseList();

            ucSQLSearch.Enabled = false;

        }

        private void cmbAutheticationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAutheticationMode.SelectedIndex == 0)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                txtUserName.Text = currentUser.Name;
                txtPassword.Text = "";
            }
            else if (cmbAutheticationMode.SelectedIndex == 1)
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
                txtUserName.Text = "";
                txtPassword.Text = "";
            }

        }
    }
}
