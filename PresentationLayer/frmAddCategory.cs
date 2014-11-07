using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogicLayer;
using BusinessObjects;
using System.Threading;

namespace PresentationLayer
{
  
    public partial class frmAddCategory : Form
    {
        public frmAddCategory()
        {
            InitializeComponent();
        }

        private void FormAddCategory_Load(object sender, EventArgs e)
        {

        }

        private void HideErrors(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                errorProvider1.SetError(ctl, null);
                HideErrors(ctl.Controls);
            }
        }

        private void New()
        {
            txtCategoryName.Text = "";
            btnSave.Enabled = true;
            HideErrors(this.Controls);
            txtCategoryName.Focus();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    BOProduct BO = new BOProduct();
                    BusinessManager BM = new BusinessManager();
                    BO.CategoryName = txtCategoryName.Text.Trim();
                    bool res = BM.BALVerifyCategoryName(BO);

                    if (res == true)
                    {
                        MessageBox.Show("Category name already exits. Please enter a new category name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCategoryName.Text = "";
                        txtCategoryName.Focus();
                    }

                    int rowsAffected = BM.BALInsertCategory(BO);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        }

        private void txtCategoryName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else 
            {
                e.Handled = true;
            }
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            if(txtCategoryName.Text.StartsWith(" "))
            {
                MessageBox.Show("You cannot use blank spaces at the first position","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                New();
            }
        }

        private void txtCategoryName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text) || string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCategoryName, "Please enter a category name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCategoryName, "");
            }
        }
    }
}
