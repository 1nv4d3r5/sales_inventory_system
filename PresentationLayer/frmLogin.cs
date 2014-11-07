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
using System.Threading;

namespace PresentationLayer
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return;
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            try
            {
                BusinessManager bm = new BusinessManager();
                BOAddUser boUser = new BOAddUser();
                boUser.Username = txtUsername.Text.Trim();
                boUser.Password = txtPassword.Text.Trim();
                string res = bm.BALCheckLogin(boUser);
                if (res == "1")
                {
                    int i;
                    progressBar1.Visible = true;
                    progressBar1.Maximum = 200;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 4;
                    progressBar1.Step = 2;

                    for (i = 0; i <= 200; i++)
                    {
                        progressBar1.PerformStep();
                    }
                    //ThreadStart ts = new ThreadStart(StartMainMenu);
                    //Thread td = new Thread(ts);
                    //td.Start();
                    this.Hide();
                    frmMainMenu frm = new frmMainMenu();
                    frm.lblUser.Text = txtUsername.Text;
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed... Try again!", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        //public void StartMainMenu()
        //{
        //    frmMainMenu menu = new frmMainMenu();
        //    menu.Show();
        //}
    }
}
