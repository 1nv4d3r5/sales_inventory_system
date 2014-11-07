using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BusinessLogicLayer;
using BusinessObjects;
namespace PresentationLayer
{
    public partial class frmSalesRecord : Form
    {
        public frmSalesRecord()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BOCustomer BO = new BOCustomer();
                BusinessManager BM = new BusinessManager();
                BO.CustomerName = cmbCustomerName.Text.Trim() ;
                groupBox3.Visible = true;
                dataGridView1.DataSource = BM.BALSalesRecordCombo(BO).Tables["Sales"].DefaultView;
                dataGridView1.DataSource = BM.BALSalesRecordCombo(BO).Tables["Customer"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    Int64 i = Convert.ToInt64(row.Cells[7].Value);
                    Int64 j = Convert.ToInt64(row.Cells[8].Value);
                    Int64 k = Convert.ToInt64(row.Cells[9].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                totalAmount.Text = sum.ToString();
                totalPayment.Text = sum1.ToString();
                paymentDue.Text = sum2.ToString();
                           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private void btnGetData_Click(object sender, EventArgs e)
        {
        }

        public void FillCombo()
        {

            try
            {
                BusinessManager BM = new BusinessManager();
                DataTable dt = BM.BALSalesRecordFillCombo();
                cmbCustomerName.Items.Clear();
                foreach (DataRow drow in dt.Rows)
                {
                    cmbCustomerName.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormSalesRecord_Load(object sender, EventArgs e)
        {
            FillCombo();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            cmbCustomerName.Text = "";
            groupBox3.Visible = false;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                this.Hide();
                frmInvoice frm = new frmInvoice();
                frm.Show();

                frm.txtInvoiceNo.Text = row.Cells[0].Value.ToString();
                frm.dtpInvoiceDate.Text = row.Cells[1].Value.ToString();
                frm.txtCustID.Text = row.Cells[2].Value.ToString();
                frm.txtCustName.Text = row.Cells[3].Value.ToString();
                frm.txtSubTotal.Text = row.Cells[4].Value.ToString();
                frm.txtVAT.Text = row.Cells[5].Value.ToString();
                frm.txtVatAmount.Text = row.Cells[6].Value.ToString();
                frm.txtGrandTotal.Text = row.Cells[7].Value.ToString();
                frm.txtTotalPayment.Text = row.Cells[8].Value.ToString();
                frm.txtPaymentDue.Text = row.Cells[9].Value.ToString();
                frm.txtRemarks.Text = row.Cells[10].Value.ToString();
                frm.btnUpdate.Enabled = true;
                frm.btnDelete.Enabled = true;
                frm.btnPrint.Enabled = true;
                frm.btnSave.Enabled = false;

                BOCustomer BO = new BOCustomer();
                BusinessManager BM = new BusinessManager();
                BO.InvoiceNo = frm.txtInvoiceNo.Text.Trim();
                DataTable dt = BM.BALSalesRecordFillList(BO);
                foreach (DataRow drow in dt.Rows)
                {
                    //cmbCustomerName.Items.Add(drow[0].ToString());
                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(drow[0].ToString().Trim());
                    lst.SubItems.Add(drow[1].ToString().Trim());
                    lst.SubItems.Add(drow[2].ToString().Trim());
                    lst.SubItems.Add(drow[3].ToString().Trim());
                    lst.SubItems.Add(drow[4].ToString().Trim());
                    frm.listView1.Items.Add(lst);
                }
                frm.listView1.Enabled = false;
                frm.btnAddCart.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSalesRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmInvoice frm = new frmInvoice();
            frm.Show();
        }

        }
    }

