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
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {            
            frmChangePassword frm = new frmChangePassword();
            frm.txtUsername.Text = label5.Text;
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDatabaseMgmt frm = new frmDatabaseMgmt();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                MessageBox.Show("Cannot select image.Please delete the existing image and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image (*.jpg *.jpeg *.png *.bmp) | *.jpg; *.jpeg; *.png; *.bmp";
                dialog.FilterIndex = 0;
                dialog.Title = "Select an image";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = dialog.FileName;
                    pictureBox1.Image = Image.FromFile(dialog.FileName);
                    btnSave.Enabled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete the image?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    BusinessManager BM = new BusinessManager();
                    int rowsAffected = BM.BALDeleteStoreImage();
                        MessageBox.Show("Store logo successfully deleted. You can add a new logo.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pictureBox1.Image = null;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BusinessManager BM = new BusinessManager();
            BOProduct BO = new BOProduct();
            BO.Picture = BM.ImageToByteArray(pictureBox1.Image);
            int rowsAffected = BM.BALSaveStoreImage(BO);
            if (rowsAffected > 0)
            {
                MessageBox.Show("Successfully saved. Please restart the application for the changes to take effect.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Couldn't be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            BusinessManager BM = new BusinessManager();
            byte[] data = BM.BALGetImageData();
            pictureBox1.Image = BM.BbyteArrayToImage(data);
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
