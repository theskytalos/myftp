using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace MyFTP_Server.Classes
{
    class TCPServer
    {
        private TcpListener tcpListener;
        private Dictionary<string, string> loggedUsers = new Dictionary<string, string>();

        public bool start()
        {
            try
            {
                IPAddress ipAdress = IPAddress.Parse("127.0.0.1");

                tcpListener = new TcpListener(ipAdress, 6790);

                tcpListener.Start();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Servidor iniciado com sucesso.");

                return true;
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao iniciar o servidor: " + e.StackTrace);

                return false;
            }
        }

        public void acceptConnections()
        {
            try
            {
                while (true)
                {
                    Socket socket = tcpListener.AcceptSocket();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Novo cliente conectado: {0}.", socket.RemoteEndPoint);

                    Thread newClient = new Thread(() => connectClient(socket));
                    newClient.Start();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao aceitar novas conexões: " + e.StackTrace);
                stop();
            }
        }

        public void connectClient(Socket socket)
        {
            try
            {
                byte[] buffer = new byte[8192];

                while (true)
                {
                    int receivedBytes = socket.Receive(buffer);
                    string receivedCommand = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

                    if (!isValidCommand(receivedCommand))
                    {
                        if (receivedCommand.Trim().Length == 0)
                            break;

                        Console.WriteLine("O Comando \"{0}\" enviado pelo cliente {1} é inválido.", receivedCommand, socket.RemoteEndPoint.ToString());
                        socket.Send(Encoding.UTF8.GetBytes("FAIL;Comando inválido."));
                        continue;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Comando \"{0}\" recebido pelo cliente {1}.", receivedCommand, socket.RemoteEndPoint.ToString());

                    int sentBytes = socket.Send(Encoding.UTF8.GetBytes("OK;"));

                    string[] commandPieces = receivedCommand.Split(' ');

                    User user;
                    string message;

                    switch (commandPieces[0])
                    {
                        case "login":
                            user = new User();
                            message = String.Empty;
                            if (user.loginUser(commandPieces[1], commandPieces[2], ref message))
                            {
                                if (!loggedUsers.ContainsKey(socket.RemoteEndPoint.ToString()))
                                {
                                    loggedUsers.Add(socket.RemoteEndPoint.ToString(), user.userName);
                                    socket.Send(Encoding.UTF8.GetBytes("OK;" + message));
                                }
                                else
                                    socket.Send(Encoding.UTF8.GetBytes("FAIL;Você já está logado."));
                            }
                            else
                                socket.Send(Encoding.UTF8.GetBytes("FAIL;" + message));
                            break;
                        case "put":
                            receiveFile(socket, commandPieces[1]);
                            break;
                        case "get":
                            sendFile(socket, commandPieces[1]);
                            break;
                        case "delete":
                            deleteFile(socket, commandPieces[1]);
                            break;
                        case "ls":
                            listFiles(socket);
                            break;
                        case "adduser":
                            user = new User();
                            message = String.Empty;
                            if (user.addUser(commandPieces[1], commandPieces[2], ref message))
                            {
                                Directory.CreateDirectory(Path.Combine(Properties.Settings.Default.File_Path, commandPieces[1]));
                                socket.Send(Encoding.UTF8.GetBytes("OK;" + message));
                            }    
                            else
                                socket.Send(Encoding.UTF8.GetBytes("FAIL;" + message));
                            break;
                        case "removeuser":
                            user = new User();
                            message = String.Empty;
                            if (user.removeUser(commandPieces[1], commandPieces[2], ref message))
                            {
                                Directory.Delete(Path.Combine(Properties.Settings.Default.File_Path, commandPieces[1]), true);
                                socket.Send(Encoding.UTF8.GetBytes("OK;" + message));
                            }
                            else
                                socket.Send(Encoding.UTF8.GetBytes("FAIL;" + message));
                            break;
                        case "changepw":
                            user = new User();
                            message = String.Empty;
                            if (user.changePw(commandPieces[1], commandPieces[2], commandPieces[3], ref message))
                                socket.Send(Encoding.UTF8.GetBytes("OK;" + message));
                            else
                                socket.Send(Encoding.UTF8.GetBytes("FAIL;" + message));
                            break;
                    }
                }
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao receber novos comandos: " + e.ToString());
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("O cliente {0} foi desconectado.", socket.RemoteEndPoint.ToString());
        }

        public bool isValidCommand(string command)
        {
            Regex regex = new Regex(@"(^login [\w]+ [\w]+$|^adduser [\w]+ [\w]+$|^removeuser [\w]+ [\w]+$|^changepw [\w]+ [\w]+ [\w]+$|^put [\w]+.[\w]+$|^get [\w]+.[\w]+$|^delete [\w]+.[\w]+$|^ls$)");
            Match match = regex.Match(command);

            if (match.Success)
                return true;

            return false;
        }

        public void receiveFile(Socket socket, string fileName)
        {
            if (!loggedUsers.ContainsKey(socket.RemoteEndPoint.ToString()))
            {
                socket.Send(Encoding.UTF8.GetBytes("FAIL;Você não está logado."));
                return;
            }

            string userName = loggedUsers[socket.RemoteEndPoint.ToString()];
            string userDirectory = Path.Combine(Properties.Settings.Default.File_Path, userName);

            try
            {
                if (!File.Exists(Path.Combine(userDirectory, fileName)))
                {
                    FileStream fsFile = new FileStream(Path.Combine(userDirectory, fileName), FileMode.Create, FileAccess.Write);
                    byte[] buffer = new byte[8192];

                    socket.Send(Encoding.UTF8.GetBytes("OK;"));

                    socket.Receive(buffer, sizeof(long), SocketFlags.None);
                    long fileSize = BitConverter.ToInt64(buffer, 0);

                    while (fsFile.Position != fileSize)
                    {
                        int receivedBytes = socket.Receive(buffer);

                        fsFile.Write(buffer, 0, receivedBytes);
                    }

                    fsFile.Close();
                }
                else
                {
                    socket.Send(Encoding.UTF8.GetBytes("FAIL;Este arquivo já existe no servidor."));
                }
                
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao receber um arquivo: " + e.ToString());
            }
        }

        public void sendFile(Socket socket, string fileName)
        {
            if (!loggedUsers.ContainsKey(socket.RemoteEndPoint.ToString()))
            {
                socket.Send(Encoding.UTF8.GetBytes("FAIL;Você não está logado."));
                return;
            }

            string userName = loggedUsers[socket.RemoteEndPoint.ToString()];
            string userDirectory = Path.Combine(Properties.Settings.Default.File_Path, userName);

            if (!File.Exists(Path.Combine(userDirectory, fileName)))
            {
                socket.Send(Encoding.UTF8.GetBytes("FAIL;O arquivo desejado não existe."));
                return;
            }

            try
            {
                FileStream fsFile = new FileStream(Path.Combine(userDirectory, fileName), FileMode.Open, FileAccess.Read);
                byte [] buffer = new byte[8192];

                socket.Send(Encoding.UTF8.GetBytes("OK;"));

                socket.Send(BitConverter.GetBytes(fsFile.Length), sizeof(long), SocketFlags.None);

                while (fsFile.Position != fsFile.Length)
                {
                    int readBytes = fsFile.Read(buffer, 0, buffer.Length);

                    socket.Send(buffer, readBytes, SocketFlags.None);
                }

                fsFile.Close();
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao enviar um arquivo: " + e.ToString());
            }
        }

        public void deleteFile(Socket socket, string fileName)
        {
            if (!loggedUsers.ContainsKey(socket.RemoteEndPoint.ToString()))
            {
                socket.Send(Encoding.UTF8.GetBytes("FAIL;Você não está logado."));
                return;
            }

            string userName = loggedUsers[socket.RemoteEndPoint.ToString()];
            string userDirectory = Path.Combine(Properties.Settings.Default.File_Path, userName);

            if (!File.Exists(Path.Combine(userDirectory, fileName)))
            {
                socket.Send(Encoding.UTF8.GetBytes("FAIL;O arquivo desejado não existe."));
                return;
            }

            try
            {
                File.Delete(Path.Combine(userDirectory, fileName));
                socket.Send(Encoding.UTF8.GetBytes("OK;"));
            } catch (Exception)
            {
                socket.Send(Encoding.UTF8.GetBytes("FAIL;Não foi possível deletar o arquivo."));
            }
        }

        public void listFiles(Socket socket)
        {
            if (!loggedUsers.ContainsKey(socket.RemoteEndPoint.ToString()))
            {
                socket.Send(Encoding.UTF8.GetBytes("FAIL;Você não está logado."));
                return;
            }

            socket.Send(Encoding.UTF8.GetBytes("OK;"));

            string userName = loggedUsers[socket.RemoteEndPoint.ToString()];
            string userDirectory = Path.Combine(Properties.Settings.Default.File_Path, userName);

            DirectoryInfo dir = new DirectoryInfo(userDirectory);

            string directoryContent = String.Empty;

            foreach (FileInfo f in dir.GetFiles("*.*"))
            {
                directoryContent += f.Name + ":";
            }

            try
            {
                byte[] directoryContentBytes = Encoding.UTF8.GetBytes(directoryContent);
                byte[] buffer = new byte[8192];
                byte[] length = new byte[sizeof(long)];

                Array.Copy(BitConverter.GetBytes(directoryContentBytes.Length), 0, length, 0, BitConverter.GetBytes(directoryContentBytes.Length).Length);

                socket.Send(length, sizeof(long), SocketFlags.None);

                for (int i = 0; i < directoryContentBytes.Length; i += 8192)
                {
                    Array.Clear(buffer, 0, buffer.Length);

                    if (directoryContentBytes.Length - i >= 8192)
                        Array.Copy(directoryContentBytes, i, buffer, 0, buffer.Length);
                    else
                        Array.Copy(directoryContentBytes, i, buffer, 0, directoryContentBytes.Length - i);

                    socket.Send(buffer);
                }
            } catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
            }
        }
        
        public void stop()
        {
            try
            {
                tcpListener.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Servidor fechado com sucesso.");
            } catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao fechar o servidor: " + e.StackTrace);
            }
        }
    }
}
