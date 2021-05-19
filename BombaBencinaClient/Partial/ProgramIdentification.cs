using BombaBencinaModel.DTO;
using BombaBencinaModel.DTO.Abstract;
using RandomUtils;
using SocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace BombaBencinaClient
{
    partial class Program
    {
        private static ClientSocket client;
        private static Station station;
        private static Meter meter; 


        /// <summary>
        /// Crea una estación y un punto de carga de prueba para testear el medidor.
        /// </summary>
        private static void CreateStation()
        {
            int _id = Convert.ToInt32(ConfigurationManager.AppSettings["default-station-id"]);
            int _carCapacity = Convert.ToInt32(ConfigurationManager.AppSettings["default-station-car-capacity"]);
            float _kwhCapacity = float.Parse(ConfigurationManager.AppSettings["default-station-kwh-capacity"]);
            ConsoleColor _listColor = ConsoleColor.Cyan;

            ChargePoint cp = new ChargePoint() 
            { 
                Id = 0,
                CarCapacity = _carCapacity,
                KwhCapacity = _kwhCapacity,
                Type = ChargePointType.ELECTRICO,
            };

            List<ChargePoint> cpList = new List<ChargePoint>();
            cpList.Add(cp);

            station = new Station()
            {
                Id = _id,
                ChargePointCapacity = 1,
                ChargePoints = cpList,
            };

            Console.WriteLine("Por defecto, se crea una estación de pruebas.");
            Console.WriteLine("Estos son sus propiedades:");
            ConsoleUtils.WriteWithColor("ID: ", _listColor);
            Console.WriteLine(station.Id);
            ConsoleUtils.WriteWithColor("Capacidad: ", _listColor);
            Console.WriteLine(string.Format("{0} punto/s de carga\n", station.ChargePointCapacity));
            Console.WriteLine("Propiedades del punto de carga:");
            ConsoleUtils.WriteWithColor("Capacidad: ", _listColor);
            Console.WriteLine(string.Format("{0} autos y {1} kw/h \n", cp.CarCapacity, cp.KwhCapacity));
        }

        /// <summary>
        /// Crea un punto de carga con ayuda del usuario.
        /// </summary>
        private static void Identify()
        {
            while (true)
            {
                try
                {
                    string userResponse;

                    ConsoleUtils.WriteLineWithColor("Eliga el tipo de medidor:", ConsoleColor.Yellow);
                    Console.WriteLine("1) Medidor eléctrico.");
                    Console.WriteLine("2) Medidor de tráfico.");
                    userResponse = ConsoleUtils.GetConsoleInput();
                    
                    switch (userResponse)
                    {
                        case "1":
                            createElectricMeter();
                            break;
                        case "2":
                            createTrafficMeter();
                            break;
                        default:
                            throw new Exception("Respuesta no válida.");
                    }

                    break;
                }
                catch (Exception e)
                {
                    ConsoleUtils.PrintException(e);
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Crea un medidor de tráfico con ayuda del usuario.
        /// </summary>
        private static void createTrafficMeter()
        {
            meter = new TrafficMeter()
            {
                Id = Convert.ToInt32(ConsoleUtils.GetConsoleInput("ID: ")),
                TotalCars = Convert.ToInt32(ConsoleUtils.GetConsoleInput("Autos estacionados: ")),    
            };

            station.TrafficMeter = meter as TrafficMeter;
        }

        /// <summary>
        /// Crea un medidor eléctrico con ayuda del usuario.
        /// </summary>
        private static void createElectricMeter()
        {
            meter = new ElectricMeter()
            {
                Id = Convert.ToInt32(ConsoleUtils.GetConsoleInput("ID: ")),
                KwhInUse = float.Parse(ConsoleUtils.GetConsoleInput("KW/H en uso: ")),
                RequiresMaintenance = bool.Parse(ConsoleUtils.GetConsoleInput("Requiere mantención (true/false): ")),
                CarsParked = Convert.ToInt32(ConsoleUtils.GetConsoleInput("Autos estacionados:")),
            };

            if ((meter as ElectricMeter).RequiresMaintenance)
            {
                (meter as ElectricMeter).MaintenanceReason = ConsoleUtils.GetConsoleInput("Razón de mantenimineto: ");
            }

            station.ElectricMeter = meter as ElectricMeter;
        }



    }
}
