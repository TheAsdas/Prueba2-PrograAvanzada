using BombaBencinaModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaModel.DAL.Interface
{
    public interface ITrafficMeter
    {
        /// <summary>
        /// Lista todos los medidores de tráficos disponibles.
        /// </summary>
        /// <returns>Lista con medidores de tráfico.</returns>
        int[] GetTrafficMeters();
    }
}
