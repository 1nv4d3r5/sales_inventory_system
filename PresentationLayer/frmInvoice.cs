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
    public partial class frmInvoice : Form
    {
        public frmInvoice()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSalesRecord frm = new frmSalesRecord();
            frm.groupBox3.Visible = false;
            frm.dataGridView1.DataSource = null;
            frm.cmbCustomerName.Text = "";
            frm.FillCombo();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomerDataRecord1 frm = new frmCustomerDataRecord1();
            frm.Show();
        }

        public double subtot()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            i = 0;
            j = 0;
            k = 0;


            try
            {

                j = listView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    k = k + Convert.ToInt32(listView1.Items[i].SubItems[5].Text);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return k;

        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
           
                try
                {
                    if (txtCustID.Text == "")
                    {
                        MessageBox.Show("Please retrieve Customer ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCustID.Focus();
                        return;
                    }

                    if (txtProductName.Text == "")
                    {
                        MessageBox.Show("Please retrieve product name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (txtSalesQty.Text == "")
                    {
                        MessageBox.Show("Please enter no. of sale quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSalesQty.Focus();
                        return;
                    }
                    int SaleQty = Convert.ToInt32(txtSalesQty.Text);
                    if (SaleQty == 0)
                    {
                        MessageBox.Show("no. of sale quantity can not be zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSalesQty.Focus();
                        return;
                    }

                    if (listView1.Items.Count == 0)
                    {

                        ListViewItem lst = new ListViewItem();
                        lst.SubItems.Add(txtConfigID.Text);
                        lst.SubItems.Add(txtProductName.Text);
                        lst.SubItems.Add(txtPrice.Text);
                        lst.SubItems.Add(txtSalesQty.Text);
                        lst.SubItems.Add(txtTotalAmount.Text);
                        listView1.Items.Add(lst);
                        txtSubTotal.Text = subtot().ToString();
                        if (txtVAT.Text != "")
                        {
                            txtVatAmount.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtVAT.Text) / 100)).ToString();
                            txtGrandTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtVatAmount.Text)).ToString();
                        }
                        int val1 = 0;
                        int val2 = 0;
                        int.TryParse(txtGrandTotal.Text, out val1);
                        int.TryParse(txtTotalPayment.Text, out val2);
                        int I = (val1 - val2);
                        txtPaymentDue.Text = I.ToString();
                        txtProductName.Text = "";
                        txtConfigID.Text = "";
                        txtPrice.Text = "";
                        txtAvailableQty.Text = "";
                        txtSalesQty.Text = "";
                        txtTotalAmount.Text = "";
                        txtSearchProductName.Text = "";
                        return;
                    }

                    for (int j = 0; j <= listView1.Items.Count - 1; j++)
                    {
                        if (listView1.Items[j].SubItems[1].Text == txtConfigID.Text)
                        {
                            listView1.Items[j].SubItems[1].Text = txtConfigID.Text;
                            listView1.Items[j].SubItems[2].Text = txtProductName.Text;
                            listView1.Items[j].SubItems[3].Text = txtPrice.Text;
                            listView1.Items[j].SubItems[4].Text = (Convert.ToInt32(listView1.Items[j].SubItems[4].Text) + Convert.ToInt32(txtSalesQty.Text)).ToString();
                            listView1.Items[j].SubItems[5].Text = (Convert.ToInt32(listView1.Items[j].SubItems[5].Text) + Convert.ToInt32(txtTotalAmount.Text)).ToString();
                            txtSubTotal.Text = subtot().ToString();
                            if (txtVAT.Text != "")
                            {
                                txtVAT.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtVAT.Text) / 100)).ToString();
                                txtGrandTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtVatAmount.Text)).ToString();
                            }
                            int val1 = 0;
                            int val2 = 0;
                            int.TryParse(txtGrandTotal.Text, out val1);
                            int.TryParse(txtTotalPayment.Text, out val2);
                            int I = (val1 - val2);
                            txtPaymentDue.Text = I.ToString();
                            txtProductName.Text = "";
                            txtConfigID.Text = "";
                            txtPrice.Text = "";
                            txtAvailableQty.Text = "";
                            txtSalesQty.Text = "";
                            txtTotalAmount.Text = "";
                            return;

                        }
                    }

                    ListViewItem lst1 = new ListViewItem();

                    lst1.SubItems.Add(txtConfigID.Text);
                    lst1.SubItems.Add(txtProductName.Text);
                    lst1.SubItems.Add(txtPrice.Text);
                    lst1.SubItems.Add(txtSalesQty.Text);
                    lst1.SubItems.Add(txtTotalAmount.Text);
                    listView1.Items.Add(lst1);
                    txtSubTotal.Text = subtot().ToString();
                    if (txtVAT.Text != "")
                    {
                        txtVatAmount.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtVAT.Text) / 100)).ToString();
                        txtGrandTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtVatAmount.Text)).ToString();
                    }
                    int val3 = 0;
                    int val4 = 0;
                    int.TryParse(txtGrandTotal.Text, out val3);
                    int.TryParse(txtTotalPayment.Text, out val4);
                    int I1 = (val3 - val4);
                    txtPaymentDue.Text = I1.ToString();
                    txtProductName.Text = "";
                    txtConfigID.Text = "";
                    txtPrice.Text = "";
                    txtAvailableQty.Text = "";
                    txtSalesQty.Text = "";
                    txtTotalAmount.Text = "";
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Cursor = Cursors.WaitCursor;
            //timer1.Enabled = true;
            BusinessManager BM = new BusinessManager();
            BOSales BO = new BOSales();
            BO.InvoiceNo = txtInvoiceNo.Text;
            rptInvoiceReport rpt = new rptInvoiceReport();
            rpt.SetDataSource(BM.BALInvoiceReport(BO));
            frmInvoiceReport frm = new frmInvoiceReport();
            frm.crystalReportViewer1.ReportSource = rpt;
            frm.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void txtVatAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVAT.Text) || string.IsNullOrWhiteSpace(txtVAT.Text))
                {
                    txtVatAmount.Text = "";
                    txtGrandTotal.Text = "";
                    return;
                }
                else
                {
                    int subtotal,vat,vatamount;
                    int.TryParse(txtSubTotal.Text, out subtotal);
                    int.TryParse(txtVAT.Text, out vat);
                    txtVatAmount.Text = ((subtotal * vat)/100).ToString();
                    int.TryParse(txtVatAmount.Text, out vatamount);
                    txtGrandTotal.Text = (subtotal + vatamount).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSalesQty_TextChanged(object sender, EventArgs e)
        {

            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtPrice.Text, out val1);
            int.TryParse(txtSalesQty.Text, out val2);
            int I = (val1 * val2);
            txtTotalAmount.Text = I.ToString();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                txtConfigID.Text = dr.Cells[1].Value.ToString();
                txtProductName.Text = dr.Cells[2].Value.ToString();
                txtPrice.Text = dr.Cells[4].Value.ToString();
                txtAvailableQty.Text = dr.Cells[5].Value.ToString();
                txtSalesQty.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                DataTable dt = BM.BALGetInvoiceData();
                dataGridView1.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("No items to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int itmCnt = 0;
                    int i = 0;
                    int t = 0;

                    listView1.FocusedItem.Remove();
                    itmCnt = listView1.Items.Count;
                    t = 1;

                    for (i = 1; i <= itmCnt + 1; i++)
                    {
                        t = t + 1;
                    }
                    txtSubTotal.Text = subtot().ToString();
                    if (txtVAT.Text != "")
                    {
                        txtVatAmount.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtVAT.Text) / 100)).ToString();
                        txtGrandTotal.Text = (Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtVatAmount.Text)).ToString();
                    }
                    int val1 = 0;
                    int val2 = 0;
                    int.TryParse(txtGrandTotal.Text, out val1);
                    int.TryParse(txtTotalPayment.Text, out val2);
                    int I = (val1 - val2);
                    txtPaymentDue.Text = I.ToString();
                }

                btnRemove.Enabled = false;
                if (listView1.Items.Count == 0)
                {
                    txtSubTotal.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BusinessManager BM = new BusinessManager();
            BOProduct BO = new BOProduct();
            BO.ProductName = txtSearchProductName.Text;
            dataGridView1.DataSource = BM.BALSearchInvoiceData(BO);
        }

        private void frmInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.lblUser.Text = lblCashier.Text;
            frm.Show();
        }

        private void New()
        {
            txtInvoiceNo.Text = "";
            dtpInvoiceDate.Text = DateTime.Today.ToString();
            txtCustID.Text = "";
            txtCustName.Text = "";
            txtProductName.Text = "";
            txtConfigID.Text = "";
            txtPrice.Text = "";
            txtAvailableQty.Text = "";
            txtSalesQty.Text = "";
            txtTotalAmount.Text = "";
            listView1.Items.Clear();
            txtSubTotal.Text = "";
            txtVAT.Text = "";
            txtVatAmount.Text = "";
            txtGrandTotal.Text = "";
            txtTotalPayment.Text = "";
            txtPaymentDue.Text = "";
            txtSearchProductName.Text = "";
            txtRemarks.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            btnPrint.Enabled = false;
            listView1.Enabled = true;
            btnAddCart.Enabled = true;
            HideErrors(this.Controls);
        }

        private void HideErrors(Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                errorProvider1.SetError(ctl, null);
                HideErrors(ctl.Controls);
            }
        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            New();
        }

        private void auto_generate_id()
        {
            txtInvoiceNo.Text = "INV-" + GetUniqueKey(1, 10000);
        }

        public static string GetUniqueKey(int min_value, int max_value)
        {
            Random rnd = new Random();
            int maxSize = rnd.Next(min_value, max_value);
            return maxSize.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    if (txtCustID.Text == "")
                    {
                        MessageBox.Show("Please retrieve Customer ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCustID.Focus();
                        return;
                    }

                    if (txtVAT.Text == "")
                    {
                        MessageBox.Show("Please enter VAT percentage", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtVAT.Focus();
                        return;
                    }

                    if (txtTotalPayment.Text == "")
                    {
                        MessageBox.Show("Please enter total payment", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTotalPayment.Focus();
                        return;
                    }
                    if (listView1.Items.Count == 0)
                    {
                        MessageBox.Show("Sorry no product added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    auto_generate_id();
                    BusinessManager BM = new BusinessManager();
                    BOSales BOS = new BOSales();
                    BOCustomer BOC = new BOCustomer();
                    BOProduct BOP = new BOProduct();
                    BOInventory BOI = new BOInventory();
                    BOS.InvoiceNo = txtInvoiceNo.Text;
                    BOS.InvoiceDate = dtpInvoiceDate.Text;
                    BOC.CustomerID = txtCustID.Text;
                    BOS.SubTotal = txtSubTotal.Text;
                    BOS.VATPercent = txtVAT.Text;
                    BOS.VATAmount = txtVatAmount.Text;
                    BOS.GrandTotal = txtGrandTotal.Text;
                    BOS.TotalPayment = txtTotalPayment.Text;
                    BOS.PaymentDue = txtPaymentDue.Text;
                    BOS.Remarks = txtRemarks.Text;

                    bool res = BM.BALVerifyInvoiceNumber(BOS);

                    if (res == true)
                    {
                        MessageBox.Show("Invoice No. already exists", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        BM.BALInsertIntoSales(BOS, BOC);
                    }

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        BOS.InvoiceNo = txtInvoiceNo.Text;
                        BOP.ConfigID = listView1.Items[i].SubItems[1].Text;
                        BOP.ProductQuantity = listView1.Items[i].SubItems[4].Text;
                        BOP.Price = listView1.Items[i].SubItems[3].Text;
                        BOP.TotalAmount = listView1.Items[i].SubItems[5].Text;
                        BM.BALInsertIntoProductSold(BOS, BOP);
                    }

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        BOI.Quantity = listView1.Items[i].SubItems[4].Text;
                        BOP.ConfigID = listView1.Items[i].SubItems[1].Text;
                        BM.BALUpdateInventoryQuantity(BOI, BOP);
                    }

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        BOI.TotalPrice = listView1.Items[i].SubItems[5].Text;
                        BOP.ConfigID = listView1.Items[i].SubItems[1].Text;
                        BM.BALUpdateInventoryTotalPrice(BOI, BOP);
                    }

                    btnSave.Enabled = false;
                    btnPrint.Enabled = true;
                    dataGridView1.DataSource = BM.BALGetInvoiceData();
                    MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTotalPayment_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtGrandTotal.Text, out val1);
            int.TryParse(txtTotalPayment.Text, out val2);
            int I = (val1 - val2);
            txtPaymentDue.Text = I.ToString();
        }

        private void txtSalesQty_Validating(object sender, CancelEventArgs e)
        {

            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtAvailableQty.Text, out val1);
            int.TryParse(txtSalesQty.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Selling quantity is more than available quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSalesQty.Text = "";
                txtTotalAmount.Text = "";
                txtSalesQty.Focus();
                return;
            }

            //if (string.IsNullOrEmpty(txtSalesQty.Text) || string.IsNullOrWhiteSpace(txtSalesQty.Text))
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(txtSalesQty, "Please enter a sales quantity");
            //}
            //else 
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(txtSalesQty, "");
            //}
        }

        private void Delete()
        {
            try
            {
                BusinessManager BM = new BusinessManager();
                BOSales BO = new BOSales();
                BO.InvoiceNo = txtInvoiceNo.Text;
                int rowsAffected = BM.BALDeleteInvoiceData(BO);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("The record has been successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    New();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Delete();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                BusinessManager BM = new BusinessManager();
                BOSales BOS = new BOSales();
                BOProduct BOP = new BOProduct();
                BOInventory BOI = new BOInventory();
                BOS.GrandTotal = txtGrandTotal.Text;
                BOS.TotalPayment = txtTotalPayment.Text;
                BOS.PaymentDue = txtPaymentDue.Text;
                BOS.Remarks = txtRemarks.Text;
                BOS.InvoiceNo = txtInvoiceNo.Text;

                try
                {
                    BM.BALUpdateSales(BOS);

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        BOP.ProductQuantity = listView1.Items[i].SubItems[4].Text;
                        BOP.Price = listView1.Items[i].SubItems[3].Text;
                        BOP.TotalAmount = listView1.Items[i].SubItems[5].Text;
                        BOS.InvoiceNo = txtInvoiceNo.Text;
                        BOP.ConfigID = listView1.Items[i].SubItems[1].Text;
                        BM.BALUpdateProductSold(BOS, BOP);
                    }

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        BOS.InvoiceNo = txtInvoiceNo.Text;
                        BOP.ConfigID = listView1.Items[i].SubItems[1].Text;
                        BOP.ProductQuantity = listView1.Items[i].SubItems[4].Text;
                        BOP.Price = listView1.Items[i].SubItems[3].Text;
                        BOP.TotalAmount = listView1.Items[i].SubItems[5].Text;
                        BM.BALInsertIntoProductSold(BOS, BOP);
                    }

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        BOI.TotalPrice = listView1.Items[i].SubItems[5].Text;
                        BOP.ConfigID = listView1.Items[i].SubItems[1].Text;
                        BM.BALUpdateInventoryTotalPrice(BOI, BOP);
                    }
                    dataGridView1.DataSource = BM.BALGetInvoiceData();
                    btnUpdate.Enabled = false;
                    MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearchProductName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSalesQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCustID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustID.Text) || string.IsNullOrWhiteSpace(txtCustID.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCustID, "Please select a customer ID");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCustID, "");
            }
        }

        private void txtProductName_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            //{
            //    e.Cancel = true;
            //    errorProvider1.SetError(txtProductName, "Please select a Product Name");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(txtProductName, "");
            //}
        }

        private void txtVAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtVAT_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtTotalPayment_Validating(object sender, CancelEventArgs e)
        {
            int totalpayment, grandtotal;
            int.TryParse(txtTotalPayment.Text, out totalpayment);
            int.TryParse(txtGrandTotal.Text, out grandtotal);
            if (totalpayment > grandtotal)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTotalPayment, "Total payment cannot be greater than grand total");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTotalPayment, "");
            }
        }
    }
}
