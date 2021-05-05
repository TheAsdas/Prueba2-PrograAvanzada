using System;
using System.Collections.Generic;

namespace BombaBencinaModel.DTO
{
    public partial class TrafficMeter
    {
        public static TrafficHistory GenerateTrafficHistory(
            TrafficMeter tm,
            Station s,
            string comment
        ){
            return new TrafficHistory()
            {
                StationId = s.Id,
                CarCapacity = SumCarCapacity(s.ChargePoints),
                CarsParked = SumCarsParked(s.ChargePoints),
                Comment = comment,
                DateSent = DateTime.Now,
            };
        }

        private static int SumCarsParked(List<ChargePoint> cpList)
        {
            int total = 0;
            cpList.ForEach(cp => total += cp.Cars.Count);
            return total;
        }

        private static int SumCarCapacity(List<ChargePoint> cpList)
        {
            int total = 0;
            cpList.ForEach(cp => total += cp.CarCapacity);
            return total;
        }

        
    }
}
