using BombaBencinaModel.DAL.Interface;
using BombaBencinaModel.DTO;
using BombaBencinaModel.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaModel.DAL
{
    class ReadingFile: IReading
    {
        private static IReading instance;
        private string trafficReadingsDirectory = Directory.GetCurrentDirectory() + "/trafico.txt";
        private string electricReadingDirectory = Directory.GetCurrentDirectory() + "/consumo.txt";

        private ReadingFile() { }

        public static IReading GetInstance()
        {
            if (instance is null) instance = new ReadingFile();

            instance = instance ?? new ReadingFile();
            return instance;
        }

        public List<ElectricHistory> GetElectricReadings()
        {
            throw new NotImplementedException();
        }

        public List<TrafficHistory> GetTrafficReadings()
        {
            throw new NotImplementedException();
        }

        public void RegisterReading(History reading)
        {
            throw new NotImplementedException();
        }
    }
}
