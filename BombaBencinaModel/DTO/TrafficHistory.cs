using BombaBencinaModel.DTO.Abstract;

namespace BombaBencinaModel.DTO
{
    public class TrafficHistory: History
    {
        private int carCapacity;
        private int carsParked;

        public int CarCapacity { get => carCapacity; set => carCapacity = value; }
        public int CarsParked { get => carsParked; set => carsParked = value; }
    }
}
