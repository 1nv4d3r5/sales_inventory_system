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
    public partial class frmInventoryData : Form
    {
        public frmInventoryData()
        {
            InitializeComponent();
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOProduct BO = new BOProduct();
                BO.ProductName = txtProductName.Text.Trim();
                DataTable dt = BM.BALSearchInventoryData(BO);
                dataGridView1.DataSource = dt;
                DataGridViewColumn col1 = dataGridView1.Columns[0];
                DataGridViewColumn col2 = dataGridView1.Columns[1];
                DataGridViewColumn col3 = dataGridView1.Columns[2];
                DataGridViewColumn col4 = dataGridView1.Columns[3];
                DataGridViewColumn col5 = dataGridView1.Columns[4];
                DataGridViewColumn col6 = dataGridView1.Columns[5];
                DataGridViewColumn col7 = dataGridView1.Columns[6];
                DataGridViewColumn col8 = dataGridView1.Columns[7];
                col1.Width = 75;
                col2.Width = 80;
                col3.Width = 150;
                col4.Width = 115;
                col5.Width = 75;
                col6.Width = 75;
                col7.Width = 90;
                col8.Width = 105;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmInventoryMgmt frm = new frmInventoryMgmt();
            frm.Show();
            frm.txtInventoryID.Text = dr.Cells[0].Value.ToString();
            frm.dataGridView1.Text = dr.Cells[1].Value.ToString();
            frm.txtProductName.Text = dr.Cells[2].Value.ToString();
            frm.txtFeatures.Text = dr.Cells[3].Value.ToString();
            frm.txtPrice.Text = dr.Cells[4].Value.ToString();
            frm.txtQuantity.Text = dr.Cells[5].Value.ToString();
            frm.txtTotalPrice.Text = dr.Cells[6].Value.ToString();
            frm.dtpInventoryDate.Text = dr.Cells[7].Value.ToString();
            frm.btnUpdate.Enabled = true;
            frm.btnDelete.Enabled = true;
            frm.btnRegister.Enabled = false;
        }

        public void getData()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                DataTable dt = BM.BALgetInventoryData();
                dataGridView1.DataSource = dt;
                DataGridViewColumn col1 = dataGridView1.Columns[0];
                DataGridViewColumn col2 = dataGridView1.Columns[1];
                DataGridViewColumn col3 = dataGridView1.Columns[2];
                DataGridViewColumn col4 = dataGridView1.Columns[3];
                DataGridViewColumn col5 = dataGridView1.Columns[4];
                DataGridViewColumn col6 = dataGridView1.Columns[5];
                DataGridViewColumn col7 = dataGridView1.Columns[6];
                DataGridViewColumn col8 = dataGridView1.Columns[7];
                col1.Width = 75;
                col2.Width = 80;
                col3.Width = 150;
                col4.Width = 115;
                col5.Width = 75;
                col6.Width = 75;
                col7.Width = 90;
                col8.Width = 105;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InventoryData_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmInventory frm = new frmInventory();
            frm.Show();
            
        }

        private void InventoryData_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProductName.Text = "";
            getData();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
