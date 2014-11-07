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
    public partial class frmAddProduct : Form
    {
        public frmAddProduct()
        {
            InitializeComponent();
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
            txtProductName.Text = "";
            cboxCompany.Text = "";
            cboxProductCategory.Text = "";
            btnSave.Enabled = true;
            HideErrors(this.Controls);
            txtProductName.Focus();
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
                            btnSave.Enabled = false;
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
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
        }

        public void FillCategoryCombo()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                cboxProductCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
                //cboxProductCategory.DropDownStyle = ComboBoxStyle.DropDownList;
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
                //cboxCompany.DropDownStyle = ComboBoxStyle.DropDownList;
                cboxCompany.DisplayMember = "CompanyName";
                cboxCompany.DataSource = BM.BALFillCompanyCombo().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormAddProduct_Load(object sender, EventArgs e)
        {
            FillCategoryCombo();
            FillCompanyCombo();
            cboxProductCategory.SelectedIndex = -1;
            cboxCompany.SelectedIndex = -1;
        }

        private void btnData_Click(object sender, EventArgs e)
        {
          
        }

        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else 
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtProductName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrEmpty(txtProductName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtProductName, "Please enter a product name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtProductName, "");
            }
        }

        private void cboxProductCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cboxCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (txtProductName.Text.StartsWith(" "))
            {
                if (txtProductName.Text.StartsWith(" "))
                {
                    MessageBox.Show("You cannot use blank spaces at the first position", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    New();
                }
            }
        }

        private void cboxProductCategory_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(cboxProductCategory.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cboxProductCategory, "Please select a product category");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cboxProductCategory, "");
            }
        }

        private void cboxCompany_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cboxCompany.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cboxCompany, "Please select a company");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cboxCompany, "");
            }
        }

        private void cboxProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboxProductCategory_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
}
