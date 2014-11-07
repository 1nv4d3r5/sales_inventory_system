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
    public partial class frmSplashScreen : Form
    {
        public frmSplashScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            frmLogin login = new frmLogin();
        
            this.progressBar1.Value = this.progressBar1.Value + 2;

            if (this.progressBar1.Value == 10)
            {
                label1.Text = "Reading modules...";
            }
            else if (this.progressBar1.Value == 20)
            {
                label1.Text = "Turning on modules...";
            }
            else if (this.progressBar1.Value == 40)
            {
                label1.Text = "Starting modules...";
            }
            else if (this.progressBar1.Value == 60)
            {
                label1.Text = "Loading modules...";
            }
            else if (this.progressBar1.Value == 80)
            {
                label1.Text = "Modules loaded...Initializing...";
            }
            else if (this.progressBar1.Value == 100)
            {
                login.Show();
                timer1.Enabled = false;
                this.Hide();
            }
        }

        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            progressBar1.Width = this.Width;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
