using RandomUtils;
using SocketUtils;
using System;

namespace BombaBencinaThreads
{
    public class ClientThread
    {
        private ClientSocket client;
        private ServerThread connectedIn;
        
        public ClientThread(
            ClientSocket client,
            ServerThread server)
        {
            this.client = client;
            connectedIn = server;
        }

        public void RunClientTask()
        {
            client.Write("Felicidades. La conexión ha sido exitosa.");
            client.Write("TODO: Funciones del cliente.");
            client.Close();

            ConsoleUtils.WriteLineWithColor("Un cliente se ha desconectado.", ConsoleColor.DarkYellow);

            connectedIn.ConnectedClients.Remove(this);
        }
    }
}
