using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BusinessObjects;
using BusinessLogicLayer;

namespace PresentationLayer
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit Sales and Inventory System application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //this.Close();
                Application.Exit();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        public enum UserType
        {
            Admin = 1,
            User = 2
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel4.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            Start_Timer();
            BusinessManager BM = new BusinessManager();
            BOAddUser BO = new BOAddUser();
            BO.Username = lblUser.Text;
            byte[] data = BM.BALGetImageData();
            pictureBox1.Image = BM.BbyteArrayToImage(data);
            SetButtonsEnabledDisabled(IsAdmin(BM.GetUserType(BO)));
        }

        private bool IsAdmin(int userTypeId)
        {
            return userTypeId == (int)UserType.Admin;
        }

        public void SetButtonsEnabledDisabled(bool isEnabled)
        {
            btnEmployeeManagement.Enabled = isEnabled;
            //btnSettings.Enabled = isEnabled;
            btnReports.Enabled = isEnabled;
            btnSupplierManagement.Enabled = isEnabled;
            reportToolStripMenuItem.Enabled = isEnabled;
            recordsToolStripMenuItem.Enabled = isEnabled;
            employeesToolStripMenuItem.Enabled = isEnabled;
        }

        System.Windows.Forms.Timer tmr = null;

        private void Start_Timer()
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel4.Text = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmInvoice frm = new frmInvoice();
            frm.lblCashier.Text = lblUser.Text;
            frm.Show();
        }

        private void btnEmployeeManagement_Click(object sender, EventArgs e)
        {
            frmRegisterUser frm = new frmRegisterUser();
            frm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.Show();
            frm.txtUsername.Text = "";
            frm.txtPassword.Text = "";
            frm.progressBar1.Visible = false;
            frm.txtUsername.Focus();
        }

        private void inventoryRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomerDataRecord3 frmCustomer = new frmCustomerDataRecord3();
            frmCustomer.ShowDialog();

        }

        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe");
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void microsoftWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("winword.exe");
        }

        private void mircorsoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.exe");
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            frmTools frm = new frmTools();
            frm.ShowDialog();
        }

        private void btnProductManagement_Click(object sender, EventArgs e)
        {
            frmProductMgmt frm = new frmProductMgmt();
            frm.ShowDialog();
        }

        private void btnCustomerManagement_Click(object sender, EventArgs e)
        {
            frmCustomerMgmt frm = new frmCustomerMgmt();
            frm.ShowDialog();
        }

        private void btnSupplierManagement_Click(object sender, EventArgs e)
        {        
            frmDataRecord frm = new frmDataRecord();
            frm.ShowDialog();
        }

        private void frmMainMenu_Resize(object sender, EventArgs e)
        {
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }

        private void backupAndRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSQLdbBackup frm = new frmSQLdbBackup();
            frm.ShowDialog();
        }

        private void systemInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("msinfo32.exe");
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddProduct frm = new frmAddProduct();
            frm.ShowDialog();
        }

        private void btnInventoryManagement_Click(object sender, EventArgs e)
        {
            frmInventoryMgmt frm = new frmInventoryMgmt();
            frm.ShowDialog();
        }

        private void productRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductRecord1 frm = new frmProductRecord1();
            frm.ShowDialog();
        }

        private void inventoryRecordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmInventoryData1 frm = new frmInventoryData1();
            frm.ShowDialog();
        }

        private void salesRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSalesRecord1 frm = new frmSalesRecord1();
            frm.ShowDialog();
        }

        private void customerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Cursor = Cursors.WaitCursor;
                //timer1.Enabled = true;
                BusinessManager BM = new BusinessManager();
                rptCustomersReport report = new rptCustomersReport();
                report.SetDataSource(BM.BALCustomerReport());
                frmViewCustomerReport vcr = new frmViewCustomerReport();
                vcr.crystalReportViewer1.ReportSource = report;
                vcr.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            frmReports frm = new frmReports();
            frm.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.label5.Text = lblUser.Text;
            frm.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.Show();
            frm.txtUsername.Text = "";
            frm.txtPassword.Text = "";
            frm.progressBar1.Visible = false;
            frm.txtUsername.Focus();
        }

        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddCategory frm = new frmAddCategory();
            frm.ShowDialog();
        }

        private void addCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddCompany frm = new frmAddCompany();
            frm.ShowDialog();
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUser frm = new frmAddUser();
            frm.ShowDialog();
        }

        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddCustomer frm = new frmAddCustomer();
            frm.ShowDialog();
        }

        private void addInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddInventory frm = new frmAddInventory();
            frm.ShowDialog();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                BusinessManager BM = new BusinessManager();
                rptSalesReport report = new rptSalesReport();
                report.SetDataSource(BM.BALSalesReport());
                frmSalesReport rpt = new frmSalesReport();
                rpt.crystalReportViewer1.ReportSource = report;
                rpt.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmMainMenu_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
