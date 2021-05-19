using BombaBencinaModel.DAL.Interface;
using BombaBencinaModel.DTO;
using BombaBencinaModel.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RandomUtils.Exceptions;

namespace BombaBencinaModel.DAL
{
    public class ReadingFile: IReading
    {
        private static IReading instance;
        private string trafficReadingsDirectory = Directory.GetCurrentDirectory() + "/trafico.txt";
        private string electricReadingDirectory = Directory.GetCurrentDirectory() + "/consumo.txt";

        private ReadingFile() { }

        public static IReading GetInstance()
        {
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
            string _parsedHistory;
            StreamWriter _writer;

            switch(reading.GetType().Name)
            {
                case "TrafficHistory":
                    _parsedHistory = JsonSerializer.Serialize(reading as TrafficHistory);
                    _writer = new StreamWriter(trafficReadingsDirectory, true);
                    _writer.WriteLine(_parsedHistory);
                    break;
                case "ElectricHistory":
                    _parsedHistory = JsonSerializer.Serialize(reading as ElectricHistory);
                    _writer = new StreamWriter(electricReadingDirectory, true);
                    _writer.WriteLine(_parsedHistory);
                    break;
                default:
                    throw new InvalidHistoryException();
            }

            _writer.Flush();
        }
    }
}
