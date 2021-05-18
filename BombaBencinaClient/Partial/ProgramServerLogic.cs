using BombaBencinaModel.DTO;
using RandomUtils;
using SocketUtils;
using System;
using System.Configuration;

namespace BombaBencinaClient
{
    partial class Program
    {
        private static string dateFormat = "yyyy-MM-dd-hh-mm-ss";

        private static void ConnectIntoServer()
        {
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"].ToString();

            try
            {
                ConsoleUtils.WriteLineWithColor("Conectando con el servidor local en el puerto " + port + "...", ConsoleColor.Yellow);

                client = new ClientSocket(ip, port);
            }
            catch (Exception e)
            {
                ConsoleUtils.PrintException(e);
                SetPort();
                ConnectIntoServer();
            }
        }

        /// <summary>
        /// Define un nuevo puerto que se guarda en el AppSettings.
        /// </summary>
        private static void SetPort()
        {
            ConfigurationManager.AppSettings["port"] = ConsoleUtils.GetConsoleInput("Defina un nuevo puerto:");
        }

        /// <summary>
        /// Cierra la conexión con el cliente.
        /// </summary>
        private static void CloseConnection()
        {
            client.Close();
            ConsoleUtils.WriteLineWithColor("Se ha desconectado del servidor.", ConsoleColor.Red);
        }

        /// <summary>
        /// Lista de tareas por hacer del cliente después de ser conectado con el servidor.
        /// </summary>
        private static void DoClientTask()
        {
            bool responseIsCorrenct;

            SendInitialMessage();
            RecieveInitialResponse(out responseIsCorrenct);
            if (responseIsCorrenct) SendUpdateMessage();
        }

        /// <summary>
        /// Recibe y analiza la respuesta incial proveniente del servidor.
        /// </summary>
        private static void RecieveInitialResponse(out bool responseIsCorrect)
        {
            string response = client.Read();
            string[] data = response.Split('|');

            Console.WriteLine(response);

            responseIsCorrect = (data[2] == "WAIT");
        }

        private static void SendUpdateMessage()
        {
            string id = meter.Id.ToString();
            string type = meter.GetType().Name;
            string value;
            string state;
            string date = DateUtils.GetDate();

            if (meter.GetType().Name == "TrafficMeter")
            {
                value = (meter as TrafficMeter).TotalCars.ToString();
                client.Write(string.Format("{0}|{1}|{2}|{3}|UPDATE", id, date, type, value));
            }
            else if (meter.GetType().Name == "ElectricMeter")
            {
                value = (meter as ElectricMeter).KwhInUse.ToString();
                state = GetMeterState();
            }
            else throw new InvalidMeterException();
        }

        private static string GetMeterState()
        {
            var _meter = meter as ElectricMeter;
            var _chargePoint = station.ChargePoints[0];

            if (_meter.CarsParked > _chargePoint.CarCapacity) return "-1";
            else if (_meter.KwhInUse > _chargePoint.KwhCapacity) return "-1";
            else if (_meter.CarsParked == _chargePoint.CarCapacity) return "1";
            else if (_meter.RequiresMaintenance) return "2";

            return "0";
        }

        /// <summary>
        /// Envía un mensaje inicial con el estado del medidor al servidor.
        /// </summary>
        /// <exception cref="InvalidMeterException">Si el medidor es de una clase no reconocida.</exception>
        private static void SendInitialMessage()
        {
            string date = DateTime.Now.ToString(dateFormat);
            string id = meter.Id.ToString();
            string type = ParseMeterType(meter.GetType().Name);

            client.Write(string.Format("{0}|{1}|{2}", date, id, type));
        }

        /// <summary>
        /// Recibe la clase del medidor creado y retorna un string que el servidor puede leer.
        /// </summary>
        /// <param name="className">Clase del medidor.</param>
        /// <exception cref="InvalidMeterException">Si el medidor es de una clase no definida.</exception>
        /// <returns>El nombre simplificado del tipo de medidor</returns>
        private static string ParseMeterType(string className)
        {
            switch (className)
            {
                case "TrafficMeter":
                    return "tráfico";
                case "ElectricMeter":
                    return "consumo";
                default:
                    throw new InvalidMeterException();
            }
        }

        /// <summary>
        /// Esta excepción se lanza cuando un medidor no está definido dentro del sistema.
        /// </summary>
        private class InvalidMeterException : Exception
        {
            public override string Message => "Este medidor es chanta y está hecho en China.";
        }

    }
}
