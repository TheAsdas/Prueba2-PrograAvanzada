using BombaBencinaModel.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaModel.DAL.Factory
{
    public class ReadingFactory
    {
        public static IReading CreateDAL()
        {
            return ReadingFile.GetInstance();
        }
    }
}
