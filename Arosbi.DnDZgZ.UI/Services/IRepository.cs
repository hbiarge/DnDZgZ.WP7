// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Data Access contract.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.Collections.Generic;

    using Arosbi.DnDZgZ.UI.Model;

    /// <summary>
    /// DAta Access contract.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get all the bus stations.
        /// </summary>
        /// <param name="callback">The callback.</param>
        void GetBuses(Action<IEnumerable<BusServicePoint>> callback);

        /// <summary>
        /// Get all the bizi stations.
        /// </summary>
        /// <param name="callback">The callback.</param>
        void GetBizis(Action<IEnumerable<BiziServicePoint>> callback);

        /// <summary>
        /// Get all wifi hotspots.
        /// </summary>
        /// <param name="callback">The callback.</param>
        void GetWifis(Action<IEnumerable<WifiServicePoint>> callback);

        /// <summary>
        /// Get the details (buses to arrive) from a certain bus station.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="callback">The callback.</param>
        void GetBusDetails(string id, Action<BusDetail> callback);

        /// <summary>
        /// Get the details (cycles remaining) from a certain bizi station.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="callback">The callback.</param>
        void GetBiziDetails(string id, Action<BiziDetail> callback);
    }
}