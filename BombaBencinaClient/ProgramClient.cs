using RandomUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaClient
{
    partial class Program
    {
        static void Main(string[] args)
        {
            StartClient();
        }

        private static void StartClient()
        {
            CreateStation();

            while(true)
            {
                Identify();
                ConnectIntoServer();
                DoClientTask();
                CloseConnection();

                string response = ConsoleUtils.GetConsoleInput("¿Reiniciar cliente? (Si/No)")
                    .ToLower()
                    .Trim();
                if (response == "n" || response == "no") break;
            }

            Console.ReadKey();
        }
    }
}
