using BombaBencinaModel.DTO;
using BombaBencinaModel.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombaBencinaModel.DAL.Interface
{
    public interface IReading
    {
        /// <summary>
        /// Registra una lectura de electricidad o de tráfico.
        /// </summary>
        /// <param name="reading">Entrada de historial</param>
        void RegisterReading(History reading);

        /// <summary>
        /// Consigue todas las lecturas de tráfico en una lista.
        /// </summary>
        /// <returns>Lista de lecturas de tráfico.</returns>
        List<TrafficHistory> GetTrafficReadings();

        /// <summary>
        /// Consigue todas las lecturas de electricidad en una lista.
        /// </summary>
        /// <returns>Lista con las lecturas de electricidad.</returns>
        List<ElectricHistory> GetElectricReadings();

    }
}
