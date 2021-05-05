using System;
using System.Collections.Generic;

namespace BombaBencinaModel.DTO
{
    public class Station
    {
        private int id;
        private int chargePointCapacity;
        private DateTime open;
        private DateTime closed;
        private TrafficMeter trafficMeter;
        private ElectricMeter electricMeter;
        private Region region;
        private List<ChargePoint> chargePoints;

        public int Id { get => id; set => id = value; }
        public int ChargePointCapacity { get => chargePointCapacity; set => chargePointCapacity = value; }
        public DateTime Open { get => open; set => open = value; }
        public DateTime Closed { get => closed; set => closed = value; }
        public TrafficMeter TrafficMeter { get => trafficMeter; set => trafficMeter = value; }
        public ElectricMeter ElectricMeter { get => electricMeter; set => electricMeter = value; }
        public Region Region { get => region; set => region = value; }
        public List<ChargePoint> ChargePoints { get => chargePoints; set => chargePoints = value; }

        public void AddChargePoint(ChargePoint cp)
        {
            chargePoints.Add(cp);
        }

        public void DeleteChargePoint(ChargePoint cp)
        {
            chargePoints.Remove(cp);
        }
    }

    public class Region
    {
        private string name;
        private TimeZone timeZone;

        public string Name { get => name; set => name = value; }
        public TimeZone TimeZone { get => timeZone; set => timeZone = value; }
    }
}
