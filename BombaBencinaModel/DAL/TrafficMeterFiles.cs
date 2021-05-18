using BombaBencinaModel.DAL.Interface;
using BombaBencinaModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaModel.DAL
{
    class TrafficMeterFiles: ITrafficMeter
    {
        private static int[] trafficMetersIDs = { 2, 4, 6 };
        private static ITrafficMeter instance;

        public static ITrafficMeter GetInstance()
        {
            instance = instance ?? new TrafficMeterFiles();

            return instance;
        }

        public int[] GetTrafficMeters()
        {
            return trafficMetersIDs;
        }
    }
}
