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
    public partial class frmCustomerMgmt : Form
    {
        public frmCustomerMgmt()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomerDataRecord2 frm = new frmCustomerDataRecord2();
            frm.Show();
            frm.getData();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomerDataRecord2 frm = new frmCustomerDataRecord2();
            frm.Show();
            frm.getData();
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
            txtCustomerName.Text = "";
            txtCustomerID.Text = "";
            txtAddress.Text = "";
            cmbZone.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtMobile.Text = "";
            txtNote.Text = "";
            txtPlace.Text = "";
            txtPostalCode.Text = "";
            txtPhone.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnRegister.Enabled = true;
            HideErrors(this.Controls);
            txtCustomerName.Focus();

        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void auto_generate_id()
        {
            txtCustomerID.Text = "C-" + GetUniqueKey(1000, 10000);
        }

        public static string GetUniqueKey(int min_value, int max_value)
        {
            Random rnd = new Random();
            int maxSize = rnd.Next(min_value, max_value);
            return maxSize.ToString();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (txtCustomerName.Text == "")
                {
                    MessageBox.Show("Please enter customer name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerName.Focus();
                    return;
                }

                if (txtAddress.Text == "")
                {
                    MessageBox.Show("Please enter an address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                    return;
                }

                if (txtCity.Text == "")
                {
                    MessageBox.Show("Please enter a city name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCity.Focus();
                    return;
                }

                if (cmbZone.Text == "")
                {
                    MessageBox.Show("Please select a zone", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbZone.Focus();
                    return;
                }

                if (txtMobile.Text == "")
                {
                    MessageBox.Show("Please enter a mobile number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMobile.Focus();
                    return;
                }

                if (txtEmail.Text == "")
                {
                    MessageBox.Show("Please enter an email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return;
                }
                try
                {
                    auto_generate_id();
                    BusinessManager BM = new BusinessManager();
                    BOCustomer BO = new BOCustomer();
                    BO.CustomerID = txtCustomerID.Text.Trim();
                    BO.CustomerName = txtCustomerName.Text.Trim();
                    BO.CustomerAddress = txtAddress.Text.Trim();
                    BO.CustomerPlace = txtPlace.Text.Trim();
                    BO.CustomerCity = txtCity.Text.Trim();
                    BO.CustomerZone = cmbZone.Text.Trim();
                    BO.CustomerPostalCode = txtPostalCode.Text.Trim();
                    BO.CustomerPhone = txtPhone.Text.Trim();
                    BO.CustomerMobile = txtMobile.Text.Trim();
                    BO.CustomerFax = txtFax.Text.Trim();
                    BO.CustomerEmail = txtEmail.Text.Trim();
                    BO.CustomerNotes = txtNote.Text.Trim();

                    bool res = BM.BALVerifyCustomerName(BO);
                    int rowsAffected = 0;
                    if (res == true)
                    {
                        MessageBox.Show("Customer ID already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        New();
                    }
                    else
                    {
                        rowsAffected = BM.BALInsertIntoCustomer(BO);
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnRegister.Enabled = false;
                            New();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    BusinessManager BM = new BusinessManager();
                    BOCustomer BO = new BOCustomer();
                    BO.CustomerID = txtCustomerID.Text.Trim();
                    BO.CustomerName = txtCustomerName.Text.Trim();
                    BO.CustomerAddress = txtAddress.Text.Trim();
                    BO.CustomerPlace = txtPlace.Text.Trim();
                    BO.CustomerCity = txtCity.Text.Trim();
                    BO.CustomerZone = cmbZone.Text.Trim();
                    BO.CustomerPostalCode = txtPostalCode.Text.Trim();
                    BO.CustomerPhone = txtPhone.Text.Trim();
                    BO.CustomerMobile = txtMobile.Text.Trim();
                    BO.CustomerFax = txtFax.Text.Trim();
                    BO.CustomerEmail = txtEmail.Text.Trim();
                    BO.CustomerNotes = txtNote.Text.Trim();
                    int rowsAffected = BM.BALUpdateCustomer(BO);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRegister.Enabled = false;
                        New();
                    }
                    else
                    {
                        MessageBox.Show("Couldn't be updated!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnRegister.Enabled = false;
                        New();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Delete()
        {
            try
            {
                int rowsAffected = 0;
                BusinessManager BM = new BusinessManager();
                BOCustomer BO = new BOCustomer();
                BO.CustomerID = txtCustomerID.Text.Trim();
                rowsAffected = BM.BALDeleteFromCustomer(BO);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("The record has been successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    New();
                }
                else
                {
                    MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    New();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    if (MessageBox.Show("Do you really want to delete the record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Delete();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmCustomerMgmt_Load(object sender, EventArgs e)
        {
            frmMainMenu frm = new frmMainMenu();
            frm.Owner = this;

        }

        private void txtCustomerName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                e.Cancel = true;
                txtCustomerName.Focus();
                errorProvider1.SetError(txtCustomerName, "Please enter customer name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCustomerName, "");
            }
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAddress, "Please enter an address");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAddress, "");
            }
        }

        private void txtPlace_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlace.Text))
            {
                e.Cancel = true;
                //txtPlace.Focus();
                errorProvider1.SetError(txtPlace, "Please enter your VDC or Municipality");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPlace, "");
            }
        }

        private void txtCity_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCity.Text))
            {
                e.Cancel = true;
                //txtCity.Focus();
                errorProvider1.SetError(txtCity, "Please enter city name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCity, "");
            }
        }

        private void cmbZone_Validating(object sender, CancelEventArgs e)
        {
            if (cmbZone.Text.Trim().Length == 0)
            {
                e.Cancel = true;
                //cmbZone.Focus();
                errorProvider1.SetError(cmbZone, "Please select a zone");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cmbZone, "");
            }
        }

        private void cmbZone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtPostalCode_Validating(object sender, CancelEventArgs e)
        {

            if (txtPostalCode.Text.Trim().Length != 5)
            {
                e.Cancel = true;
                //txtPostalCode.Focus();
                errorProvider1.SetError(txtPostalCode, "Postal Code must be 5 digits");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPostalCode, "");
            }
        }

        private void txtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtPhone_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhone.Text) && txtPhone.Text.Trim().Length != 7)
            {
                e.Cancel = true;
                //txtPhone.Focus();
                errorProvider1.SetError(txtPhone, "Phone number must be 7 digits");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPhone, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            BusinessManager BM = new BusinessManager();
            BOCustomer Customer = new BOCustomer();
            Customer.CustomerEmail = txtEmail.Text;
            bool result = BM.IsEmail(Customer);

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                e.Cancel = true;
                //txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Please enter e-mail address");
            }
            else if (result == false)
            {
                e.Cancel = true;
                //txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalid E-mail Address");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private void txtMobile_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMobile.Text) && (txtMobile.Text.Trim().Length != 10))
            {
                e.Cancel = true;
                //txtMobile.Focus();
                errorProvider1.SetError(txtMobile, "Mobile number must be 10 digits");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtMobile, "");
            }
        }

        private void txtFax_Validating(object sender, CancelEventArgs e)
        {
            if (txtFax.Text.Trim().Length != 0)
            {
                if (txtFax.Text.Trim().Length != 7)
                {
                    e.Cancel = true;
                    //txtFax.Focus();
                    errorProvider1.SetError(txtFax, "Fax number must be 7 digits");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtFax, "");
                }
            }
        }
    }
}
