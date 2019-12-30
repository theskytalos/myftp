using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using System.Windows.Forms;

namespace MyFTP_Client.Classes
{
    class TCPClient
    {
        public static TcpClient tcpClient { get; set; }
        public static int currentTransferedBytes { get; set; }
        public static int transferedBytes { get; set; }

        public TCPClient()
        {
            if (tcpClient == null)
                tcpClient = new TcpClient();

            currentTransferedBytes = 0;
            transferedBytes = 0;
        }

        public bool connect()
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect("127.0.0.1", 6790);
                return true;
            } catch (Exception)
            {
                MessageBox.Show("Não foi possível conectar ao servidor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void uploadFile(string filePath, string fileName, frmMain frmMain)
        {
            string[] commandArgs = { fileName };
            if (!sendCommand("put", commandArgs))
                return;

            Stream stream = tcpClient.GetStream();
            byte[] buffer = new byte[8192];

            stream.Read(buffer, 0, buffer.Length);

            if (Encoding.UTF8.GetString(buffer).Split(';')[0] == "FAIL")
            {
                MessageBox.Show(Encoding.UTF8.GetString(buffer).Split(';')[1], "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                activeGroupBox(frmMain.gpbServer);
                return;
            }
                
            FileStream fsFile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            
            setProgressBarValue(frmMain.pgbFileTransfer, 0);
            setProgressBarMaxValue(frmMain.pgbFileTransfer, Convert.ToInt32(fsFile.Length));

            stream.Write(BitConverter.GetBytes(fsFile.Length), 0, sizeof(long));

            currentTransferedBytes = 0;
            transferedBytes = 0;

            while (fsFile.Position != fsFile.Length)
            {
                int readBytes = fsFile.Read(buffer, 0, buffer.Length);

                stream.Write(buffer, 0, readBytes);

                currentTransferedBytes += readBytes;
                setProgressBarValue(frmMain.pgbFileTransfer, currentTransferedBytes);
            }

            fsFile.Close();

            MessageBox.Show("Upload Concluído", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            loadServerFiles(frmMain);
            setProgressBarValue(frmMain.pgbFileTransfer, 0);
            activeGroupBox(frmMain.gpbServer);
        }

        public void downloadFile(string fileName, string filePath, frmMain frmMain)
        {
            string[] commandArgs = { fileName };
            if (!sendCommand("get", commandArgs))
                return;
            
            Stream stream = tcpClient.GetStream();
            byte[] buffer = new byte[8192];

            stream.Read(buffer, 0, buffer.Length);

            if (Encoding.UTF8.GetString(buffer).Split(';')[0] == "FAIL")
            {
                MessageBox.Show(Encoding.UTF8.GetString(buffer).Split(';')[1], "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                activeGroupBox(frmMain.gpbServer);
                return;
            }

            FileStream fsFile = new FileStream(Path.Combine(filePath, fileName), FileMode.Create, FileAccess.Write);

            stream.Read(buffer, 0, sizeof(long));

            long fileSize = BitConverter.ToInt64(buffer, 0);

            setProgressBarValue(frmMain.pgbFileTransfer, 0);
            setProgressBarMaxValue(frmMain.pgbFileTransfer, Convert.ToInt32(fileSize));

            currentTransferedBytes = 0;
            transferedBytes = 0;

            while (fsFile.Position != fileSize)
            {
                int receivedBytes = stream.Read(buffer, 0, buffer.Length);
                currentTransferedBytes += receivedBytes;
                setProgressBarValue(frmMain.pgbFileTransfer, currentTransferedBytes);
                fsFile.Write(buffer, 0, receivedBytes);
            }

            fsFile.Close();

            MessageBox.Show("Download Concluído.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            setProgressBarValue(frmMain.pgbFileTransfer, 0);
            activeGroupBox(frmMain.gpbServer);
        }

        public void deleteFile(string fileName, frmMain frmMain)
        {
            string[] commandArgs = { fileName };
            if (!sendCommand("delete", commandArgs))
                return;

            Stream stream = tcpClient.GetStream();

            byte[] buffer = new byte[8192];

            stream.Read(buffer, 0, buffer.Length);

            if (Encoding.UTF8.GetString(buffer).Split(';')[0] == "FAIL")
                MessageBox.Show(Encoding.UTF8.GetString(buffer).Split(';')[1], "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                MessageBox.Show("Arquivo apagado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadServerFiles(frmMain);
            }     
        }

        delegate void loadServerFilesDelegate(frmMain ctrl);
        public static void loadServerFiles(frmMain ctrl)
        {
            if (ctrl.InvokeRequired)
            {
                loadServerFilesDelegate del = new loadServerFilesDelegate(loadServerFiles);
                ctrl.Invoke(del, ctrl);
            }
            else
            {
                ctrl.LoadServerFiles();
            }
        }

        delegate void activeGroupBoxDelegate(Control ctrl);
        public static void activeGroupBox(Control ctrl)
        {
            if (ctrl.InvokeRequired)
            {
                activeGroupBoxDelegate del = new activeGroupBoxDelegate(activeGroupBox);
                ctrl.Invoke(del, ctrl);
            }
            else
            {
                ctrl.Enabled = true;
            }
        }

        delegate void changeProgressBarDelegate(ProgressBar ctrl, int value);
        public static void setProgressBarValue(ProgressBar ctrl, int value)
        {
            if (ctrl.InvokeRequired)
            {
                changeProgressBarDelegate del = new changeProgressBarDelegate(setProgressBarValue);
                ctrl.Invoke(del, ctrl, value);
            }
            else
            {
                ctrl.Value = value;
            }
        }

        public static void setProgressBarMaxValue(ProgressBar ctrl, int value)
        {
            if (ctrl.InvokeRequired)
            {
                changeProgressBarDelegate del = new changeProgressBarDelegate(setProgressBarMaxValue);
                ctrl.Invoke(del, ctrl, value);
            }
            else
            {
                ctrl.Maximum = value;
            }
        }

        public static void changeProgressBarValue(ProgressBar ctrl, int value)
        {
            if (ctrl.InvokeRequired)
            {
                changeProgressBarDelegate del = new changeProgressBarDelegate(changeProgressBarValue);
                ctrl.Invoke(del, ctrl, value);
            }
            else
            {
                ctrl.Value += value;
            }
        }

        public List<string> listFiles()
        {
            List<string> serverFiles = new List<string>();

            if (!sendCommand("ls", null))
                return serverFiles;

            Stream stream = tcpClient.GetStream();

            byte[] buffer = new byte[8192];

            stream.Read(buffer, 0, buffer.Length);

            if (Encoding.UTF8.GetString(buffer).Split(';')[0] == "FAIL")
            {
                MessageBox.Show(Encoding.UTF8.GetString(buffer).Split(';')[1], "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return serverFiles;
            }

            stream.Read(buffer, 0, buffer.Length);
            long listSize = BitConverter.ToInt64(buffer, 0);
            int receivedStringBytes = 0;
            string files = String.Empty;
            
            while (receivedStringBytes < listSize)
            {
                int receivedBytes = stream.Read(buffer, 0, buffer.Length);
                files += Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                receivedStringBytes += receivedBytes;
            }

            if (files.Length > 0)
            {
                string[] files_pieces = files.Split(':');
                for (int i = 0; i < files_pieces.Length - 1; i++)
                    if (files_pieces[i].Trim().Length > 0)
                        serverFiles.Add(files_pieces[i]);
            }

            return serverFiles;
        }

        public bool sendCommand(string command, string[] args)
        {
            Stream stream = tcpClient.GetStream();
            byte[] buffer = null;

            switch (command)
            {
                case "login":
                    buffer = Encoding.UTF8.GetBytes(command + " " + args[0] + " " + args[1]);
                    break;
                case "put":
                    buffer = Encoding.UTF8.GetBytes(command + " " + args[0]);
                    break;
                case "get":
                    buffer = Encoding.UTF8.GetBytes(command + " " + args[0]);
                    break;
                case "delete":
                    buffer = Encoding.UTF8.GetBytes(command + " " + args[0]);
                    break;
                case "ls":
                    buffer = Encoding.UTF8.GetBytes(command);
                    break;
                case "adduser":
                    buffer = Encoding.UTF8.GetBytes(command + " " + args[0] + " " + args[1]);
                    break;
                case "removeuser":
                    buffer = Encoding.UTF8.GetBytes(command + " " + args[0] + " " + args[1]);
                    break;
                case "changepw":
                    buffer = Encoding.UTF8.GetBytes(command + " " + args[0] + " " + args[1] + " " + args[2]);
                    break;
            }

            stream.Write(buffer, 0, buffer.Length);

            byte[] bufferResponse = new byte[8192];

            stream.Read(bufferResponse, 0, bufferResponse.Length);

            if (Encoding.UTF8.GetString(bufferResponse).Split(';')[0] == "OK")
                return true;
            else if (Encoding.UTF8.GetString(bufferResponse).Split(';')[0] == "FAIL")
                MessageBox.Show("Comando Inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public void disconnect()
        {
            try
            {
                tcpClient.Close();
            } catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }
    }
}
