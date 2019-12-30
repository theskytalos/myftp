using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFTP_Client.Forms
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.serverIPAddress = txtServerIP.Text;
            Properties.Settings.Default.serverIPPort = Convert.ToInt32(txtServerPort.Text);
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            txtServerIP.Text = Properties.Settings.Default.serverIPAddress;
            txtServerPort.Text = Properties.Settings.Default.serverIPPort.ToString();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
