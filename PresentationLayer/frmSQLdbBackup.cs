using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace PresentationLayer
{
    public partial class frmSQLdbBackup : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        string sql = "";
        string connectionString = "";

        public frmSQLdbBackup()
        {
            InitializeComponent();     

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDataSource.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter Data Source","Data Source Empty",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtDataSource.Focus();
                    return;
                }

                if(radioButton1.Checked == true)
                {
                connectionString = @"Data Source=" + txtDataSource.Text + ";uid=" + txtUserID.Text + ";pwd=" + txtPassword.Text + "";
                }
                else
                {
                    connectionString = @"Data Source="+txtDataSource.Text+";Integrated Security=true";
                }

                con = new SqlConnection(connectionString);
                con.Open();
                //sql = "EXEC sp_databases";
                sql = "SELECT * FROM sys.databases d WHERE d.database_id > 4";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader();
                MessageBox.Show("Successfully connected!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbDatabase.Items.Clear();

                while (rdr.Read())
                {
                    cmbDatabase.Items.Add(rdr[0].ToString());
                }
                rdr.Dispose();
                con.Close();
                con.Dispose();
                txtDataSource.Enabled = false;
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                cmbDatabase.Enabled = true;
                btnBackup.Enabled = false;
                btnRestore.Enabled = false;
                btnBrowse.Enabled = true;
                btnBrowseBackup.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDataSource_MouseUp(object sender, MouseEventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(txtDataSource, "For e.g.,  .\\SQLEXPRESS");
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            txtDataSource.Enabled = true;
            txtUserID.Enabled = true;
            txtPassword.Enabled = true;
            btnDisconnect.Enabled = false;
            btnConnect.Enabled = true;
            btnBackup.Enabled = false;
            btnRestore.Enabled = false;
            radioButton1.Checked = true;
            btnBrowse.Enabled = false;
            btnBrowseBackup.Enabled = false;
            cmbDatabase.Enabled = false;
            cmbDatabase.Text = "";
            txtDataSource.Text = "";
            txtDataSource.Focus();
            txtUserID.Text = "";
            txtPassword.Text = "";
        }

        private void frmSQLdbBackup_Load(object sender, EventArgs e)
        {
            btnDisconnect.Enabled = false;
            cmbDatabase.Enabled = false;
            btnBackup.Enabled = false;
            btnRestore.Enabled = false;
            btnBrowse.Enabled = false;
            btnBrowseBackup.Enabled = false;
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDatabase.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("Please select a database", "Database not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                con = new SqlConnection(connectionString);
                con.Open();
                sql = "BACKUP DATABASE " + cmbDatabase.Text + " TO DISK='" + txtLocation.Text + "\\"+cmbDatabase.Text+"-"+DateTime.Now.Ticks.ToString()+".bak'";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                MessageBox.Show("Database successfully backed up", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLocation.Clear();
                btnBackup.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = dialog.SelectedPath;
                btnBackup.Enabled = true;
            }
        }

        private void btnBrowseBackup_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Backup File(*.bak)|*.bak| All Files(*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.Title = "Select a BAK File"; 

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtBackupPath.Text = dialog.FileName;
                btnRestore.Enabled = true;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (cmbDatabase.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("Please select a database", "Database not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                con = new SqlConnection(connectionString);
                con.Open();
                sql = "ALTER DATABASE " + cmbDatabase.Text + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                sql += "RESTORE DATABASE " + cmbDatabase.Text + " FROM DISK ='" + txtBackupPath.Text + "' WITH REPLACE";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                MessageBox.Show("Database successfully restored", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBackupPath.Clear();
                btnRestore.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSQLdbBackup_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                txtUserID.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtUserID.Enabled = true;
            txtPassword.Enabled = true;
            txtUserID.Clear();
        }
    }
}
