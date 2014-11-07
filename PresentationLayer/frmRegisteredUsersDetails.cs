using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogicLayer;
using DataAccessLayer;
using BusinessObjects;

namespace PresentationLayer
{
    public partial class frmRegisteredUsersDetails : Form
    {
        public frmRegisteredUsersDetails()
        {
            InitializeComponent();
        }

        private void frmRegisteredUsersDetails_Load(object sender, EventArgs e)
        {
            try
            {
                BusinessManager dObject = new BusinessManager();
                dataGridView1.DataSource = dObject.GetUserData();
                dataGridView1.Columns[1].Visible = false;
                DataGridViewColumn col = dataGridView1.Columns[0];
                DataGridViewColumn col1 = dataGridView1.Columns[2];
                DataGridViewColumn col2 = dataGridView1.Columns[3];
                DataGridViewColumn col3 = dataGridView1.Columns[4];
                col.Width = 100;
                col1.Width = 100;
                col2.Width = 100;
                col3.Width = 100;

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
            frmRegisterUser frm = new frmRegisterUser();
            //// or simply use column name instead of index
            ////dr.Cells["id"].Value.ToString();            
            frm.Show();
            frm.txtUsername.Text = dr.Cells[0].Value.ToString();
            frm.txtUsername.Enabled = false;
            frm.txtPassword.Text = dr.Cells[1].Value.ToString();
            frm.txtPassword.Enabled = false;
            frm.txtName.Text = dr.Cells[2].Value.ToString();
            frm.txtContact.Text = dr.Cells[3].Value.ToString();
            frm.txtEmail.Text = dr.Cells[4].Value.ToString();
            frm.btnUpdate.Enabled = true;
            frm.txtName.Focus();
            frm.btnRegister.Enabled = false;
            frm.txtConfirmPassword.Text = dr.Cells[1].Value.ToString() ;
            frm.txtConfirmPassword.Enabled = false;
            try
            {
                if (Convert.ToInt32(dr.Cells[6].Value) == 1)
                {
                    frm.cmbUserType.Text = "Administrator";
                }
                else
                {
                    frm.cmbUserType.Text = "Standard User";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

        private void frmRegisteredUsersDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Hide();
            //frmRegisterUser frm = new frmRegisterUser();
            //frm.Show();
        }
    }
}
