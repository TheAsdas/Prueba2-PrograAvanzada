using BombaBencinaModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaModel.DAL.Interface
{
    public interface IElectricMeter
    {
        /// <summary>
        /// Lista todos los medidores eléctricos.
        /// </summary>
        /// <returns>Lista de medidores eléctricos.</returns>
        int[] GetElectricMeters();
    }
}
