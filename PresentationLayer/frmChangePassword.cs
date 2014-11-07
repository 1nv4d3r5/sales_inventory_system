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
using DataAccessLayer;
using System.Threading;

namespace PresentationLayer
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    int RowsAffected = 0;
                    BusinessManager bm = new BusinessManager();
                    BOAddUser bo = new BOAddUser();
                    bo.OldPassword = txtOldPassword.Text.Trim();
                    bo.NewPassword = txtNewPassword.Text.Trim();
                    bo.Username = txtUsername.Text.Trim();                  

                    RowsAffected = bm.BALChangePassword(bo);
                    if (RowsAffected > 0)
                    {
                        MessageBox.Show("Password successfully changed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        txtUsername.Text = "";
                        txtOldPassword.Text = "";
                        txtNewPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        frmLogin userlogin = new frmLogin();
                        userlogin.Show();
                        userlogin.txtUsername.Text = "";
                        userlogin.txtPassword.Text = "";
                        userlogin.progressBar1.Visible = false;
                        userlogin.txtUsername.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Text = "";
                        txtOldPassword.Text = "";
                        txtNewPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        txtOldPassword.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrEmpty(txtUsername.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "Please enter a username");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUsername, "");
            }
        }

        private void txtOldPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOldPassword.Text) || string.IsNullOrEmpty(txtOldPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "Please enter old password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtOldPassword, "");
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                e.Cancel = true;
                //txtPassword.Focus();
                errorProvider1.SetError(txtNewPassword, "Please enter a new password");
            }
            else if (txtOldPassword.Text.Trim() == txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "Old password and new password must not match");
            }
            else if (txtNewPassword.Text.Trim().Length < 7)
            {
                e.Cancel = true;
                //txtPassword.Focus();
                errorProvider1.SetError(txtNewPassword, "The new password must be at least 7 characters");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Please confirm password");
            }
            else if (txtNewPassword.Text != txtConfirmPassword.Text)
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
    }
}
