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
    public partial class frmConfigData : Form
    {
        public frmConfigData()
        {
            InitializeComponent();
        }

        public void getData()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                DataTable dt = BM.BALgetConfigData1();
                dataGridView1.DataSource = dt;
                DataGridViewColumn col1 = dataGridView1.Columns[1];
                DataGridViewColumn col2 = dataGridView1.Columns[2];
                col1.Width = 200;
                col2.Width = 300;
                dataGridView1.Columns[4].Visible = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormConfigData_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOProduct BO = new BOProduct();
                BO.ProductName = txtProductName.Text.Trim();
                DataTable dt = BM.BALSearchConfigData1(BO);
                dataGridView1.DataSource = dt;
                DataGridViewColumn col1 = dataGridView1.Columns[1];
                DataGridViewColumn col2 = dataGridView1.Columns[2];
                col1.Width = 200;
                col2.Width = 300;
                dataGridView1.Columns[4].Visible = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {                
                    DataGridViewRow dr = dataGridView1.SelectedRows[0];
                    this.Hide();
                    frmInventoryMgmt frm = new frmInventoryMgmt();
                    frm.Show();
                    frm.txtConfigID.Text = dr.Cells[0].Value.ToString();
                    frm.txtProductName.Text = dr.Cells[1].Value.ToString();
                    frm.txtFeatures.Text = dr.Cells[2].Value.ToString();
                    frm.txtPrice.Text = dr.Cells[3].Value.ToString();
                    //frm.label8.Text = label1.Text;
                    frm.txtQuantity.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmConfigData_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Hide();
            //frmConfig frm = new frmConfig();
            //frm.Show();            
        }
    }
}
