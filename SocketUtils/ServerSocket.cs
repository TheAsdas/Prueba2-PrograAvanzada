using RandomUtils;
using System;
using System.Net;
using System.Net.Sockets;

namespace SocketUtils
{
    /// <summary>
    /// Conexión de un servidor.
    /// </summary>
    public class ServerSocket
    {
        private int port;
        private Socket server;
        private bool isStarted;

        /// <summary>
        /// ¿Se inició correctamente el servidor?
        /// </summary>
        public bool IsStarted { get => isStarted; }

        /// <summary>
        /// Define el puerto del servidor.
        /// </summary>
        /// <param name="puerto">Puerto donde se inicia el server.</param>
        public ServerSocket(int puerto)
        {
            this.port = puerto;
        }

        /// <summary>
        /// Inicia el servidor en el puerto definido.
        /// </summary>
        /// <returns><c>True</c>, si el servidor se inicia correctamente. <c>False</c> en caso contrario.</returns>
        public void Start()
        {
            try
            {
                //crear instancia de socket
                this.server = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );

                //hacer bind para tomar el control del puerto
                this.server
                    .Bind(new IPEndPoint(
                        IPAddress.Any, 
                        this.port)
                    );

                //escuchar una cantidad de clientes
                this.server.Listen(10);

                isStarted = true;
            }
            catch (Exception e)
            {
                ConsoleUtils.PrintException(e);
                isStarted = false;
            }
        }


        /// <summary>
        /// Hace que el servidor espere a la llegada de un cliente.
        /// </summary>
        /// <returns>El socket del cliente recibido, o <c>null</c> si ocurre un error.</returns>
        public ClientSocket WaitForClient()
        {
            try
            {
                //Detener la función hasta que el servidor encuentre a un cliente.
                return new ClientSocket(server.Accept());
            }
            catch(Exception e)
            {
                ConsoleUtils.PrintException(e);
                return null;
            }
        }

        /// <summary>
        /// Cierra el servidor.
        /// </summary>
        public void Close()
        {
            server.Close();
        }


    }
}
