using RandomUtils;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace BombaBencinaServer
{
    partial class Program
    {
        private static string command;
        private static Dictionary<string, string> allCommands;
        private static bool clearScreen = false;

        /// <summary>
        /// Menu principal de comandos que el usuario puede ingresar.
        /// </summary>
        private static void StartCommandMenu()
        {
            CreateCommandDictionary();

            Console.WriteLine("No olvide que puede ingresar comandos. Escriba 'ayuda' para ver los comandos.");

            while (true)
            {
                AskForCommand();
                CheckCommand();
                ClearScreen();
            }
        }

        /// <summary>
        /// Limpia la pantalla si corresponde.
        /// </summary>
        private static void ClearScreen()
        {
            if (clearScreen)
            {
                Console.Clear();
                clearScreen = false;
            }
        }

        /// <summary>
        /// Crea un diccionario con los nombres de todos los comandos disponibles para el usuario, con sus respectivas explicaciones.
        /// </summary>
        private static void CreateCommandDictionary()
        {
            allCommands = new Dictionary<string, string>();

            allCommands.Add("ayuda", "Muestra todos los comandos.");
            allCommands.Add("conectar", "Conecta el servidor en un puerto específicado por usted.");
            allCommands.Add("acerca", "Revisa la información del servidor.");
            allCommands.Add("desconectar", "Desconecta la base de datos.");
            allCommands.Add("salir", "Cierra el programa.");
        }

        /// <summary>
        /// Chequea y corre los comandos válidos. Los comandos inválidos generan de un mensaje de error.
        /// </summary>
        private static void CheckCommand()
        {
            switch(command)
            {
                case "ayuda":
                    ShowAllCommands();
                    break;
                case "salir":
                    Environment.Exit(0);
                    break;
                case "conectar":
                    StartServerInSpecificPort();
                    break;
                case "desconectar":
                    DisconnectServer();
                    break;
                case "acerca":
                    ShowServerInfo();
                    break;
                default:
                    ConsoleUtils.WriteLineWithColor("Comando inválido. Ocupe 'ayuda' para ver todos los comandos.", ConsoleColor.Yellow);
                    break;
            }
        }

        /// <summary>
        /// Desconecta el servidor.
        /// </summary>
        private static void DisconnectServer()
        {
            if (serverThread == null)
            {
                ConsoleUtils.WriteLineWithColor("¡El servidor no está activo!", ConsoleColor.Red);
                return;
            }

            serverThread.Abort = true;
            serverThread.Server.Close();
            serverThread = null;
        }

        /// <summary>
        /// Muestra información del servidor.
        /// </summary>
        private static void ShowServerInfo()
        {
            if (serverThread == null)
            {
                ConsoleUtils.WriteLineWithColor("¡El servidor no está conectado! Ocupe el comando 'puerto' para crear la conexión.", ConsoleColor.Red);
                return;
            }

            ConsoleUtils.WriteLineWithColor("Acerca del servidor", ConsoleColor.Cyan);
            ConsoleUtils.WriteLineWithColor("IP: 127.0.0.1:" + serverThread.Port, ConsoleColor.Cyan);
            ConsoleUtils.WriteLineWithColor("Usuarios conectados: " + serverThread.ConnectedClients.Count, ConsoleColor.Cyan);
        }

        /// <summary>
        /// Reinicia el servidor en un puerto específicado por el usuario.
        /// </summary>
        private static void StartServerInSpecificPort()
        {
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            int newPort;

            if(serverThread != null)
            {
                ConsoleUtils.WriteLineWithColor("Atención: Usted está a punto de reiniciar el servidor en un puerto distinto.", ConsoleColor.Yellow);
                Console.WriteLine("Actualmente, el servidor corre en el puerto {0}.", port);
                Console.WriteLine("Ingrese el nuevo puerto: (0 para cancelar)");
            } 
            else
            {
                Console.WriteLine("Actualmente, el servidor no está activo.");
                Console.WriteLine("Ingrese el puerto: (0 para cancelar)");
            }

            try
            {
                newPort = Convert.ToInt32(ConsoleUtils.GetConsoleInput());
            }
            catch (Exception e)
            {
                ConsoleUtils.PrintException(e);
                return;
            }

            if (newPort == 0 || (newPort == port && serverThread != null)) return;

            ConfigurationManager.AppSettings["port"] = newPort.ToString();

            StartServerThread();
        }

        /// <summary>
        /// Muestra los comandos desde el diccionario de comandos.
        /// </summary>
        private static void ShowAllCommands()
        {
            Console.WriteLine("Comandos: ");
            foreach(KeyValuePair<string, string> entry in allCommands)
            {
                ConsoleUtils.WriteWithColor(entry.Key + ": ", ConsoleColor.Magenta);
                Console.WriteLine(entry.Value);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Recibe un input del usuario.
        /// </summary>
        private static void AskForCommand()
        {
            command = Console.ReadLine().ToLower().Trim();
        }
    }
}
