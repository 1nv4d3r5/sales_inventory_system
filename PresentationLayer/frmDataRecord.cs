using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace PresentationLayer
{
    public partial class frmDataRecord : Form
    {
        public frmDataRecord()
        {
            InitializeComponent();
        }

        private void btnProductManagement_Click(object sender, EventArgs e)
        {
            frmInventoryData1 frm = new frmInventoryData1();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            frmCustomerDataRecord3 frmCustomer = new frmCustomerDataRecord3();
            frmCustomer.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            frmSalesRecord1 frm = new frmSalesRecord1();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmProductRecord1 frm = new frmProductRecord1();
            frm.ShowDialog();
        }

        private void frmDataRecord_Load(object sender, EventArgs e)
        {

        }

        private void frmDataRecord_MdiChildActivate(object sender, EventArgs e)
        {

        }
    }
}
