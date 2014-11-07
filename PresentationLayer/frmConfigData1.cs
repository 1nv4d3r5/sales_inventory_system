using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using BusinessLogicLayer;
using BusinessObjects;

namespace PresentationLayer
{
    public partial class frmConfigData1 : Form
    {
        public frmConfigData1()
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

        private void FormConfigData1_Load(object sender, EventArgs e)
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

        private void FormConfigData1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow drow = dataGridView1.SelectedRows[0];
                this.Hide();
                frmConfig frm = new frmConfig();
                BusinessManager BM = new BusinessManager();
                frm.Show();
                frm.txtConfigID.Text = drow.Cells[0].Value.ToString();
                frm.cmbProductName.Text = drow.Cells[1].Value.ToString();
                frm.txtFeatures.Text = drow.Cells[2].Value.ToString();
                frm.txtPrice.Text = drow.Cells[3].Value.ToString();
                byte[] data = (byte[])drow.Cells[4].Value;
                frm.pictureBox1.Image = BM.BbyteArrayToImage(data);
                frm.btnDelete.Enabled = true;
                frm.btnUpdate.Enabled = true;
                frm.btnSave.Enabled = false;
                frm.cmbProductName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
