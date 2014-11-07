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

namespace PresentationLayer
{
    public partial class frmCompanyMgmt : Form
    {
        public frmCompanyMgmt()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    BOCompany BO = new BOCompany();
                    BusinessManager BM = new BusinessManager();
                    BO.CompanyName = txtCompanyName.Text.Trim();
                    bool res = BM.BALVerifyCompanyName(BO);

                    if (res == true)
                    {
                        MessageBox.Show("Company name already exits. Please enter a new company name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCompanyName.Text = "";
                        txtCompanyName.Focus();
                    }

                    int rowsAffected = BM.BALInsertCompany(BO);

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
            txtCompanyName.Text = "";           
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            HideErrors(this.Controls);
            txtCompanyName.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }
               
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (txtCompanyName.Text != "")
                {
                    if ((MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        BOCompany BO = new BOCompany();
                        BusinessManager BM = new BusinessManager();
                        BO.CompanyName = txtCompanyName.Text.Trim();
                        int res = BM.BALDeleteCompany(BO);
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
                    MessageBox.Show("Please enter a company to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCompanyName.Focus();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    BOCompany BO = new BOCompany();
                    BusinessManager BM = new BusinessManager();
                    BO.CompanyName = txtCompanyName.Text.Trim();
                    BO.TextBox = textBox1.Text.Trim();

                    int res = BM.BALUpdateCompany(BO);

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

        private void btnShow_Click(object sender, EventArgs e)
        {
              this.Hide();
              frmCompanyList frm = new frmCompanyList();
              frm.Show();
          
        }

        private void FormAddCompany_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            if (txtCompanyName.Text.StartsWith(" "))
            {
                MessageBox.Show("You cannot start company name with blank space", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                New();
            }
        }

        private void txtCompanyName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void frmCompanyMgmt_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void txtCompanyName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompanyName.Text) || string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCompanyName, "Please enter a company name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCompanyName, "");
            }

        }
        }
    }

