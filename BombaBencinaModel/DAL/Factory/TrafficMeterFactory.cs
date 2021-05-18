using BombaBencinaModel.DAL.Interface;

namespace BombaBencinaModel.DAL.Factory
{
    public class TrafficMeterFactory
    {
        public static ITrafficMeter CreateDal()
        {
            return TrafficMeterFiles.GetInstance();
        }
    }
}
