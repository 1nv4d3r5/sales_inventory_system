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
    public partial class frmProductMgmt : Form
    {
        public frmProductMgmt()
        {
            InitializeComponent();
        }        private void HideErrors(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                errorProvider1.SetError(ctl, null);
                HideErrors(ctl.Controls);
            }
        }

        private void New()
        {
            txtProductName.Text = "";
            cboxCompany.Text = "";
            cboxProductCategory.Text = "";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnRegister.Enabled = true;
            HideErrors(this.Controls);
            txtProductName.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (txtProductName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter product name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductName.Focus();
                    return;
                }

                if (cboxProductCategory.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please select a product category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboxProductCategory.Focus();
                    return;
                }

                if (cboxCompany.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please select a company", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboxCompany.Focus();
                    return;
                }

                try
                {
                    BOProduct BO = new BOProduct();
                    BusinessManager BM = new BusinessManager();
                    BO.ProductName = txtProductName.Text.Trim();
                    BO.ProductCategory = cboxProductCategory.Text.Trim();
                    BO.ProductCompany = cboxCompany.Text.Trim();
                    bool res = BM.BALVerifyProductName(BO);

                    if (res == true)
                    {
                        MessageBox.Show("Product name already exits. Please enter a new product name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtProductName.Text = "";
                        txtProductName.Focus();
                    }
                    else
                    {
                        int rowsAffected = BM.BALInsertProductName(BO);
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Autocomplete();
                            txtProductName.Text = "";
                            cboxCompany.Text = "";
                            cboxProductCategory.Text = "";
                            txtProductName.Focus();
                            btnRegister.Enabled = false;
                        }


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
                if (txtProductName.Text != "")
                {
                    if (MessageBox.Show("Do you really want to delete the record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        BOProduct BO = new BOProduct();
                        BusinessManager BM = new BusinessManager();
                        BO.ProductName = txtProductName.Text.Trim();
                        int res = BM.BALDeleteProduct(BO);
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
                    MessageBox.Show("Please enter a product to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductName.Focus();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    BOProduct BO = new BOProduct();
                    BusinessManager BM = new BusinessManager();
                    BO.ProductName = txtProductName.Text.Trim();
                    BO.ProductCategory = cboxProductCategory.Text.Trim();
                    BO.ProductCompany = cboxCompany.Text.Trim();
                    BO.TextBox = textBox1.Text.Trim();

                    int res = BM.BALUpdateProduct(BO);

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

        private void btnList_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProductRecord frm = new frmProductRecord();
            frm.Show();
            frm.getData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmConfig frm = new frmConfig();
            frm.ShowDialog();
        }

        public void FillCategoryCombo()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                cboxProductCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
                cboxProductCategory.DropDownStyle = ComboBoxStyle.DropDownList;
                cboxProductCategory.DisplayMember = "CategoryName";
                //cboxProductCategory.ValueMember = "CategoryName";
                cboxProductCategory.DataSource = BM.BALFillCategoryCombo().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FillCompanyCombo()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                cboxCompany.AutoCompleteSource = AutoCompleteSource.ListItems;
                cboxCompany.DropDownStyle = ComboBoxStyle.DropDownList;
                cboxCompany.DisplayMember = "CompanyName";
                cboxCompany.DataSource = BM.BALFillCompanyCombo().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmProductMgmt_Load(object sender, EventArgs e)
        {
            FillCategoryCombo();
            FillCompanyCombo();
            cboxCompany.SelectedIndex = -1;
            cboxProductCategory.SelectedIndex = -1;
        }

        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            frmCompanyMgmt frm = new frmCompanyMgmt();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCategoryMgmt frm = new frmCategoryMgmt();
            frm.ShowDialog();
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if(txtProductName.Text.StartsWith(" "))
            {
                MessageBox.Show("You cannot start Product Name with a blank space","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                New();
            }
        }

        private void txtProductName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrEmpty(txtProductName.Text))
            {
                e.Cancel = true;
               errorProvider1.SetError(txtProductName,"Please enter a product name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtProductName,"");
            }
        }

        private void cboxProductCategory_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(cboxProductCategory.Text) || string.IsNullOrEmpty(cboxProductCategory.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cboxProductCategory, "Please select a product category name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cboxProductCategory, "");
            }
        }

        private void cboxCompany_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboxCompany.Text) || string.IsNullOrEmpty(cboxCompany.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cboxCompany, "Please select a company name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cboxCompany, "");
            }
        }
    }
}
