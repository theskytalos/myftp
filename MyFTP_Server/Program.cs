using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MyFTP_Server.Classes;

namespace MyFTP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Properties.Settings.Default.File_Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files");
            Properties.Settings.Default.Save();

            Console.Title = "MyFTP - Server";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Iniciando servidor...");

            TCPServer tcpServer = new TCPServer();

            tcpServer.start();
            tcpServer.acceptConnections();
            tcpServer.stop();
        }
    }
}
