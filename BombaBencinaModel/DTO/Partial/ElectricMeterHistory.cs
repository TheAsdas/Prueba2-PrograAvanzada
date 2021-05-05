using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaModel.DTO
{
    public partial class ElectricMeter
    {
        static public ElectricHistory GenerateElectricHistory(
            ElectricMeter em,
            Station s,
            string comment = null
        ){
            return new ElectricHistory()
            {
                ChargePointCapacity = s.ChargePointCapacity,
                ChargePointsInstalled = s.ChargePoints.Count,
                Comment = comment,
                DateSent = DateTime.Now,
                KwhCapacity = SumKwhCapacity(s.ChargePoints),
                KwhInUse = em.KwhInUse,
                MaintenanceReason = em.MaintenanceReason,
                RequiresMaintenence = em.RequiresMaintenance,
                StationId = s.Id
            };
        }

        static private float SumKwhCapacity(List<ChargePoint> cpList)
        {
            float total = 0;

            cpList.ForEach(cp => total += cp.KwhCapacity);

            return total;
        }

    }
}
