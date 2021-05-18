using BombaBencinaModel.DAL.Interface;
using BombaBencinaModel.DAL.Factory;
using RandomUtils;
using SocketUtils;
using System;

namespace BombaBencinaThreads
{
    public class ClientThread
    {
        private ClientSocket client;
        private ServerThread server;

        public ClientThread(
            ClientSocket client,
            ServerThread server)
        {
            this.client = client;
            this.server = server;
        }

        /// <summary>
        /// Lista de tareas del servidor al conectarse con el cliente.
        /// </summary>
        public void RunClientTask()
        {
            bool isCorrect;
            string meterId;

            RecieveInitialMessage(out isCorrect, out meterId);
            SendInitialResponse(isCorrect, meterId);

            ConsoleUtils.WriteLineWithColor("Un cliente se ha desconectado...", ConsoleColor.Red);
            server.ConnectedClients.Remove(this);
        }

        /// <summary>
        /// Recibe y procesa el mensaje inicial proveniente del cliente.
        /// </summary>
        /// <param name="isCorrect">Referencia a la validez del medidor.</param>
        /// <param name="meterId">Referencia a la ID del medidor.</param>
        private void RecieveInitialMessage(out bool isCorrect, out string meterId)
        {
            string initialMessage = client.Read();
            Console.WriteLine(initialMessage);

            string[] data = initialMessage.Split('|');

            //flags
            bool a = DateChecksOut(data[0]);
            bool b = IdChecksOut(data[1], data[2]);

            isCorrect = (a && b);
            meterId = data[1];
        }

        /// <summary>
        /// Envía una respuesta al clietne dependiendo de los resultados del análisis del mensaje inicial.
        /// </summary>
        /// <param name="isCorrect">Define si el medidor es válido.</param>
        /// <param name="meterId">ID del medidor conectado.</param>
        private void SendInitialResponse(bool isCorrect, string meterId)
        {
            string date = DateUtils.GetDate();
            string status = isCorrect ? "WAIT" : "ERROR"; 
            client.Write(string.Format("{0}|{1}|{2}", date, meterId, status));
        }

        /// <summary>
        /// Chequea que la ID del medidor exista en las listas de IDs válidas.
        /// </summary>
        /// <param name="id">ID del medidor.</param>
        /// <param name="type">Tipo de medidor.</param>
        /// <returns><c>True</c> si la ID existe, <c>false</c> en caso contrario.</returns>
        private bool IdChecksOut(string id, string type)
        {
            if (type == "tráfico")
            {
                ITrafficMeter dal = TrafficMeterFactory.CreateDal();

                foreach (int validId in dal.GetTrafficMeters())
                {
                    if (validId.ToString() == id) return true;
                }
            }
            else
            {
                IElectricMeter dal = ElectricMeterFactory.CreateDal();

                foreach(int validId in dal.GetElectricMeters())
                {
                    if (validId.ToString() == id) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Chequea que la fecha esté dentro del rango válido.
        /// </summary>
        /// <param name="date">Fecha para chequear</param>
        /// <returns><c>True</c> si está está dentro del rango, <c>false</c> en caso contrario.</returns>
        private bool DateChecksOut(string date)
        {
            DateTime clientTime = DateUtils.ParseToDateTime(date);
            DateTime timeOut = DateTime.Now.AddMinutes(-30);

            return (clientTime <= timeOut);
        }
    }
}
