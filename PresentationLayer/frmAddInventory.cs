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
    public partial class frmAddInventory : Form
    {
        public frmAddInventory()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmAddInventory_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmConfigData2 frm = new frmConfigData2();
            frm.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnSave_Click(object sender, EventArgs e)
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
                BOI.InventoryDate = dtpInventoryDate.Text;
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
                    auto_generate();
                    int rowsAffected = BM.BALInsertIntoInventory(BOP, BOI);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtPrice.Text = "";
            txtFeatures.Text = "";
            txtProductName.Text = "";
            txtQuantity.Text = "";
            txtTotalPrice.Text = "";
            txtInventoryID.Text = "";
            dtpInventoryDate.Text = DateTime.Today.ToString();
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
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
    }
}
