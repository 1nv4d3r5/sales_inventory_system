using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogicLayer;
using BusinessObjects;
namespace PresentationLayer
{
    public partial class frmDatabaseMgmt : Form
    {     
        public frmDatabaseMgmt()
        {
            InitializeComponent();
        }

        private void frmDatabaseMgmt_Load(object sender, EventArgs e)
        {
            btnBackup.Enabled = false;
            btnRestore.Enabled = false;
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

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOBackup BO = new BOBackup();
                BO.BackupLocation = txtLocation.Text;
                int rowsAffected = BM.BALBackupDatabase(BO);
                    MessageBox.Show("Database successfully backed up", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLocation.Clear();
                    btnBackup.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {

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
                BusinessManager BM = new BusinessManager();
                BOBackup BO = new BOBackup();
                BO.RestoreLocation = txtBackupPath.Text;
                int rowsAffected = BM.BALRestoreDatabase(BO);
                    MessageBox.Show("Database successfully restored", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBackupPath.Clear();
                    btnRestore.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
