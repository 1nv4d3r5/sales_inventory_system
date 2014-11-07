using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using BusinessLogicLayer;
using BusinessObjects;

namespace PresentationLayer
{
    public partial class frmCustomerDataRecord2 : Form
    {
        public frmCustomerDataRecord2()
        {
            InitializeComponent();
        }

        public void getData()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                dataGridView1.DataSource = BM.BALgetCustomerDataRecord2().Tables["Customer"].DefaultView;
                DataGridViewColumn col = dataGridView1.Columns[1];
                col.Width = 115;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomerDataRecord2_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow drow = dataGridView1.SelectedRows[0];
                this.Hide();
                frmCustomerMgmt frm = new frmCustomerMgmt();
                frm.Show();
                frm.txtCustomerID.Text = drow.Cells[0].Value.ToString();
                frm.txtCustomerName.Text = drow.Cells[1].Value.ToString();
                frm.txtAddress.Text = drow.Cells[2].Value.ToString();
                frm.txtPlace.Text = drow.Cells[3].Value.ToString();
                frm.txtCity.Text = drow.Cells[4].Value.ToString();
                frm.cmbZone.Text = drow.Cells[5].Value.ToString();
                frm.txtPostalCode.Text = drow.Cells[6].Value.ToString();
                frm.txtPhone.Text = drow.Cells[7].Value.ToString();
                frm.txtMobile.Text = drow.Cells[8].Value.ToString();
                frm.txtFax.Text = drow.Cells[9].Value.ToString();
                frm.txtEmail.Text = drow.Cells[10].Value.ToString();
                frm.txtNote.Text = drow.Cells[11].Value.ToString();
                frm.btnUpdate.Enabled = true;
                frm.btnDelete.Enabled = true;
                frm.btnRegister.Enabled = false;
                frm.txtCustomerName.Focus();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOProduct BO = new BOProduct();
                BO.TextBox = textBox1.Text.Trim();
                dataGridView1.DataSource = BM.BALSearchCustomerDataRecord2(BO).Tables["Customer"].DefaultView;
                DataGridViewColumn col = dataGridView1.Columns[1];
                col.Width = 115;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomerDataRecord2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmCustomerMgmt frm = new frmCustomerMgmt();
            frm.ShowDialog();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
    }
}
