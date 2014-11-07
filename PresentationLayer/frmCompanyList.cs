using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects;
using BusinessLogicLayer;

namespace PresentationLayer
{
    public partial class frmCompanyList : Form
    {
        public frmCompanyList()
        {
            InitializeComponent();
        }


        public void ShowList()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                DataTable dt = BM.BALGetCompanyData();
                BindingSource bSource = new BindingSource();
                bSource.DataSource = dt;
                dataGridView1.DataSource = bSource;
                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 233;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmCompanyMgmt frm = new frmCompanyMgmt();
            //// or simply use column name instead of index
            ////dr.Cells["id"].Value.ToString();
            frm.Show();
            frm.txtCompanyName.Text = dr.Cells[0].Value.ToString();
            frm.textBox1.Text = dr.Cells[0].Value.ToString();
            frm.btnDelete.Enabled = true;
            frm.btnUpdate.Enabled = true;
            frm.txtCompanyName.Focus();
            frm.btnSave.Enabled = false;
        }

        private void FormCompanyList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmCompanyMgmt frm = new frmCompanyMgmt();
            frm.Show();
        }

        private void FormCompanyList_Load(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                dataGridView1.DataSource = BM.BALShowCompanyList();
                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 220;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
