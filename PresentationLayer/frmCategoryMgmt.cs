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
  
    public partial class frmCategoryMgmt : Form
    {
        public frmCategoryMgmt()
        {
            InitializeComponent();
           // New();
        }

        private void FormAddCategory_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
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
           // btnDelete.Enabled = false;
           // btnUpdate.Enabled = false;
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
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (txtCategoryName.Text != "")
                {

                    if (MessageBox.Show("Do you really want to delete the category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        BOProduct BO = new BOProduct();
                        BusinessManager BM = new BusinessManager();
                        BO.CategoryName = txtCategoryName.Text.Trim();
                        int res = BM.BALDeleteCategory(BO);
                        if (res > 0)
                        {
                            MessageBox.Show("Successfully deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            New();
                        }
                        else
                        {
                            MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            New();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a category to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCategoryName.Focus();
                }
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCategoryList frm = new frmCategoryList();
            frm.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    BOProduct BO = new BOProduct();
                    BusinessManager BM = new BusinessManager();
                    BO.CategoryName = txtCategoryName.Text.Trim();
                    BO.TextBox = textBox1.Text.Trim();

                    int res = BM.BALUpdateCategory(BO);

                    if (res > 0)
                    {
                        MessageBox.Show("Successfully updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnUpdate.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Record couldn't be updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        New();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            if(txtCategoryName.Text.StartsWith(" "))
            {
                MessageBox.Show("You cannot start category name with blank space","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                New();
            }
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

        private void frmCategoryMgmt_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
