using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaModel.DTO
{
    public enum ChargePointType
    {
        ELECTRICO, DUAL
    }

    public class ChargePoint
    {
        private int id;
        private ChargePointType type;
        private int carCapacity;
        private DateTime replaceIn;
        private float kwhCapacity;
        private List<Car> cars;

        public int Id { get => id; set => id = value; }
        public ChargePointType Type { get => type; set => type = value; }
        public int CarCapacity { get => carCapacity; set => carCapacity = value; }
        public DateTime ReplaceIn { get => replaceIn; set => replaceIn = value; }
        public float KwhCapacity { get => kwhCapacity; set => kwhCapacity = value; }
        public List<Car> Cars { get => cars; set => cars = value; }

        public void AddCar(Car c)
        {
            cars.Add(c);
        }

        public void RemoveCar(Car c)
        {
            cars.Remove(c);
        }
    }

    public class Car
    {
        private string plate;
        private float kwhUsage;

        public string Plate { get => plate; set => plate = value; }
        public float KwhUsage { get => kwhUsage; set => kwhUsage = value; }
    }

}
