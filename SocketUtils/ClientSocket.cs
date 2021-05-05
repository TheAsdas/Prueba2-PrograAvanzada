﻿using RandomUtils;
using System;
using System.IO;
using System.Net.Sockets;

namespace SocketUtils
{
    /// <summary>
    /// Conexión de un cliente.
    /// </summary>
    public class ClientSocket
    {
        private Socket client;
        private StreamWriter writer;
        private StreamReader reader;

        /// <summary>
        /// Crea la conexión con el cliente.
        /// </summary>
        /// <param name="client">Cliente a conectar.</param>
        public ClientSocket(Socket client)
        {
            this.client = client;

            //Define un stream, y un writer y reader para mensajes.
            Stream stream = new NetworkStream(this.client);
            this.writer = new StreamWriter(stream);
            this.reader = new StreamReader(stream);
        }

        /// <summary>
        /// Envía una cadena de texto al cliente.
        /// </summary>
        /// <param name="message">Cadena de texto.</param>
        public void Write(string message)
        {
            try
            {
                this.writer.WriteLine(message);
                this.writer.Flush();
            }
            catch (IOException e)
            {
                ConsoleUtils.PrintException(e);
            }
        }

        /// <summary>
        /// Recibe una cadena de texto del cliente.
        /// </summary>
        /// <returns>Un <c>string</c> con el mensaje, o <c>null</c> si ocurre un error de conexión.</returns>
        public string Read()
        {
            try
            {
                return reader.ReadLine().Trim();
            }
            catch (Exception e)
            {
                ConsoleUtils.PrintException(e);
                return null;
            }
        }

        /// <summary>
        /// Cierra la conexión con el cliente.
        /// </summary>
        public void Close()
        {
            client.Close();
        }
    }
}
