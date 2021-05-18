using BombaBencinaModel.DAL.Interface;
using BombaBencinaModel.DTO;

namespace BombaBencinaModel.DAL
{
    class ElectricMeterFiles : IElectricMeter
    {
        private static IElectricMeter instance;
        private static int[] electricMetersIDs = { 1, 3, 5 };

        public static IElectricMeter GetInstance()
        {
            instance = instance ?? new ElectricMeterFiles();
            return instance;
        }

        public int[] GetElectricMeters()
        {
            return electricMetersIDs;
        }
    }
}
