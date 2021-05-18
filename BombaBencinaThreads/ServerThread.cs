using RandomUtils;
using SocketUtils;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace BombaBencinaThreads
{
    public class ServerThread
    {
        private int port;
        private ServerSocket server;
        private bool abort = false;
        private List<ClientThread> connectedClients = new List<ClientThread>();

        public bool Abort { get => abort; set => abort = value; }
        public int Port { get => port; }
        public ServerSocket Server { get => server; }
        public List<ClientThread> ConnectedClients { get => connectedClients; }

        public ServerThread(int puerto)
        {
            port = puerto;
        }

        public void StartServer()
        {
            server = new ServerSocket(port);
            ConsoleUtils.WriteLineWithColor("Iniciando el servidor local en el puerto " + port, ConsoleColor.Yellow);

            server.Start();

            if (server.IsStarted)
            {
                ConsoleUtils.WriteLineWithColor("Server iniciado con éxito en 127.0.0.1:" + port, ConsoleColor.Green);
                StartReceptingClients();
            }
        }

        private void StartReceptingClients()
        {
            Console.WriteLine("El servidor ahora permite la conexión de clientes.\nEsperando...");

            while (!abort)
            {
                ClientSocket cs = server.WaitForClient();

                if (cs != null)
                {
                    ConsoleUtils.WriteLineWithColor("¡Cliente conectado!", ConsoleColor.Green);
                    StartClientThread(cs);
                }
            }

            server.Close();
        }

        private void StartClientThread(ClientSocket client)
        {
            ClientThread ct = new ClientThread(client, this);
            Thread t = new Thread(new ThreadStart(ct.RunClientTask));

            connectedClients.Add(ct);
            t.IsBackground = true;
            t.Start();
        }
    }
}
