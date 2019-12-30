using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MyFTP_Client.Classes;

namespace MyFTP_Client.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        { 
            Application.Exit();
        }

        private void loginUser()
        {
            if (txtUsername.Text.Trim().Length != 0 && txtPassword.Text.Trim().Length != 0)
            {
                TCPClient tcpClient = new TCPClient();

                if (!TCPClient.tcpClient.Connected)
                    if (!tcpClient.connect())
                        return;

                string[] commandArgs = { txtUsername.Text, txtPassword.Text };
                if (!tcpClient.sendCommand("login", commandArgs))
                    return;

                Stream stream = TCPClient.tcpClient.GetStream();
                byte[] buffer = new byte[8192];

                stream.Read(buffer, 0, buffer.Length);

                string response = Encoding.UTF8.GetString(buffer);
                string[] responsePieces = response.Split(';');

                if (responsePieces[0] == "OK")
                {
                    MessageBox.Show(responsePieces[1], "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmMain frmMain = new frmMain();
                    this.Hide();
                    frmMain.ShowDialog();
                }
                else if (responsePieces[0] == "FAIL")
                    MessageBox.Show(responsePieces[1], "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Os campos usuário e senha não podem estar vazios", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            loginUser();
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length != 0 && txtPassword.Text.Trim().Length != 0)
            {
                TCPClient tcpClient = new TCPClient();

                if (!TCPClient.tcpClient.Connected)
                    if (!tcpClient.connect())
                        return;
                    
                string[] commandArgs = { txtUsername.Text, txtPassword.Text };
                if (!tcpClient.sendCommand("adduser", commandArgs))
                    return;

                Stream stream = TCPClient.tcpClient.GetStream();
                byte[] buffer = new byte[8192];

                stream.Read(buffer, 0, buffer.Length);

                string response = Encoding.UTF8.GetString(buffer);
                string[] responsePieces = response.Split(';');

                if (responsePieces[0] == "OK")
                    MessageBox.Show(responsePieces[1], "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (responsePieces[0] == "FAIL")
                    MessageBox.Show(responsePieces[1], "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Os campos usuário e senha não podem estar vazios", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void LnkChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtUsername.Text.Trim().Length != 0 && txtPassword.Text.Trim().Length != 0)
            {
                string newPassword = Microsoft.VisualBasic.Interaction.InputBox("Digite a senha nova.");

                if (newPassword.Trim().Length == 0)
                    return;

                TCPClient tcpClient = new TCPClient();

                if (!TCPClient.tcpClient.Connected)
                    if (!tcpClient.connect())
                        return;

                string[] commandArgs = { txtUsername.Text.Trim(), txtPassword.Text.Trim(), newPassword.Trim() };
                if (!tcpClient.sendCommand("changepw", commandArgs))
                    return;

                Stream stream = TCPClient.tcpClient.GetStream();
                byte[] buffer = new byte[8192];

                stream.Read(buffer, 0, buffer.Length);

                string response = Encoding.UTF8.GetString(buffer);
                string[] responsePieces = response.Split(';');

                if (responsePieces[0] == "OK")
                    MessageBox.Show(responsePieces[1], "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (responsePieces[0] == "FAIL")
                    MessageBox.Show(responsePieces[1], "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Os campos usuário e senha não podem estar vazios", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            TCPClient tcpClient = new TCPClient();

            if (TCPClient.tcpClient.Connected)
                tcpClient.disconnect();
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            frmConfig frmConfig = new frmConfig();
            frmConfig.Show();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginUser();
            }
        }
    }
}
