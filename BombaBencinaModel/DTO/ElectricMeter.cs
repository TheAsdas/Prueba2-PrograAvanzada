using BombaBencinaModel.DTO.Abstract;

namespace BombaBencinaModel.DTO
{
    public partial class ElectricMeter: Meter
    {
        private float kwhInUse;
        private bool requiresMaintenance;
        private string maintenanceReason;
        private int carsParked;

        public float KwhInUse { get => kwhInUse; set => kwhInUse = value; }
        public bool RequiresMaintenance { get => requiresMaintenance; set => requiresMaintenance = value; }
        public string MaintenanceReason { get => maintenanceReason; set => maintenanceReason = value; }
        public int CarsParked { get => carsParked; set => carsParked = value; }
    }
}
