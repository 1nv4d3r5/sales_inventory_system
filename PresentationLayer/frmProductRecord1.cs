using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using BusinessObjects;
using BusinessLogicLayer;
namespace PresentationLayer
{
    public partial class frmProductRecord1 : Form
    {
        public frmProductRecord1()
        {
            InitializeComponent();
        }

        public void getData()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                DataTable dt = BM.BALGetProductData();
                dataGridView1.DataSource = dt;
                DataGridViewColumn col1 = dataGridView1.Columns[0];
                DataGridViewColumn col2 = dataGridView1.Columns[1];
                DataGridViewColumn col3 = dataGridView1.Columns[2];
                col1.Width = 215;
                col2.Width = 215;
                col3.Width = 215;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProductsRecord_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOProduct BO = new BOProduct();
                BO.ProductName = txtSearchProduct.Text.Trim();
                DataTable dt = BM.BALSearchbyProductName(BO);
                dataGridView1.DataSource = dt;
                DataGridViewColumn col1 = dataGridView1.Columns[0];
                DataGridViewColumn col2 = dataGridView1.Columns[1];
                DataGridViewColumn col3 = dataGridView1.Columns[2];
                col1.Width = 215;
                col2.Width = 215;
                col3.Width = 215;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOProduct BO = new BOProduct();
                BO.ProductCompany = txtSearchCompany.Text.Trim();
                DataTable dt = BM.BALSearchbyCompanyName(BO);
                dataGridView1.DataSource = dt;
                DataGridViewColumn col1 = dataGridView1.Columns[0];
                DataGridViewColumn col2 = dataGridView1.Columns[1];
                DataGridViewColumn col3 = dataGridView1.Columns[2];
                col1.Width = 215;
                col2.Width = 215;
                col3.Width = 215;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchCategory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOProduct BO = new BOProduct();
                BO.ProductCategory = txtSearchCategory.Text.Trim();
                DataTable dt = BM.BALSearchbyCategoryName(BO);
                dataGridView1.DataSource = dt;
                DataGridViewColumn col1 = dataGridView1.Columns[0];
                DataGridViewColumn col2 = dataGridView1.Columns[1];
                DataGridViewColumn col3 = dataGridView1.Columns[2];
                col1.Width = 215;
                col2.Width = 215;
                col3.Width = 215;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProductsRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

    }
}
