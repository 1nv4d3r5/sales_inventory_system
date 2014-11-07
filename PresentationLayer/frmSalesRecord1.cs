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
    public partial class frmSalesRecord1 : Form
    {
        public frmSalesRecord1()
        {
            InitializeComponent();
            FillCombo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BOCustomer BO = new BOCustomer();
                BusinessManager BM = new BusinessManager();
                BO.CustomerName = cmbCustomerName.Text.Trim() ;
                //groupBox3.Visible = true;
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
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void frmSalesRecord1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();

        }

        }
    }

