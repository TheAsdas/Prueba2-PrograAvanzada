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
            Identify();
            ConnectIntoServer();
            DoClientTask();


            CloseConnection();

            Console.ReadKey();
        }
    }
}
