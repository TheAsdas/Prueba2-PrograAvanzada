using BombaBencinaThreads;
using RandomUtils;
using System;
using System.Configuration;
using System.Threading;

namespace BombaBencinaServer
{
    partial class Program
    {
        private static ServerThread serverThread;

        static void Main(string[] args)
        {
            ConsoleUtils.WriteLineWithColor("Bienvenido a la bencinera Copenca.", ConsoleColor.Magenta);

            StartServerThread();

            Thread.Sleep(2000);

            StartCommandMenu();
        }

        /// <summary>
        /// Comienza el hilo del servidor.
        /// </summary>
        private static void StartServerThread()
        {
            if (serverThread != null)
            {
                serverThread.Server.Close();
                serverThread.Abort = true;
            }

            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            serverThread = new ServerThread(puerto); ;
            Thread t = new Thread(new ThreadStart(serverThread.StartServer)); ;

            ConsoleUtils.WriteLineWithColor("Iniciando el hilo del servidor...", ConsoleColor.Yellow);
            t.IsBackground = true;
            t.Start();
        }

    }

}
