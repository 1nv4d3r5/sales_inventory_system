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
    public partial class frmInventoryMgmt : Form
    {
        public frmInventoryMgmt()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
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
            txtPrice.Text = "";
            txtFeatures.Text = "";
            txtProductName.Text = "";
            txtQuantity.Text = "";
            txtTotalPrice.Text = "";
            txtInventoryID.Text = "";
            dtpInventoryDate.Text = DateTime.Today.ToString();
            txtSearch.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnRegister.Enabled = true;
            HideErrors(this.Controls);
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            New();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (txtProductName.Text == "")
                {
                    MessageBox.Show("Please select a product name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductName.Focus();
                    return;
                }

                if (txtQuantity.Text == "")
                {
                    MessageBox.Show("Please enter a quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.Focus();
                    return;
                }
                try
                {
                    BusinessManager BM = new BusinessManager();
                    BOProduct BOP = new BOProduct();
                    BOInventory BOI = new BOInventory();
                    BOP.ConfigID = txtConfigID.Text;
                    BOI.InventoryDate = dtpInventoryDate.Text;
                    auto_generate();
                    BOI.InventoryID = txtInventoryID.Text;
                    BOI.Quantity = txtQuantity.Text;
                    BOI.TotalPrice = txtTotalPrice.Text;
                    bool res = BM.BALVerifyInventoryName(BOP);

                    if (res == true)
                    {
                        MessageBox.Show("Record already exists!" + "\n" + "Please update the stock of product", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        int rowsAffected = BM.BALInsertIntoInventory(BOP, BOI);

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnRegister.Enabled = false;
                            getData();
                            //frmMainMenu frm = new frmMainMenu();
                            //frm.GetData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Delete()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOInventory BOI = new BOInventory();
                BOI.InventoryID = txtInventoryID.Text;
                int RowsAffected = BM.BALDeleteFromInventory(BOI);

                if (RowsAffected > 0)
                {
                    MessageBox.Show("The record has been successfully deleted", "Successfully Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    New();
                }
                else
                {
                    MessageBox.Show("No record found", "No Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Delete();
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
                    BOProduct BOP = new BOProduct();
                    BOInventory BOI = new BOInventory();
                    BOP.ConfigID = txtConfigID.Text;
                    BOI.InventoryID = txtInventoryID.Text;
                    BOI.InventoryDate = dtpInventoryDate.Text;
                    BOI.TotalPrice = txtTotalPrice.Text;
                    BOI.Quantity = txtQuantity.Text;
                    int RowsAffected = BM.BALUpdateIntoInventory(BOP, BOI);
                    if (RowsAffected > 0)
                    {
                        MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnUpdate.Enabled = false;
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Record couldn't be updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        public void getData()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                DataSet ds = BM.BALGetInventoryData();
                dataGridView1.DataSource = ds.Tables["Inventory"].DefaultView;
                dataGridView1.DataSource = ds.Tables["Config"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int getRandom(int min, int max)
        {
            Random rnd = new Random();
            int number = rnd.Next(min, max);
            return number;
        }

        private void auto_generate()
        {
            txtInventoryID.Text = "I-" + getRandom(100, 1000);
        }

        private void frmInventoryMgmt_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmInventoryData frm = new frmInventoryData();
            frm.Show();
            frm.getData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmConfigData frm = new frmConfigData();
            frm.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOProduct BO = new BOProduct();
                BO.ProductName = txtSearch.Text;
                DataSet ds = BM.BALSearchInventory(BO);
                dataGridView1.DataSource = ds.Tables["Inventory"].DefaultView;
                dataGridView1.DataSource = ds.Tables["Config"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtPrice.Text, out val1);
            int.TryParse(txtQuantity.Text, out val2);
            int I = (val1 * val2);
            txtTotalPrice.Text = I.ToString();
        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProductName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtProductName, "Please select a product name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtProductName, "");
            }
        }

        private void txtQuantity_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtQuantity, "Please enter a quantity");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtQuantity, "");
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void frmInventoryMgmt_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
