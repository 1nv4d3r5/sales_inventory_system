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
    public partial class frmAddUser : Form
    {
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOAddUser BO = new BOAddUser();
                BO.Username = txtUsername.Text.Trim();
                bool res = BM.BALVerifyUserName(BO);

                if (txtUsername.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Focus();
                    return;
                }

                if (res == true)
                {
                    MessageBox.Show("Username not available", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!res)
                {
                    MessageBox.Show("Username available", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    BusinessManager addUser = new BusinessManager();
                    BOAddUser boUser = new BOAddUser();
                    boUser.Username = txtUsername.Text.Trim();
                    boUser.Password = txtPassword.Text.Trim();
                    boUser.Name = txtName.Text.Trim();
                    boUser.Contact = txtContact.Text.Trim();
                    boUser.Email = txtEmail.Text.Trim();
                    if (cmbUserType.SelectedIndex == 1)
                    {
                        boUser.UserType = 1;
                    }
                    else
                    {
                        boUser.UserType = 2;
                    }

                    int rt = addUser.BALadduser(boUser);

                    if (rt > 0)
                    {
                        MessageBox.Show("User added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        txtName.Text = "";
                        txtContact.Text = "";
                        txtEmail.Text = "";
                        txtConfirmPassword.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("User couldn't be added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("User already exists. Choose a unique username and try again.", "User Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            txtConfirmPassword.Text = "";
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            HideErrors(this.Controls);
            txtUsername.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Please enter a username");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUsername, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                //txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Please enter a password");
            }
            else if (txtPassword.Text.Trim().Length < 7)
            {
                e.Cancel = true;
                //txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Your password must be at least 7 characters");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Please confirm password");
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Passwords must match");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Please enter employee name");
                e.Cancel = true;
                //txtName.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtName, "");
            }
        }

        private void txtContact_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtContact.Text) || string.IsNullOrWhiteSpace(txtContact.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtContact, "Please enter a contact number");
                // txtContact.Focus();
            }
            else if (txtContact.Text.Trim().Length == 7 || txtContact.Text.Trim().Length == 10)
            {
                e.Cancel = false;
                errorProvider1.SetError(txtContact, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtContact, "Invalid Input. Please check your contact number and try again.");

            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            BusinessManager BM = new BusinessManager();
            BOCustomer Customer = new BOCustomer();
            Customer.CustomerEmail = txtEmail.Text;
            bool result = BM.IsEmail(Customer);

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                //txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Please enter e-mail address");
            }
            else if (result == false)
            {
                e.Cancel = true;
                // txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalid E-mail Address");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }

        }

        private void cmbUserType_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbUserType.Text))
            {
                e.Cancel = true;
                //cmbUserType.Focus();
                errorProvider1.SetError(cmbUserType, "Please select a user type");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cmbUserType, "");
            }
        }

        private void cmbUserType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
