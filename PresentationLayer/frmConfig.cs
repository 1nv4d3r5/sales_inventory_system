using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using BusinessLogicLayer;
using BusinessObjects;

namespace PresentationLayer
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                var browse = openFileDialog1;
                browse.Filter = "Image (*.jpg *.jpeg *.png *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";
                browse.FilterIndex = 0;
                browse.Title = "Open Image";
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtFeatures.Text = "";
            txtPrice.Text = "";
            cmbProductName.Text = "";
            pictureBox1.Image = null;
            pictureBox1.Image = Properties.Resources._12;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            HideErrors(this.Controls);
            cmbProductName.Focus();
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
                    BusinessManager BM = new BusinessManager();
                    // BM.PassImageToDatabase(pictureBox1.Image);
                    BOProduct BO = new BOProduct();
                    BO.ProductName = cmbProductName.Text;
                    BO.Features = txtFeatures.Text;
                    BO.Price = txtPrice.Text;
                    BO.Picture = BM.ImageToByteArray(pictureBox1.Image);
                    int rowsAffected = BM.BALInsertIntoConfig(BO);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                        txtPrice.Text = "";
                        txtFeatures.Text = "";
                        pictureBox1.Image = Properties.Resources._12;
                    }
                    else
                    {
                        MessageBox.Show("Couldn't be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Delete()
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    int RowsAffected = 0;
                    BusinessManager BM = new BusinessManager();
                    BOProduct BO = new BOProduct();
                    BO.ConfigID = txtConfigID.Text.Trim();
                    RowsAffected = BM.BALDeleteConfigData1(BO);

                    if (RowsAffected > 0)
                    {
                        MessageBox.Show("Successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        New();
                    }
                    else
                    {
                        MessageBox.Show("No Record Found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        New();
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete the record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Delete();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    BusinessManager BM = new BusinessManager();
                    // BM.PassImageToDatabase(pictureBox1.Image);
                    BOProduct BO = new BOProduct();
                    BO.ConfigID = txtConfigID.Text;
                    BO.ProductName = cmbProductName.Text;
                    BO.Features = txtFeatures.Text;
                    BO.Price = txtPrice.Text;
                    BO.Picture = BM.ImageToByteArray(pictureBox1.Image);
                    int rowsAffected = BM.BALUpdateConfigData1(BO);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnUpdate.Enabled = false;
                        txtPrice.Text = "";
                        txtFeatures.Text = "";
                        pictureBox1.Image = global::PresentationLayer.Properties.Resources._12;
                    }
                    else
                    {
                        MessageBox.Show("Couldn't be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmConfigData1 frm = new frmConfigData1();
            frm.Show();
            frm.getData();
        }

        public void FillProductCombo()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                cmbProductName.AutoCompleteSource = AutoCompleteSource.ListItems;
                //cmbProductName.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbProductName.DisplayMember = "ProductName";
                cmbProductName.DataSource = BM.BALFillProductCombo().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            FillProductCombo();
            cmbProductName.SelectedIndex = -1;
        }

        private void btnGetData_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void cmbProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else 
            {
                e.Handled = true;
            }
        }

        private void cmbProductName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbProductName.Text) || string.IsNullOrWhiteSpace(cmbProductName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbProductName, "Please select a product name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cmbProductName, "");
            }
        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPrice, "Please enter a price");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPrice, "");
            }
        }

        private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
