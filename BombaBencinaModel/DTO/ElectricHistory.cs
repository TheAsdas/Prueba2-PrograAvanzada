using BombaBencinaModel.DTO.Abstract;

namespace BombaBencinaModel.DTO
{
    public class ElectricHistory: History
    {
        private int chargePointCapacity;
        private int chargePointsInstalled;
        private float kwhCapacity;
        private float kwhInUse;
        private bool requiresMaintenence;
        private string maintenanceReason;

        public int ChargePointCapacity { get => chargePointCapacity; set => chargePointCapacity = value; }
        public int ChargePointsInstalled { get => chargePointsInstalled; set => chargePointsInstalled = value; }
        public float KwhCapacity { get => kwhCapacity; set => kwhCapacity = value; }
        public float KwhInUse { get => kwhInUse; set => kwhInUse = value; }
        public bool RequiresMaintenence { get => requiresMaintenence; set => requiresMaintenence = value; }
        public string MaintenanceReason { get => maintenanceReason; set => maintenanceReason = value; }
    }
}
