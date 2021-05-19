using BombaBencinaModel.DTO;
using RandomUtils;
using RandomUtils.Exceptions;
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
            int _port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            string _ip = ConfigurationManager.AppSettings["ip"].ToString();

            try
            {
                ConsoleUtils.WriteLineWithColor("Conectando con el servidor local en el puerto " + _port + "...", ConsoleColor.Yellow);

                client = new ClientSocket(_ip, _port);
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
            bool _responseIsCorrect;

            SendInitialMessage();
            RecieveInitialResponse(out _responseIsCorrect);
            if (_responseIsCorrect) SendUpdateMessage();
        }

        /// <summary>
        /// Recibe y analiza la respuesta incial proveniente del servidor.
        /// </summary>
        private static void RecieveInitialResponse(out bool responseIsCorrect)
        {
            string _response = client.Read();
            string[] _data = _response.Split('|');

            Console.WriteLine(_response);

            responseIsCorrect = (_data[2] == "WAIT");
        }

        private static void SendUpdateMessage()
        {
            string _id = station.Id.ToString();
            string _type = ParseMeterType(meter.GetType().Name);
            string _value;
            string _state;
            string _date = DateUtils.GetDate();
            string _message;

            if (meter.GetType().Name == "TrafficMeter")
            {
                _value = (meter as TrafficMeter).TotalCars.ToString();
                _message = "{0}|{1}|{2}|{3}|UPDATE";
                client.Write(string.Format(_message, _id, _date, _type, _value));
            }
            else if (meter.GetType().Name == "ElectricMeter")
            {
                _value = (meter as ElectricMeter).KwhInUse.ToString();
                _state = GetMeterState();
                _message = "{0}|{1}|{2}|{3}|{4}|UPDATE";
                client.Write(string.Format(_message, _id, _date, _type, _value, _state));
            }
            else throw new InvalidMeterException();
        }

        /// <summary>
        /// Verifica el estado del medidor eléctrico y retorna una cadena con el ID del estado.
        /// </summary>
        /// <returns>        
        ///     <list type="bullet">
        ///         <item>
        ///             <term>"-1"</term>
        ///             <description>Error de lectura.</description>
        ///         </item>
        ///         <item>
        ///             <term>"1"</term>
        ///             <description>Punto de carga lleno.</description>
        ///         </item>
        ///         <item>
        ///             <term>"2"</term>
        ///             <description>Requiere mantención.</description>
        ///         </item>
        ///         <item>
        ///             <term>"0"</term>
        ///             <description>Todo correcto.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        private static string GetMeterState()
        {
            ElectricMeter _meter = meter as ElectricMeter;
            ChargePoint _chargePoint = station.ChargePoints[0];

            /* Los errores de lectura ocurren cuando hay más autos de los
             * que la estación permite tener, o  cuando está ocupando más
             * kw/h de los que tiene capacidad */
            if (_meter.CarsParked > _chargePoint.CarCapacity) return "-1";
            else if (_meter.KwhInUse > _chargePoint.KwhCapacity) return "-1";
            else if (_meter.RequiresMaintenance) return "2";
            else if (_meter.CarsParked == _chargePoint.CarCapacity) return "1";

            return "0";
        }

        /// <summary>
        /// Envía un mensaje inicial con el estado del medidor al servidor.
        /// </summary>
        /// <exception cref="InvalidMeterException">Si el medidor es de una clase no reconocida.</exception>
        private static void SendInitialMessage()
        {
            string _date = DateTime.Now.ToString(dateFormat);
            string _id = meter.Id.ToString();
            string _type = ParseMeterType(meter.GetType().Name);

            client.Write(string.Format("{0}|{1}|{2}", _date, _id, _type));
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



    }
}
