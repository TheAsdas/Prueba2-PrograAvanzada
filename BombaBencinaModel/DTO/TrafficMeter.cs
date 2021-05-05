using BombaBencinaModel.DTO.Abstract;

namespace BombaBencinaModel.DTO
{
    public partial class TrafficMeter: Meter
    {
        private int totalCars;

        public int TotalCars { get => totalCars; set => totalCars = value; }
    }
}
