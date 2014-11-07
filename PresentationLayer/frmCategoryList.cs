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
    public partial class frmCategoryList : Form
    {
        public frmCategoryList()
        {
            InitializeComponent();
        }
               
        private void FormCategoryList_Load(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                dataGridView1.DataSource = BM.BALShowCategoryList();
                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 233;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormCategoryList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmCategoryMgmt frm = new frmCategoryMgmt();
            frm.Show();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmCategoryMgmt frm = new frmCategoryMgmt();
            //// or simply use column name instead of index
            ////dr.Cells["id"].Value.ToString();
            frm.Show();
            frm.txtCategoryName.Text = dr.Cells[0].Value.ToString();
            frm.textBox1.Text = dr.Cells[0].Value.ToString();
            frm.btnDelete.Enabled = true;
            frm.btnUpdate.Enabled = true;
            frm.txtCategoryName.Focus();
            frm.btnSave.Enabled = false;
        }
        }
    }

