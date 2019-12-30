using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Threading;
using MyFTP_Client.Properties;
using MyFTP_Client.Classes;
using MyFTP_Client.Forms;

namespace MyFTP_Client
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadIcons();
            LoadServerFiles();
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdFile = new OpenFileDialog();

            if (ofdFile.ShowDialog() == DialogResult.OK)
            {
                TCPClient tcpClient = new TCPClient();

                try
                {
                    Thread uploadThread = new Thread(() => tcpClient.uploadFile(ofdFile.FileName, ofdFile.SafeFileName, this));
                    uploadThread.Start();
                    tmFileTransferSpeed.Start();
                    gpbServer.Enabled = false;
                } catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            TCPClient tcpClient = new TCPClient();

            if (lstServerFiles.SelectedItems.Count > 0)
            {
                string fileName = lstServerFiles.SelectedItems[0].Text;

                FolderBrowserDialog fbdDownloadFolder = new FolderBrowserDialog();

                if (fbdDownloadFolder.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Thread downloadThread = new Thread(() => tcpClient.downloadFile(fileName, fbdDownloadFolder.SelectedPath, this));
                        downloadThread.Start();
                        tmFileTransferSpeed.Start();
                        gpbServer.Enabled = false;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um arquivo para baixar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            TCPClient tcpClient = new TCPClient();

            if (TCPClient.tcpClient.Connected)
                tcpClient.disconnect();

            Application.Exit();
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            LoadServerFiles();
        }

        private void SairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void FecharToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Application.Exit();
        }

        private void ApagarUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userName = Microsoft.VisualBasic.Interaction.InputBox("Insira seu usuário.");
            string userPassword = Microsoft.VisualBasic.Interaction.InputBox("Insira sua senha.");

            if (MessageBox.Show("Tem certeza que deseja apagar sua conta?", "Absoluta?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TCPClient tcpClient = new TCPClient();

                if (!TCPClient.tcpClient.Connected)
                    if (!tcpClient.connect())
                        return;

                string[] commandArgs = { userName.Trim(), userPassword.Trim() };
                tcpClient.sendCommand("removeuser", commandArgs);

                Stream stream = TCPClient.tcpClient.GetStream();
                byte[] buffer = new byte[8192];

                stream.Read(buffer, 0, buffer.Length);

                string response = Encoding.UTF8.GetString(buffer);
                string[] responsePieces = response.Split(';');

                if (responsePieces[0] == "OK")
                    MessageBox.Show(responsePieces[1], "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (responsePieces[0] == "FAIL")
                    MessageBox.Show(responsePieces[1], "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);

                tcpClient.disconnect();
                Application.Restart();
            }
        }

        public void LoadServerFiles()
        {
            TCPClient tcpClient = new TCPClient();

            try
            {
                List<string> serverFiles = tcpClient.listFiles();

                lstServerFiles.Items.Clear();

                if (serverFiles.Count == 0)
                    return;

                DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                foreach (string fileName in serverFiles)
                {
                    if (fileName.Length > 3)
                    {
                        string fileExtension = fileName.Substring(fileName.Length - 4, 4);
                        if (!imglstIcons.Images.Keys.Contains(fileExtension))
                        {
                            imglstIcons.Images.Add(fileExtension, (Image)Resources.ResourceManager.GetObject("unknown_icon"));
                        }

                        ListViewItem lstvItem = new ListViewItem();
                        lstvItem.Text = fileName;
                        lstvItem.ImageIndex = imglstIcons.Images.Keys.IndexOf(fileExtension);
                        lstServerFiles.Items.Add(lstvItem);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void LoadIcons()
        {
            string[] fileIcons = { ".pdf", ".7z", ".rar", ".zip", ".png", ".jpg", ".jpeg", ".csv", ".txt", ".doc", ".docx", ".xls", ".exe", ".docx", ".mp4", ".mp3", ".avi", ".iso", ".dll", ".bin", ".wmv", ".mov", ".mpeg", ".dat", ".wma", ".mpg", ".3gp", ".webm", ".ogg", ".gz", ".tar.gz" };

            foreach (string fileIcon in fileIcons)
            {
                imglstIcons.Images.Add(fileIcon, (Image)Resources.ResourceManager.GetObject(fileIcon.Substring(1, fileIcon.Length - 1) + "_icon"));
            }
            
        }

        private void ConfiguraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig frmConfig = new frmConfig();
            frmConfig.Show();
        }

        private void LstServerFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void LstServerFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] droppedFiles = (string[]) e.Data.GetData(DataFormats.FileDrop);
            
            foreach (string droppedFile in droppedFiles)
            {
                string fileName = Path.GetFileName(droppedFile);

                TCPClient tcpClient = new TCPClient();

                try
                {
                    Thread uploadThread = new Thread(() => tcpClient.uploadFile(droppedFile, fileName, this));
                    uploadThread.Start();
                    tmFileTransferSpeed.Start();
                    gpbServer.Enabled = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void LstServerFiles_DoubleClick(object sender, EventArgs e)
        {
            TCPClient tcpClient = new TCPClient();

            if (lstServerFiles.SelectedItems.Count > 0)
            {
                string fileName = lstServerFiles.SelectedItems[0].Text;

                FolderBrowserDialog fbdDownloadFolder = new FolderBrowserDialog();

                if (fbdDownloadFolder.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Thread downloadThread = new Thread(() => tcpClient.downloadFile(fileName, fbdDownloadFolder.SelectedPath, this));
                        downloadThread.Start();
                        tmFileTransferSpeed.Start();
                        gpbServer.Enabled = false;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um arquivo para baixar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            TCPClient tcpClient = new TCPClient();

            if (lstServerFiles.SelectedItems.Count > 0)
            {
                string fileName = lstServerFiles.SelectedItems[0].Text;

                if (MessageBox.Show("Tem certeza que deseja apagar o arquivo \"" + fileName + "\"?", "Absoluta?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        tcpClient.deleteFile(fileName, this);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um arquivo para deletar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TmFileTransferSpeed_Tick(object sender, EventArgs e)
        {
            int dataPerSecond = (TCPClient.currentTransferedBytes - TCPClient.transferedBytes);

            tssFileTransferSpeed.Text = "Transferindo a ";
            if (dataPerSecond == 0)
            {
                tssFileTransferSpeed.Text = "Pronto";
                tmFileTransferSpeed.Stop();
            }
            else if (dataPerSecond > 0 && dataPerSecond < 1000)
            {
                tssFileTransferSpeed.Text += dataPerSecond + "b/s";
            }
            else if (dataPerSecond >= 1000 && dataPerSecond < 1000000)
            {
                tssFileTransferSpeed.Text += (dataPerSecond / 1000) + "kb/s";
            }
            else if (dataPerSecond >= 1000000 && dataPerSecond < 1000000000)
            {
                tssFileTransferSpeed.Text += (dataPerSecond / 1000000) + "Mb/s";
            }
            
            TCPClient.transferedBytes = TCPClient.currentTransferedBytes;
        }
    }
}
