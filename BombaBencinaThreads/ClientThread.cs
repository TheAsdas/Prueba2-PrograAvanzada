using BombaBencinaModel.DAL.Interface;
using BombaBencinaModel.DAL.Factory;
using RandomUtils;
using RandomUtils.Exceptions;
using SocketUtils;
using System;
using System.Text.Json;
using BombaBencinaModel.DTO.Abstract;
using BombaBencinaModel.DTO;

namespace BombaBencinaThreads
{
    

    public class ClientThread
    {
        private ClientSocket client;
        private ServerThread server;
        private static IReading dal = ReadingFactory.CreateDAL();

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
            bool _isCorrect;
            string _meterId;
            string _clientStatus;

            RecieveInitialMessage(out _isCorrect, out _meterId);
            SendInitialResponse(_isCorrect, _meterId);
            RecieveClientStatus(out _clientStatus);
            StoreStatus(_clientStatus);

            ConsoleUtils.WriteLineWithColor("Un cliente se ha desconectado...", ConsoleColor.Red);
            server.ConnectedClients.Remove(this);
        }

        /// <summary>
        /// Detecta qué tipo de historial es el que se está intentando guardar.
        /// </summary>
        /// <param name="clientStatus"></param>
        private void StoreStatus(string clientStatus)
        {
            string[] _data = clientStatus.Split('|');

            switch (_data[2])
            {
                case "consumo":
                    StoreElectricStatus(_data);
                    break;
                case "tráfico":
                    StoreTrafficStatus(_data);
                    break;
                default:
                    throw new InvalidMeterException();
            }
        }

        private void StoreTrafficStatus(string[] data)
        {
            TrafficHistory _history = new TrafficHistory()
            {
                StationId = Convert.ToInt32(data[0]),
                DateSent = DateUtils.ParseToDateTime(data[1]),
                CarsParked = Convert.ToInt32(data[3]),
            };

            lock(dal)
            {
                dal.RegisterReading(_history);
            }
        }

        /// <summary>
        /// Genera y guarda una entrada en el historial de medidores eléctricos.
        /// </summary>
        /// <param name="data"></param>
        private void StoreElectricStatus(string[] data)
        {
            ElectricHistory _history = new ElectricHistory()
            {
                DateSent = DateUtils.ParseToDateTime(data[1]),
                KwhInUse = float.Parse(data[3]),
                RequiresMaintenence = data[4] == "2",
                StationId = Convert.ToInt32(data[0]),

            };

            lock(dal)
            {
                dal.RegisterReading(_history);
            }
        }

        /// <summary>
        /// Recibe el mensaje de actualización del cliente y lo guarda en una referencia.
        /// </summary>
        /// <param name="clientStatus">Referenncia donde guardar el estatus del cliente.</param>
        private void RecieveClientStatus(out string clientStatus)
        {
            clientStatus = client.Read();
            Console.WriteLine(clientStatus);
        }

        /// <summary>
        /// Recibe y procesa el mensaje inicial proveniente del cliente.
        /// </summary>
        /// <param name="isCorrect">Referencia a la validez del medidor.</param>
        /// <param name="meterId">Referencia a la ID del medidor.</param>
        private void RecieveInitialMessage(out bool isCorrect, out string meterId)
        {
            string _initialMessage = client.Read();
            Console.WriteLine(_initialMessage);

            string[] data = _initialMessage.Split('|');

            //flags
            bool _a = DateChecksOut(data[0]);
            bool _b = IdChecksOut(data[1], data[2]);

            isCorrect = (_a && _b);
            meterId = data[1];
        }

        /// <summary>
        /// Envía una respuesta al clietne dependiendo de los resultados del análisis del mensaje inicial.
        /// </summary>
        /// <param name="isCorrect">Define si el medidor es válido.</param>
        /// <param name="meterId">ID del medidor conectado.</param>
        private void SendInitialResponse(bool isCorrect, string meterId)
        {
            string _date = DateUtils.GetDate();
            string _status = isCorrect ? "WAIT" : "ERROR"; 
            client.Write(string.Format("{0}|{1}|{2}", _date, meterId, _status));
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
            DateTime _clientTime = DateUtils.ParseToDateTime(date);
            DateTime _timeOut = DateTime.Now.AddMinutes(-30);

            return (_clientTime <= _timeOut);
        }
    }
}
